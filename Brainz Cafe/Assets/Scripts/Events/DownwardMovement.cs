using System.Collections;
using UnityEngine;

public class DownwardMovement : MonoBehaviour
{
    public float targetY = 5.5f;
    public float speed = 1f;
    public float upwardSpeed = 1f;
    public float delayBeforeMovingUp = 5f;

    private bool isMovingDownward = false;
    private bool isMovingUpward = false;

    public void StartDownwardMovement()
    {
        isMovingDownward = true;
    }

    private void Update()
    {
        if (isMovingDownward)
        {
            AcidGangDownwardMovement();
        }

        if (isMovingUpward)
        {
            MoveUpward();
        }
    }

    public void AcidGangDownwardMovement()
    {
        // Calculate the direction to the target position
        Vector2 direction = new Vector2(0f, targetY - transform.position.y).normalized;

        // Move the object downward
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the object reached the target Y position
        if (transform.position.y <= targetY)
        {
            StopDownwardMovement();

            // Start the upward movement after a delay
            StartCoroutine(StartUpwardMovementWithDelay());
        }
    }

    private IEnumerator StartUpwardMovementWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeMovingUp);
        StartUpwardMovement();
    }

    private void StartUpwardMovement()
    {
        isMovingUpward = true;
    }

    private void MoveUpward()
    {
        // Move the object upwards
        transform.Translate(Vector2.up * upwardSpeed * Time.deltaTime);

        // Check if the object passed the target Y position (y = 7)
        if (transform.position.y >= 7f)
        {
            StopUpwardMovement();
        }
    }

    // Call this method to stop the downward movement
    public void StopDownwardMovement()
    {
        isMovingDownward = false;
    }

    // Call this method to stop the upward movement
    private void StopUpwardMovement()
    {
        isMovingUpward = false;
    }
}
