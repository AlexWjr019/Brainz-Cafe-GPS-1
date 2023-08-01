using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalCustomerSactisfactionTimer : MonoBehaviour
{
    public Image timer_radial_image;
    public float time_remaining;
    public float max_time = 5.0f;
    private bool isSitting = false; // Flag to track if the customer is sitting

    public bool isAttacking = false; // Flag to track if the customer is currently chasing the player
    //private float chaseDuration = 10f; // Duration of the chase in seconds
    //private float chaseTimer = 0f; // Timer to track the chase duration

    public string playerTag = "Chairs"; // Tag of the player object
    public float moveSpeed = 5f; // Speed at which the customer moves towards the player

    public float damageRate = 1f; // Rate at which damage is applied to the table
    private float damageTimer = 0f; // Timer to track the time since the last damage

    private Transform player; // Reference to the player's transform
    private float destroyTimer = 0f;
    private float destroyDelay = 3f;
    public CustomerInteraction CustomerInteraction;

    private Coroutine damageCoroutine; // Reference to the damage coroutine
    private bool shouldDamageTable = true; // Flag to track if the customer should damage the table

    public int moneyDrop;

    void Start()
    {
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

        CustomerInteraction = GetComponent<CustomerInteraction>();

    }

    void Update()
    {
        if (isSitting)
        {

            if (time_remaining > 0)
            {
                if (CustomerInteraction.foodName == null )
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
                if (!isAttacking)
                {
                    // Customer satisfaction time has run out, start chasing the player
                    StartDamageTable();
                }
            }
        }

        if (isAttacking)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageRate)
            {
                DamageTable();
                damageTimer = 0f;

            }
            if (CustomerInteraction.foodName == null )
            {
                shouldDamageTable = false; // Set the flag to stop damaging the table
                StartCoroutine(LeaveAfterDelay(3f));
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
        if (collision.gameObject.CompareTag("Chairs"))
        {
            isSitting = true;
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

    public void StartDamageTable()
    {
        isAttacking = true;
        damageCoroutine = StartCoroutine(ContinuousDamageTable());
    }

    private void StopDamageTable()
    {
        isAttacking = false;
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    private void DestroyCustomer()
    {
        CurrencyManager.Instance.AddMoney(moneyDrop);
        Destroy(gameObject);
    }

    private void DamageTable()
    {
        if (shouldDamageTable)
        {
            // Find all the table objects with the "Table" tag
            GameObject[] tables = GameObject.FindGameObjectsWithTag("Chairs");

            foreach (GameObject table in tables)
            {
                // Check if the table has a HealthBar component
                HealthBar healthBar = table.GetComponent<HealthBar>();
                if (healthBar != null)
                {
                    // Apply damage to the table's health bar
                    healthBar.TakeDamage(5f);
                }
            }
        }
    }

    private IEnumerator LeaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopDamageTable();
        DestroyCustomer();
    }

    private IEnumerator ContinuousDamageTable()
    {
        while (true)
        {
            DamageTable();
            yield return new WaitForSeconds(damageRate);
        }
    }
}
