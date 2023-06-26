using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSatisfactionTImer : MonoBehaviour
{
    public Image timer_radial_image;
    public float time_remaining;
    public float max_time = 5.0f;
    private bool damageApplied = false; // Declare the damageApplied variable

    void Start()
    {
        time_remaining = max_time;
    }

    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            timer_radial_image.fillAmount = time_remaining / max_time;
        }

        CheckTimeRemaining();
    }

    void CheckTimeRemaining()
    {
        if (time_remaining <= 0)
        {
            if (!damageApplied)
            {
                HealthDamage health_damage = FindObjectOfType<HealthDamage>();
                if (health_damage != null)
                {
                    health_damage.TakeDamage(20);
                }
                damageApplied = true;
            }
        }
        else
        {
            damageApplied = false;
        }
    }
}


