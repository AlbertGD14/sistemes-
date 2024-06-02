using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corridor : MonoBehaviour
{
    // Singleton instance
    public static corridor Instance { get; private set; }

    public AudioClip corridorClip;
    private AudioSource audioSource;

    public float firstStopZ = 14.8f; // La posición Z donde el jugador se detendrá primero
    public float finalZ = 102f; // La posición Z final

    private bool audioPlayed = false;

    private void Awake()
    {
        // Implementar el patrón singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        // Mover jugadores a la primera posición
        MovePlayersTo(firstStopZ, false);

        // Esperar hasta que todos los jugadores lleguen a la primera posición
        yield return new WaitUntil(() => !AnyPlayerMoving());

        // Reproducir el audio del corredor
        PlayCorridorIntro();

        // Esperar a que el audio del corredor termine
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Mover jugadores a la posición final
        MovePlayersTo(finalZ, true);

        // Esperar hasta que todos los jugadores lleguen a la posición final
        yield return new WaitUntil(() => !AnyPlayerMoving());
    }

    private void MovePlayersTo(float targetZ, bool secondPhase)
    {
        playerMovementCorridor[] players = FindObjectsOfType<playerMovementCorridor>();
        foreach (var player in players)
        {
            player.MoveTo(targetZ, secondPhase);
        }
    }

    private bool AnyPlayerMoving()
    {
        playerMovementCorridor[] players = FindObjectsOfType<playerMovementCorridor>();
        foreach (var player in players)
        {
            if (player.IsMoving)
            {
                return true;
            }
        }
        return false;
    }

    public void PlayCorridorIntro()
    {
        if (!audioPlayed)
        {
            if (corridorClip != null && audioSource != null)
            {
                audioSource.clip = corridorClip;
                audioSource.Play();
                audioPlayed = true;
            }
        }
    }

    public void OnPlayerReachedPosition()
    {
        if (!AnyPlayerMoving() && !audioPlayed)
        {
            PlayCorridorIntro();
        }
    }
}
