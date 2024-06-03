using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ShakeBlink : MonoBehaviour
{
    public float shakeDuration = 10.0f;  
    public float shakeMagnitude = 0.1f;  
    public float initialBlinkFrequency = 1.0f; 
    public float finalBlinkFrequency = 0.1f; 
    public AudioClip blackTurnClip; 

    private Vector3 originalPosition;
    private Renderer platformRenderer;
    private Color originalColor;
    private List<GameObject> objectsOnPlatform = new List<GameObject>(); 
    private AudioSource audioSource; 
    private AudioSource backgroundAudioSource;
    private AudioClip backgroundAudioClip; 
    private bool isShakeInProgress = false; 


    void Start()
    {
        originalPosition = transform.localPosition; 
        platformRenderer = GetComponent<Renderer>(); 
        if (platformRenderer != null)
        {
            originalColor = platformRenderer.material.color; 
        }

        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerShakeAndBlink();
        }
    }

    public void TriggerShakeAndBlink()
    {
        if (!isShakeInProgress)
        {
            StartCoroutine(ShakeAndBlink());
        }
    }

    public void SetBackgroundAudioSource(AudioSource source, AudioClip clip)
    {
        backgroundAudioSource = source;
        backgroundAudioClip = clip;
    }

    private IEnumerator ShakeAndBlink()
    {
        isShakeInProgress = true;
        float elapsed = 0.0f;
        bool isRed = false;
        float blinkFrequency = initialBlinkFrequency;

        if (backgroundAudioSource != null && backgroundAudioClip != null)
        {
            backgroundAudioSource.clip = backgroundAudioClip;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            // Blink logic
            if (platformRenderer != null)
            {
                platformRenderer.material.color = isRed ? originalColor : Color.red;
                isRed = !isRed;
            }

            elapsed += blinkFrequency;
            yield return new WaitForSeconds(blinkFrequency);

            // Update blink frequency to become faster over time
            float t = elapsed / shakeDuration;
            blinkFrequency = Mathf.Lerp(initialBlinkFrequency, finalBlinkFrequency, t);
        }
        isShakeInProgress = false; // Reset flag to false when shake and blink is finished

        // Stop the background audio
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Stop();
        }

        // Finish with the platform's color turning black for 0.5 seconds
        if (platformRenderer != null)
        {
            platformRenderer.material.color = Color.black;
        }

        // Play the audio when the platform turns black
        if (blackTurnClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(blackTurnClip);
            yield return new WaitForSeconds(2.0f);
        }

        // Destroy players
        foreach (var obj in objectsOnPlatform)
        {
            if (obj != null)
            {
                Lives lives = obj.GetComponent<Lives>();
                if (lives != null)
                {
                    if (obj == Controller.instance.boot1)
                    {
                        lives.LoseLife(1);
                    }
                    else if (obj == Controller.instance.boot2)
                    {
                        lives.LoseLife(2);
                    }
                }
            }
        }

        objectsOnPlatform.Clear(); 

        yield return new WaitForSeconds(0.5f);

        // Restore the original state
        transform.localPosition = originalPosition;
        if (platformRenderer != null)
        {
            platformRenderer.material.color = originalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("jugador"))
        {
            if (!objectsOnPlatform.Contains(other.gameObject))
            {
                objectsOnPlatform.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("jugador"))
        {
            if (objectsOnPlatform.Contains(other.gameObject))
            {
                objectsOnPlatform.Remove(other.gameObject);
            }
        }
    }
}



