using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClownSatisfactionTimer : MonoBehaviour
{
    public Image timer_radial_image;
    public float time_remaining;
    public float max_time = 5.0f;
    private bool isSitting = false; // Flag to track if the customer is sitting

    public bool isChasing = false; // Flag to track if the customer is currently chasing the player
    //private float chaseDuration = 10f; // Duration of the chase in seconds
    //private float chaseTimer = 0f; // Timer to track the chase duration

    public string playerTag = "Chairs"; // Tag of the player object
    public float moveSpeed = 5f; // Speed at which the customer moves towards the player

    //public float dmgRate = 1f; // Rate at which damage is applied to the table
    //private float dmgTimer = 0f; // Timer to track the time since the last damage

    private Transform player; // Reference to the player's transform
    private float destroyTimer = 0f;
    private float destroyDelay = 3f;
    public CustomerInteraction CustomerInteraction;

    //private Coroutine dmgCoroutine; // Reference to the damage coroutine
    //private bool shouldDamageTable = true; // Flag to track if the customer should damage the table

    public JumpScare jumpscareImage;
    public bool jumpscare = false;

    public int moneyDrop;
    void Start()
    {
        //jumpscare.SetActive(false);

        time_remaining = max_time;
        timer_radial_image.gameObject.SetActive(true); // Deactivate the image at the start

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

        GameObject jumpScareObject = GameObject.FindGameObjectWithTag("JumpScare");
        if (jumpScareObject != null)
        {
            jumpscareImage = jumpScareObject.GetComponent<JumpScare>();
        }
        else
        {
            Debug.LogWarning("TileSpawner object not found with tag: TileSpawner");
        }

        CustomerInteraction = GetComponent<CustomerInteraction>();

    }

    void Update()
    {
        if (isSitting)
        {

            if (time_remaining > 0)
            {
                if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                {
                    // Do nothing, time_remaining will remain unchanged
                    if (destroyTimer < destroyDelay)
                    {
                        destroyTimer += Time.deltaTime;
                    }
                    else
                    {
                        DestroyCustomer();
                    }
                }
                else
                {
                    time_remaining -= Time.deltaTime;
                    timer_radial_image.fillAmount = time_remaining / max_time;
                }

            }
            else
            {
                if (jumpscare)
                {
                    SpawnImageForPlayer();
                }


                if (!isChasing)
                {
                    StartChase();
                    //DestroyCustomer();
                    //jumpscare.SetActive(true);
                    //jumpscare player with a fade in and fade out screen and steal point from player
                }

            }
        }
        if (isChasing)
        {
            if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
            {

                StartCoroutine(LeaveAfterDelay(3f));
                CurrencyManager.Instance.AddMoney(moneyDrop);
            }

        }

    }
    private void SpawnImageForPlayer()
    {
        // Check if the tileSpawner is assigned and the tilePrefab is set

        // Call the SpawnTile method on the tileSpawner
        jumpscareImage.SpawnImage();
        jumpscare = false;

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
        if (collision.gameObject.CompareTag("Chairs"))
        {
            isSitting = true;
            jumpscare = true;
            StartCoroutine(StartTimerAfterDelay(2f)); // Start the timer after a 2-second delay
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chairs"))
        {
            // Customer is leaving the chair
            isSitting = false;
            timer_radial_image.gameObject.SetActive(false); // Deactivate the image when the customer is leaving the chair
        }
    }



    public void StartChase()
    {
        isChasing = true;
        jumpscare = true;
    }



    private void DestroyCustomer()
    {
        // Perform any necessary clean-up tasks;
        CurrencyManager.Instance.AddMoney(moneyDrop);
        Destroy(gameObject);
    }

    private IEnumerator LeaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyCustomer();
    }

}
