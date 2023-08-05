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
    private float timer = 0f; // Timer to track the elapsed time

    private bool isSpawningAllowed = true;

    private void OnEnable()
    {
        isSpawningAllowed = true;
        StartCoroutine(SpawnCustomersRoutine());
    }

    private void OnDisable()
    {
        isSpawningAllowed = false;
    }

    private IEnumerator SpawnCustomersRoutine()
    {
        while (isSpawningAllowed)
        {
            timer += Time.deltaTime;

            if (timer >= checkInterval)
            {
                timer = 0f;

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
        int randomPair = Random.Range(1, 10); // Generate a random number between 1 and 2

        if (randomPair <= 7)
        {
            Instantiate(customerPair1, spawnPosition.position, Quaternion.identity);
            AudioManager.instance.PlayNormalZombieAudio();
        }
        else if (randomPair == 8)
        {
            Instantiate(customerPair2, spawnPosition.position, Quaternion.identity);
            AudioManager.instance.PlayAcidZombieAudio();
        }
        else if (randomPair == 9)
        {
            Instantiate(customerPair3, spawnPosition.position, Quaternion.identity);
            AudioManager.instance.PlayBruteZombieAudio();
        }
        else if (randomPair == 10)
        {
            Instantiate(customerPair4, spawnPosition.position, Quaternion.identity);
            AudioManager.instance.PlayClownZombieAudio();
        }
    }
}
