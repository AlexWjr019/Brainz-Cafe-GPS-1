using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq.Expressions;

public class HealthBar : MonoBehaviour
{
    [HideInInspector]
    SpriteRenderer sr;

    [HideInInspector]
    BuyItem bi;

    public Image healthBarFill;
    public float currentHealth;
    public float maxHealth = 100f;
    public float fillSpeed = 1f; // Adjust the fill speed as desired

    private Coroutine fillCoroutine; // Store a reference to the fill coroutine

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
        switch (bi.upgradedTable)
        {
            case -1:
                switch (currentHealth)
                {
                    case 60:
                        sr.sprite = phasesDefault[0];
                        break;
                    case 20:
                        sr.sprite = phasesDefault[1];
                        break;
                }
                break;
            case 0:
                switch (currentHealth)
                {
                    case 150:
                        sr.sprite = phasesVer2[0];
                        break;
                    case 50:
                        sr.sprite = phasesVer2[1];
                        break;
                }
                break;
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
        }
    }
}
