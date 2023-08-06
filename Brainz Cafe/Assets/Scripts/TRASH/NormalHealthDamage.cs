//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NormalHealthDamage : MonoBehaviour
//{
//    private NormalCustomerSactisfactionTimer CustomerSatisfactionTimer;
//    //private Timer timer;

//    public float damageAmount = 3f;
//    //private float originalDamageAmount;

//    private void Start()
//    {
//        CustomerSatisfactionTimer = GetComponent<NormalCustomerSactisfactionTimer>();
//        //originalDamageAmount = damageAmount;

//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Chairs") && CustomerSatisfactionTimer.time_remaining <= 0)
//        {
//            Counter healthBar = collision.gameObject.GetComponent<Counter>();
//            if (healthBar != null)
//            {
//                // Apply damage to the player's health bar
//                healthBar.TakeDamage(damageAmount);
//            }

//            // Destroy the enemy object
//            Destroy(gameObject);
//        }

//    }
//}
