using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private static int playerLives1 = 2;
    private static int playerLives2 = 2;

    private GameObject player1;
    private GameObject player2;

    public AudioClip loseLifeSound1;
    public AudioClip loseLifeSound2;
    public AudioClip loseLifeSound12;

    private AudioSource audioSource;
    private static int totalPlayers = 2;
    private static int playersOut = 0;
    private static bool isInvokingSound = false; // Flag to check if the sound is already scheduled

    private static bool soundplayer1;
    private static bool soundplayer2;

    void Start()
    {

        player1 = Controller.instance.boot1;
        player2 = Controller.instance.boot2;

        soundplayer1 = false;
        soundplayer2 = false;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void LoseLife(int playerID)
    {
        if (playerID == 1)
        {
            playerLives1 -= 1;
            soundplayer1 = true;
        }

        if (playerID == 2)
        {
            playerLives2 -= 1;
            soundplayer2 = true;
        }

        livesPlayers();
    }

    public void livesPlayers()
    {
        if (playerLives1 == 1 && soundplayer2 == false && soundplayer1)
        {
            audioSource.clip = loseLifeSound1;
            audioSource.Play();
            soundplayer1 = false;
        }

        if (playerLives1 == 0 && soundplayer2 == false && soundplayer1)
        {
            audioSource.clip = loseLifeSound2;
            audioSource.Play();
            soundplayer1 = false;
            soundplayer2 = false;

            Controller.EndGamePlayer1();
            playersOut++;


            if (playersOut == totalPlayers)
            {
                Controller.RestartGame();
            }
        }

        if (playerLives1 == 1 && playerLives2 == 1 && soundplayer1 && soundplayer2)
        {
            audioSource.clip = loseLifeSound12;
            audioSource.Play();
            soundplayer1 = false;
            soundplayer2 = false;
        }

        if (playerLives1 == 0 && playerLives2 == 0 && soundplayer1 && soundplayer2)
        {
            audioSource.clip = loseLifeSound12;
            audioSource.Play();
            soundplayer1 = false;
            soundplayer2 = false;

            Controller.EndGamePlayer2();
            playersOut++;


            if (playersOut == totalPlayers)
            {
                Controller.RestartGame();
            }
        }

        if (playerLives2 == 1 && soundplayer1 == false && soundplayer2)
        {
            audioSource.clip = loseLifeSound1;
            audioSource.Play();
            soundplayer2 = false;
        }

        if (playerLives2 == 0 && soundplayer1 == false && soundplayer2)
        {
            audioSource.clip = loseLifeSound2;
            audioSource.Play();
            soundplayer2 = false;

            Controller.EndGamePlayer2();
            playersOut++;


            if (playersOut == totalPlayers)
            {
                Controller.RestartGame();
            }
        }
    }

    public static void ResetPlayerCounters()
    {
        totalPlayers = 2;
        playersOut = 0;
        playerLives1 = 2;
        playerLives2 = 2;
    }
}