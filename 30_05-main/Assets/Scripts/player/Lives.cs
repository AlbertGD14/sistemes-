using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public int playerLives = 2; // Number of lives the player has
    public AudioClip loseLifeSound1; // Sound to play when losing the first life
    public AudioClip loseLifeSound2; // Sound to play when losing the second life

    private AudioSource audioSource; // Reference to the AudioSource component

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
    }

    public void LoseLife()
    {
        if (playerLives > 0)
        {
            playerLives--;

            // Play the corresponding sound based on the remaining lives
            if (playerLives == 1)
            {
                audioSource.clip = loseLifeSound1;
                audioSource.Play();
            }
            else if (playerLives == 0)
            {
                audioSource.clip = loseLifeSound2;
                audioSource.Play();
                Destroy(gameObject);

            }
        }
    }
}
