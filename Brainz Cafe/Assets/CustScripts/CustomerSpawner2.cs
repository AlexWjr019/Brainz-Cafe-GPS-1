using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner2 : MonoBehaviour
{
    public GameObject customerPair1; // Prefab for the first pair with 1 customer
    public GameObject customerPair2; // Prefab for the second pair with 2 customers
    public Transform spawnPosition; // Reference to the empty GameObject for the spawn position

    private float checkInterval = 10f; // Interval in seconds to check for customer presence
    private float timer = 0f; // Timer to track the elapsed time

    private void Start()
    {
        StartCoroutine(SpawnCustomersRoutine());
    }

    private IEnumerator SpawnCustomersRoutine()
    {
        while (true)
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
        Instantiate(customerPair1, spawnPosition.position, Quaternion.identity);

        int randomPair = Random.Range(1, 3); // Generate a random number between 1 and 2

        if (randomPair == 1)
        {
            Instantiate(customerPair1, spawnPosition.position, Quaternion.identity);
        }
        else if (randomPair == 2)
        {
            Instantiate(customerPair2, spawnPosition.position, Quaternion.identity);
            Instantiate(customerPair2, spawnPosition.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
        }
    }
}
