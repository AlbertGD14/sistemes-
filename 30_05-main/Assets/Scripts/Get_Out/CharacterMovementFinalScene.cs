using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovementFinalScene : MonoBehaviour
{
    public float speed = 5.0f;
    private Animator animator;
    private Vector3 movement;

    private Collider myCollider;
    private Rigidbody myRigidbody;

    void Start()
    {
        movement = new Vector3(0.0f, 0.0f, 1.0f);
        animator = GetComponent<Animator>();

        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

        StartCoroutine(StartMovementAfterDelay(5.0f));
    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        StartCoroutine(MoveCharacter());
    }

    IEnumerator MoveCharacter()
    {
        while (true)
        {
            transform.Translate(movement * speed * Time.deltaTime);

            animator.SetFloat("Speed", speed);

            yield return null;
        }
    }
}
