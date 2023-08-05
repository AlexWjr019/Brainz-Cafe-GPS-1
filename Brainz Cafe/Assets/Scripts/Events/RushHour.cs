using System.Collections;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    public CustomerSpawner customerSpawner;

    public float customerBoostDuration = 20f;
    private float customerBoost = 2f;

    public GameObject rushHourSignboard;


    private void Start()
    {
        customerSpawner = FindObjectOfType<CustomerSpawner>();
        
    }

    public void TriggerRushHourEvent()
    {
        // Move the signboard down when the event is triggered
        rushHourSignboard.GetComponent<RHDownwardMovement>().StartRHDownwardMovement();

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
