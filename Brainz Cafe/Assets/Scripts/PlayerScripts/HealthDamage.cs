using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthDamage : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public int sceneID;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
    //    if (Input.GetKeyUp(KeyCode.V))
    //    {
    //        TakeDamage(20);
    //    }

    }

    public void TakeDamage(int damege)
    {
        currentHealth -= damege;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the player GameObject or perform other actions
            SceneManager.LoadScene(sceneID);
        }
    }

}
