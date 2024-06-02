using UnityEngine;

public class mommyMovement : MonoBehaviour
{
    public float moveSpeed;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void MoveMommyTo(float targetZ)
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y, targetZ);
        isMoving = true;
    }

    private void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
