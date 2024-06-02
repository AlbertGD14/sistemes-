using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager1 : MonoBehaviour
{
    public AudioClip initialSound; // Initial sound to play when the game starts
    public AudioClip backgroundSound; // Background sound to play while playing
    public AudioClip finalSound; // Final sound to play when the game ends

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool finalSoundPlayed = false; // Flag to ensure final sound is played only once

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an AudioSource component is attached
        if (audioSource == null)
        {
            // If not attached, add the AudioSource component
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Play the initial sound when the game starts
        if (initialSound != null)
        {
            audioSource.clip = initialSound;
            audioSource.Play();

            // Invoke the PlayBackgroundSound method after the initial sound has finished playing
            Invoke("PlayBackgroundSound", initialSound.length);
        }
        else
        {
            Debug.LogWarning("No initial sound assigned.");
        }
    }

    void Update()
    {
        CheckForFinalSound();
    }

    // Play the background sound
    void PlayBackgroundSound()
    {
        if (backgroundSound != null)
        {
            audioSource.clip = backgroundSound;
            audioSource.loop = true; // Loop the background sound
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No background sound assigned.");
        }
    }

    // Play the final sound
    public void PlayFinalSound()
    {
        if (finalSound != null)
        {
            audioSource.clip = finalSound;
            audioSource.loop = false; // Do not loop the final sound
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No final sound assigned.");
        }
    }

    // Check for the number of GameObjects tagged as "plat"
    void CheckForFinalSound()
    {
        if (!finalSoundPlayed && GameObject.FindGameObjectsWithTag("plat").Length == 6)
        {
            // Stop background sound
            audioSource.Stop();

            // Play final sound
            PlayFinalSound();

            // Set flag to prevent playing final sound again
            finalSoundPlayed = true;
        }
    }
}
