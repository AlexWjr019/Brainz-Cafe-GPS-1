using UnityEngine;

public class SlowdownArea : MonoBehaviour
{
    private float slowdownFactor = 0.5f; // Factor by which the player's movement speed will be reduced
    private float tempSpeed;
    private bool isSlowingDown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null && !isSlowingDown)
            {
                tempSpeed = playerMovement.walkSpeed;
                playerMovement.walkSpeed *= slowdownFactor;
                isSlowingDown = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null && isSlowingDown)
            {
                playerMovement.walkSpeed = tempSpeed;
                isSlowingDown = false;
            }
        }
    }
}
