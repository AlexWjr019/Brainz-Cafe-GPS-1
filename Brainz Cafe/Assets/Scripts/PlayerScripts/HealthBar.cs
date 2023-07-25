using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;
    public float currentHealth;
    public float maxHealth = 100f;
    public float fillSpeed = 1f; // Adjust the fill speed as desired

    private Coroutine fillCoroutine; // Store a reference to the fill coroutine

    //private Timer timer;
    //private HealthBar healthBar;

    private void Start()
    {
        //timer = GameObject.FindObjectOfType<Timer>();
        //healthBar = GetComponent<HealthBar>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //if (timer.nightShift)
        //{
        //    currentHealth -= damage * 2;
        //}
        //if (!timer.nightShift)
        //{
        //    currentHealth -= damage;
        //}

        Debug.Log(damage);

        // Stop the previous fill coroutine if it is running
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        // Start a new fill coroutine
        fillCoroutine = StartCoroutine(FillCoroutine());

        if (currentHealth <= 0)
        {
            // Perform necessary actions when health reaches zero (e.g., player dies)
            // You can add your own logic here.
            Destroy(gameObject);
        }
    }

    private IEnumerator FillCoroutine()
    {
        float targetFillAmount = currentHealth / 100f;
        float currentFillAmount = healthBarFill.fillAmount;

        // Gradually decrease the fill amount until it reaches the target fill amount
        while (healthBarFill.fillAmount > targetFillAmount)
        {
            currentFillAmount -= fillSpeed * Time.deltaTime;
            healthBarFill.fillAmount = Mathf.Max(currentFillAmount, targetFillAmount);
            yield return null;
        }

        // Reset the fill coroutine reference
        fillCoroutine = null;
    }
}
