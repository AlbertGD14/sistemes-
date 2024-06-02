using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TitleAppear : MonoBehaviour
{
    // Reference to the Canvas Group component
    public CanvasGroup canvasGroup;

    // Duration for fading in and out
    public float fadeDuration = 1f;

    // Reference to the AudioSource component
    public AudioSource audioSource;

    // Sound to play when the canvas appears
    public AudioClip appearSound;

    // Flag to check if the canvas is visible
    private bool isCanvasVisible = false;

    void Start()
    {
        // Ensure the canvas is initially hidden
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        // Ensure the audio source is not playing at start
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (canvasGroup != null && !isCanvasVisible)
            {
                StopAllCoroutines();  // Stop any ongoing fade out
                StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f));
                isCanvasVisible = true; // Mark the canvas as visible

                // Play the appear sound
                if (audioSource != null && appearSound != null)
                {
                    audioSource.clip = appearSound;
                    audioSource.Play();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (canvasGroup != null && isCanvasVisible)
            {
                StopAllCoroutines();  // Stop any ongoing fade in
                StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f));
                isCanvasVisible = false; // Mark the canvas as not visible
            }
        }
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / fadeDuration);
            yield return null;
        }

        cg.alpha = end;

        // Enable/disable interaction based on final alpha value
        cg.interactable = (end == 1f);
        cg.blocksRaycasts = (end == 1f);
    }
}
