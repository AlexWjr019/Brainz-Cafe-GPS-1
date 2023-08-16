using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPair1; // Prefab for the first pair with 1 customer
    public GameObject customerPair2; // Prefab for the second pair with 2 customers
    public GameObject customerPair3;
    public GameObject customerPair4;
    public Transform spawnPosition; // Reference to the empty GameObject for the spawn position

    public float checkInterval = 4f; // Interval in seconds to check for customer presence
    public float nightModeInterval;

    [HideInInspector]
    public bool isSpawningAllowed = true;

    private void OnEnable()
    {
        StartCoroutine(SpawnCustomersRoutine(checkInterval));
    }

    private void OnDisable()
    {
        isSpawningAllowed = false;
    }

    private IEnumerator SpawnCustomersRoutine(float checkInterval)
    {
        while (true)
        {
            if (isSpawningAllowed)
            {
                yield return new WaitForSeconds(checkInterval);

                if (!IsStartPointOccupied())
                {
                    SpawnCustomers();
                }
            }
            yield return null;
        }
    }

    private bool IsStartPointOccupied()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(spawnPosition.position);
        foreach (var collider in colliders)
        {
            // If any collider found at the spawn position that is not the spawner itself, return true
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }
        // No colliders found at the spawn position
        return false;
    }

    public void SpawnCustomers()
    {
        int randomPair = Random.Range(1, 100); // Generate a random number between 1 and 2

        if (randomPair <= 70)
        {
            Instantiate(customerPair1, spawnPosition.position, Quaternion.identity);
            AudioManager.Instance.NormZombSpawn();
        }
        else if (randomPair >= 71 && randomPair <= 80)
        {
            Instantiate(customerPair2, spawnPosition.position, Quaternion.identity);
            AudioManager.Instance.Play("AcidSpawn");
        }
        else if (randomPair >= 81 && randomPair <= 90)
        {
            Instantiate(customerPair3, spawnPosition.position, Quaternion.identity);
            AudioManager.Instance.Play("BruteSpawn");
        }
        else if (randomPair >= 91 && randomPair <= 100)
        {
            Instantiate(customerPair4, spawnPosition.position, Quaternion.identity);
            AudioManager.Instance.Play("ClownSpawn");
        }
    }
}
