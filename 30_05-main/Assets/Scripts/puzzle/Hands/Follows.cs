using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follows : MonoBehaviour
{
    public Transform target; // Reference to the GameObject (e.g., player) that the Hands will follow

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
        else
        {
            Debug.LogWarning("Target is not assigned. Hands cannot follow.");
        }
    }
}


