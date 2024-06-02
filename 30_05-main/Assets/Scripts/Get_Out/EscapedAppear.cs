using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EscapedAppear : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float fadeDuration = 1f;

    public AudioSource audioSource;

    public AudioClip appearSound;

    private bool isCanvasVisible = false;

    void Start()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

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
                StopAllCoroutines();
                StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f));
                isCanvasVisible = true;

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
                StopAllCoroutines();
                StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f));
                isCanvasVisible = false;
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

        cg.interactable = (end == 1f);
        cg.blocksRaycasts = (end == 1f);
    }
}
