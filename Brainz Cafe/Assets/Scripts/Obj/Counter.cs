using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public bool isOccupied;

    //[HideInInspector]
    public SpriteRenderer sr;

    public BuyItem bi;

    public float currentHealth;
    public float maxHealth = 100f;

    public Sprite[] phasesDefault;
    public Sprite[] phasesVer2;

    float poisonTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (bi.upgradedTable == -1)
        {
            if (currentHealth <= 100 && currentHealth >= 61)
            {
                sr.sprite = phasesDefault[0];
            }
            if (currentHealth <= 60f && currentHealth >= 21f)
            {
                sr.sprite = phasesDefault[1];
            }
            if (currentHealth <= 20)
            {
                sr.sprite = phasesDefault[2];
            }
        }
        else if (bi.upgradedTable == 0)
        {
            if (currentHealth <= 250 && currentHealth >= 151)
            {
                sr.sprite = bi.upgrades[bi.upgradedTable];
            }
            if (currentHealth <= 150 && currentHealth >= 51f)
            {
                sr.sprite = phasesVer2[1];
            }
            if (currentHealth <= 50)
            {
                sr.sprite = phasesVer2[2];
            }
        }

        if (bi.poisoned && poisonTimer < bi.poisonDuration)
        {
            poisonTimer += Time.deltaTime;
        }
        else
        {
            poisonTimer = 0;
            bi.poisoned = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (bi.poisoned)
        {
            currentHealth -= damage / 2;
        }
        else
        {
            currentHealth -= damage;
        }

        Debug.Log(damage);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlayBarrierDestroyAudio();
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void Repair()
    {
        currentHealth = maxHealth;
    }
}
