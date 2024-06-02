using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruz : MonoBehaviour
{
    private Transform playerTransform;
    public Transform specificPosition;
    public AudioClip pickUpSound; 
    public AudioClip dropSound;

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool placed = false;

    public string newTag = "placed"; // New tag to set when the platform becomes black
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

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jugador"))
        {
            AttachToPlayer(other.transform);
        }
        else if (other.CompareTag("plat"))
        {
            PlaceOnSpecificPosition();
            DetachFromPlayer(); 
        }
    }

    private void AttachToPlayer(Transform player)
    {
        if (!placed)
        {
            audioSource.clip = pickUpSound;
            audioSource.Play();
            playerTransform = player;
            transform.SetParent(playerTransform);
        }
    }

    private void DetachFromPlayer()
    {
        if (playerTransform != null)
        {
            audioSource.clip = dropSound;
            audioSource.Play();
            transform.SetParent(null);
            playerTransform = null;
        }
    }

    private void PlaceOnSpecificPosition()
    {
        if (placed == false)
        {
            if (specificPosition != null)
            {
                transform.position = specificPosition.position;
                transform.rotation = specificPosition.rotation;
                gameObject.tag = newTag;
                audioSource.clip = dropSound;
                audioSource.Play();
                placed = true;
            }
            else
            {
                Debug.LogWarning("SpecificPosition is not assigned in the Inspector");
            }
        }
    }
}


