using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDamage : MonoBehaviour
{
    private CustomerSatisfactionTimer CustomerSatisfactionTimer;
    //private Timer timer;

    public float damageAmount = 3f;
    //private float originalDamageAmount;

    private void Start()
    {
        CustomerSatisfactionTimer = GetComponent<CustomerSatisfactionTimer>();
        //originalDamageAmount = damageAmount;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chairs") && CustomerSatisfactionTimer.time_remaining <= 0)
        {
            HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                // Apply damage to the player's health bar
                healthBar.TakeDamage(damageAmount);
            }

            // Destroy the enemy object
            Destroy(gameObject);
        }

    }


}
