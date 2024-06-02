using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementMommy : MonoBehaviour
{
    public float speed = 5.0f;
    public AudioClip firstFootstepClip; // AudioClip para los primeros pasos
    public AudioClip secondFootstepClip; // AudioClip para los segundos pasos
    private Animator animator;
    private AudioSource audioSource;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool secondPhase = false;

    public bool IsMoving
    {
        get { return isMoving; }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isMoving)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

           
            animator.SetFloat("Speed", speed);

            
            if (!audioSource.isPlaying)
            {
                audioSource.clip = secondPhase ? secondFootstepClip : firstFootstepClip;
                audioSource.Play();
            }

            
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                animator.SetFloat("Speed", 0);
                audioSource.Stop(); 
                mommy.Instance.OnPlayersReachedFinalPosition(); 
            }
        }
    }

    public void MoveTo(float targetZ, bool secondPhase = false)
    {
        this.secondPhase = secondPhase;
        targetPosition = new Vector3(transform.position.x, transform.position.y, targetZ);
        isMoving = true;
    }

    public void Rotate180Degrees()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
