using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDamage : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.V))
    //    {
    //        TakeDamage(20);
    //    }
    //}

    public void TakeDamage(int damege)
    {
        currentHealth -= damege;

        healthBar.SetHealth(currentHealth);
    }

}
