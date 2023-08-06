using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public bool isOccupied;

    [HideInInspector]
    public SpriteRenderer sr;

    [HideInInspector]
    BuyItem bi;

    public float currentHealth;
    public float maxHealth = 100f;

    public Sprite[] phasesDefault;
    public Sprite[] phasesVer2;

    private void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        bi = FindObjectOfType<BuyItem>();
    }

    private void Update()
    {
        if (bi.upgradedTable == -1)
        {
            if (currentHealth == 60)
            {
                sr.sprite = phasesDefault[0];
            }
            if (currentHealth == 20)
            {
                sr.sprite = phasesDefault[1];
            }
        }
        else if (bi.upgradedTable == 0)
        {
            if (currentHealth == 150)
            {
                sr.sprite = phasesVer2[0];
            }
            if (currentHealth == 50)
            {
                sr.sprite = phasesVer2[1];
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log(damage);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlayBarrierDestroyAudio();
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Repair()
    {
        currentHealth = maxHealth;
    }
}
