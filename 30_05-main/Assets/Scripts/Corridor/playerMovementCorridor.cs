using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementCorridor : MonoBehaviour
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
            // Mover el personaje hacia la posición objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Reproducir la animación de caminar
            animator.SetFloat("Speed", speed);

            // Reproducir el audio de los pasos
            if (!audioSource.isPlaying)
            {
                audioSource.clip = secondPhase ? secondFootstepClip : firstFootstepClip;
                audioSource.Play();
            }

            // Verificar si el jugador ha llegado a la posición objetivo
            if (transform.position == targetPosition)
            {
                isMoving = false;
                animator.SetFloat("Speed", 0);
                audioSource.Stop(); // Detener el audio de los pasos cuando el jugador deja de caminar
                corridor.Instance.OnPlayerReachedPosition(); // Llamar al método en el script corridor
            }
        }
    }

    public void MoveTo(float targetZ, bool secondPhase = false)
    {
        this.secondPhase = secondPhase;
        targetPosition = new Vector3(transform.position.x, transform.position.y, targetZ);
        isMoving = true;
    }
}
