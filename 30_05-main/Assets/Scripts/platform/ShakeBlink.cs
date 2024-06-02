using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBlink : MonoBehaviour
{
    public float shakeDuration = 10.0f;  // Duration of the shake
    public float shakeMagnitude = 0.1f;  // Magnitude of the shake
    public float initialBlinkFrequency = 1.0f; // Initial blink frequency
    public float finalBlinkFrequency = 0.1f; // Final blink frequency
    public AudioClip blackTurnClip; // Audio clip to play when the platform turns black
    public AudioClip stage2Audio;  // Audio clip for stage 2
    public AudioClip finalAudio;   // Final audio clip

    private Vector3 originalPosition;
    private Renderer platformRenderer;
    private Color originalColor;
    private List<GameObject> objectsOnPlatform = new List<GameObject>();
    private AudioSource audioSource;
    private AudioSource backgroundAudioSource;
    private AudioClip backgroundAudioClip;
    private bool isShakeInProgress = false; // Flag to track if shake and blink is in progress

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
        isShakeInProgress = true; // Set flag to true at the start of shake and blink

        float elapsed = 0.0f;
        bool isRed = false;
        float blinkFrequency = initialBlinkFrequency;

        // Start playing background audio if not already playing
        if (backgroundAudioSource != null && backgroundAudioClip != null && !backgroundAudioSource.isPlaying)
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

            if (platformRenderer != null)
            {
                platformRenderer.material.color = isRed ? originalColor : Color.red;
                isRed = !isRed;
            }

            elapsed += blinkFrequency;
            yield return new WaitForSeconds(blinkFrequency);

            float t = elapsed / shakeDuration;
            blinkFrequency = Mathf.Lerp(initialBlinkFrequency, finalBlinkFrequency, t);
        }

        isShakeInProgress = false; // Reset flag to false when shake and blink is finished

        if (stage2Audio != null && audioSource != null)
        {
            audioSource.clip = stage2Audio;
            audioSource.Play();
            backgroundAudioSource.volume = 0.3f; // Reduce background audio volume
            yield return new WaitForSeconds(stage2Audio.length); // Wait for stage 2 audio to finish
        }


        // Dim the background audio before playing the black turn clip
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.volume = 0.3f; // Reduce volume to 30%
        }

        // Finish with the platform's color turning black for 0.5 seconds
        if (platformRenderer != null)
        {
            platformRenderer.material.color = Color.black;
        }

        // Play the audio clip when the platform turns black
        if (blackTurnClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(blackTurnClip);
            // Wait for the black turn clip to finish
            yield return new WaitForSeconds(blackTurnClip.length);
        }
        else
        {
            // Default wait time if blackTurnClip is not set
            yield return new WaitForSeconds(0.5f);
        }

        // Restore the background audio volume
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.volume = 1.0f; // Restore volume to 100%
        }

        // Destroy all objects in the list
        foreach (var obj in objectsOnPlatform)
        {
            if (obj != null)
            {
                Lives lives = obj.GetComponent<Lives>();
                if (lives != null)
                {
                    lives.LoseLife();
                }
            }
        }
        objectsOnPlatform.Clear(); // Clear the list

        yield return new WaitForSeconds(0.5f);

        // Restore the original state
        transform.localPosition = originalPosition;
        if (platformRenderer != null)
        {
            platformRenderer.material.color = originalColor;
        }

        if (finalAudio != null && audioSource != null)
        {
            audioSource.clip = finalAudio;
            audioSource.Play();
            backgroundAudioSource.Stop(); //
            backgroundAudioSource.Stop(); // Stop background audio
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
