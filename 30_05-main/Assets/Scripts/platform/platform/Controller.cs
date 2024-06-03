using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public List<ShakeBlink> platforms; 

    public AudioClip initialAudio;     
    public AudioClip stage1Audio;      
    public AudioClip stage2Audio;      
    public AudioClip finalAudio;       
    public AudioClip shakeBlinkBackgroundAudio; 

    private AudioSource audioSource;   
    private AudioSource backgroundAudioSource; 

    public static Controller instance; 

    public GameObject boot1; // Player  boot 1
    public GameObject boot2; // Player boot 2
    public Transform[] spawnPoints1;   // Spawn points for players with boot 1
    public Transform[] spawnPoints2;   // Spawn points for players with boot 2

    private bool finalSoundPlayed = false; // Flag to ensure final sound is played only once

    void Awake()
    {
        // Ensure there's only one instance of the controller
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        backgroundAudioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(InitialSound());
    }

    public static void EndGamePlayer1()
    {
        instance.boot1.SetActive(false);
    }

    public static void EndGamePlayer2()
    {
        instance.boot2.SetActive(false);
    }

    public static void RestartGame()
    {
        if (instance != null)
        {
            // Stop any ongoing coroutines
            instance.StopAllCoroutines();

            instance.boot1.SetActive(true);
            instance.boot2.SetActive(true);

            // Reset player counters
            Lives.ResetPlayerCounters();

            //Sound 



            // Restart the main coroutine
            instance.StartCoroutine(instance.ManagePlatformMovements());
        }
    }

    private IEnumerator InitialSound()
    {
        audioSource.clip = initialAudio;
        audioSource.Play();
        yield return new WaitForSeconds(15.0f);
        StartCoroutine(ManagePlatformMovements());
    }

    private IEnumerator ManagePlatformMovements()
    {
        yield return new WaitForSeconds(5.0f);
        audioSource.clip = stage1Audio;
        audioSource.Play();
        yield return new WaitForSeconds(stage1Audio.length); 

        StartShakeAndBlink(3);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(5);
        yield return new WaitForSeconds(12.0f);

        yield return new WaitForSeconds(2.0f);
        backgroundAudioSource.Stop();

        yield return new WaitForSeconds(1.0f);
        audioSource.clip = stage2Audio;
        audioSource.Play();
        yield return new WaitForSeconds(stage2Audio.length); 

        StartShakeAndBlink(9);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(12);
        yield return new WaitForSeconds(12.0f);

        StartShakeAndBlink(14);
        yield return new WaitForSeconds(12.0f);

        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Stop();
        }

        audioSource.clip = finalAudio; // Audio final 
        audioSource.Play();
        finalSoundPlayed = true;

        StartCoroutine(WaitForFinalSoundToEnd());
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

    private IEnumerator WaitForFinalSoundToEnd()
    {

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        changeScene();
    }

    void changeScene()
    {
        if (finalSoundPlayed)
        {
            SceneManager.UnloadSceneAsync("Platform");
            SceneManager.LoadScene("Get_Out", LoadSceneMode.Additive);
        }
    }
}
