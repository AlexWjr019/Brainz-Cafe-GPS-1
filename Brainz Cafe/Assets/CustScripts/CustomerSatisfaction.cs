using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSatisfaction : MonoBehaviour
{
    public Image timer_radial_image;
    public float time_remaining;
    public float max_time = 5.0f;
    private bool isSitting = false; // Flag to track if the customer is sitting

    public bool isChasing = false; // Flag to track if the customer is currently chasing the player
    private float chaseDuration = 10f; // Duration of the chase in seconds
    private float chaseTimer = 0f; // Timer to track the chase duration

    public string playerTag = "Player"; // Tag of the player object
    public float moveSpeed = 5f; // Speed at which the customer moves towards the player

    private Transform player; // Reference to the player's transform

    void Start()
    {
        time_remaining = max_time;
        timer_radial_image.gameObject.SetActive(false); // Deactivate the image at the start

        // Find the player object with the specified tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found with tag: " + playerTag);
        }
    }

    void Update()
    {
        if (isSitting)
        {
            if (time_remaining > 0)
            {
                time_remaining -= Time.deltaTime;
                timer_radial_image.fillAmount = time_remaining / max_time;
            }
            else
            {
                if (!isChasing)
                {
                    // Customer satisfaction time has run out, start chasing the player
                    StartChase();
                }
            }
        }

        if (isChasing)
        {
            // Update the chase timer
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseDuration)
            {
                // Chase duration has ended, destroy the customer
                DestroyCustomer();
            }
            else
            {
                if (player != null)
                {
                    // Move the customer towards the player
                    Vector2 direction = player.position + new Vector3(1.2f, 0f, 0f) - transform.position;
                    direction.Normalize();
                    transform.Translate(direction * moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    private IEnumerator StartTimerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isSitting)
        {
            timer_radial_image.gameObject.SetActive(true); // Activate the image after the delay
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chair"))
        {
            // Customer is sitting on a chair
            isSitting = true;
            StartCoroutine(StartTimerAfterDelay(2f)); // Start the timer after a 2-second delay
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Chair"))
        {
            // Customer is leaving the chair
            isSitting = false;
            timer_radial_image.gameObject.SetActive(false); // Deactivate the image when the customer is leaving the chair
        }
    }

    public void StartChase()
    {
        isChasing = true;
    }

    private void DestroyCustomer()
    {
        // Perform any necessary clean-up tasks
        Destroy(gameObject);
    }
}
