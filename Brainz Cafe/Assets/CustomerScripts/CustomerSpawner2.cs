using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner2 : MonoBehaviour
{
    public GameObject customerPair1; // Prefab for the first pair with 1 customer
    public GameObject customerPair2; // Prefab for the second pair with 2 customers

    public Transform spawnPosition; // Reference to the empty GameObject for the spawn position


    private void Start()
    {
        SpawnCustomers();
    }

    public void SpawnCustomers()
    {
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
