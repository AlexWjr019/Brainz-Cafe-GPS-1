using System.Collections;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    public CustomerSpawner2 customerSpawner;

    public float customerBoostDuration = 20f;
    private float customerBoost = 4f;


    private void Start()
    {
        customerSpawner = FindObjectOfType<CustomerSpawner2>();
        
    }

    public void TriggerRushHourEvent()
    {
        CustomerBoostEvent();
    }

    public void CustomerBoostEvent()
    {
        StartCoroutine(BoostEventRoutine());
    }

    private IEnumerator BoostEventRoutine()
    {
        // Boost customer spawn time
        float originalCustomerSpawn = customerSpawner.checkInterval;
        customerSpawner.checkInterval = customerBoost;

        yield return new WaitForSeconds(customerBoostDuration);

        // Restore customer spawn time
        customerSpawner.checkInterval = originalCustomerSpawn;

    }
}
