using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovementOutside : MonoBehaviour
{
    public float speed = 5.0f;
    private Animator animator;
    private Vector3 movement;

    private Collider myCollider;
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(0.0f, 0.0f, 1.0f);
        animator = GetComponent<Animator>();

        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the character forward
        transform.Translate(movement * speed * Time.deltaTime);

        // Play the walking animation
        animator.SetFloat("Speed", speed);
    }
}
