using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<ShakeBlink> platforms; // List of platforms with ShakeBlink script

    public AudioClip initialAudio;     // Initial audio clip
    public AudioClip stage1Audio;      // Audio clip before stage 1
    public AudioClip stage2Audio;      // Audio clip before stage 2
    public AudioClip finalAudio;       // Final audio clip
    public AudioClip shakeBlinkBackgroundAudio; // Background audio during shake and blink

    private AudioSource audioSource;   // Reference to the AudioSource component
    private AudioSource backgroundAudioSource; // Reference to the background AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        backgroundAudioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(ManagePlatformMovements());
    }

    private IEnumerator ManagePlatformMovements()
    {
        audioSource.clip = initialAudio;
        audioSource.Play();
        yield return new WaitForSeconds(20.0f); 

        audioSource.clip = stage1Audio;
        audioSource.Play();
        yield return new WaitForSeconds(stage1Audio.length); // Wait for stage 1 audio to finish

        // Start shake and blink on platforms
        StartShakeAndBlink(3);
        yield return new WaitForSeconds(12.0f); 

        StartShakeAndBlink(4);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(5);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(6);
        yield return new WaitForSeconds(12.0f); 

        backgroundAudioSource.Stop();

        audioSource.clip = stage2Audio;
        audioSource.Play();
        yield return new WaitForSeconds(stage2Audio.length);

        StartShakeAndBlink(8);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(9);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(10);
        yield return new WaitForSeconds(12.0f); 

        // Stop background audio before playing final audio
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Stop();
        }

        // Play the final audio
        audioSource.clip = finalAudio;
        audioSource.Play();
    }

    private void StartShakeAndBlink(int count)
    {
        List<ShakeBlink> availablePlatforms = new List<ShakeBlink>(platforms);
        int platformsToShake = Mathf.Min(count, availablePlatforms.Count);

        for (int i = 0; i < platformsToShake; i++)
        {
            int randomIndex = Random.Range(0, availablePlatforms.Count);
            ShakeBlink platform = availablePlatforms[randomIndex];
            platform.SetBackgroundAudioSource(backgroundAudioSource, shakeBlinkBackgroundAudio);
            platform.TriggerShakeAndBlink();
            availablePlatforms.RemoveAt(randomIndex);
        }
    }
}
