using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public Transform[] spawnPositions;
    //public float spawnInterval = 1f;
    public bool canSpawnFood = true;
    private bool delayStarted = false;
    private float delayTimer = 0.0f;
    public float spawnDelay = 3.0f;
    private int spawnIndex = 0;
    private Queue<int> customerFoodIndices = new Queue<int>(); // Indices of the food shown to the customers

    private void Update()
    {
        //delayTimer += Time.deltaTime;

        //if (delayStarted && delayTimer >= spawnDelay)
        //{
        //    if (delayTimer >= spawnInterval)
        //    {
        //        SpawnFood();
        //        delayTimer = 0.0f; // Reset the delay timer
        //    }
        //}
        if (canSpawnFood && customerFoodIndices.Count > 0)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= spawnDelay)
            {
                SpawnFood();
                delayTimer = 0.0f; // Reset the delay timer
            }
        }
    }

    public void SpawnFood()
    {
        if (foodPrefabs.Length == 0)
        {
            Debug.LogWarning("No food prefabs assigned to the spawner.");
            return;
        }

        if (spawnPositions.Length == 0)
        {
            Debug.LogWarning("No spawn positions assigned to the spawner.");
            return;
        }

        if (customerFoodIndices.Count == 0)
        {
            Debug.LogWarning("No customer food indices available to spawn.");
            return;
        }

        int spawnPositionIndex = GetNextAvailableSpawnPosition();
        if (spawnPositionIndex == -1)
        {
            Debug.LogWarning("All spawn positions are currently occupied.");
            return; // Exit the method if no available spawn positions
        }

        int foodIndex = customerFoodIndices.Dequeue(); // Dequeue the first customer food index

        if (foodIndex < 0 || foodIndex >= foodPrefabs.Length)
        {
            Debug.LogWarning("Invalid food index: " + foodIndex);
            return; // Exit the method if the food index is invalid
        }

        GameObject foodPrefab = foodPrefabs[foodIndex];
        Transform spawnPosition = spawnPositions[spawnPositionIndex];
        GameObject foodInstance = Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

        // Remove the "(clone)" suffix from the instantiated food object's name
        foodInstance.name = RemoveCloneSuffix(foodInstance.name);

        //canSpawnFood = false;
        //delayStarted = false; // Reset the delay flag
        delayTimer = 0.0f; // Reset the delay timer

        // Check if there are more customer food indices to spawn
        if (customerFoodIndices.Count > 0)
        {
            delayStarted = true; // Start the delay for the next food spawn
        }
    }

    private int GetNextAvailableSpawnPosition()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int nextIndex = (spawnIndex + i) % spawnPositions.Length;
            if (!CheckForExistingObject(spawnPositions[nextIndex].position))
            {
                return nextIndex;
            }
        }

        return -1; // No available spawn positions
    }

    private bool CheckForExistingObject(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to a food object or any other object you want to exclude
            if (collider.CompareTag("Food"))
            {
                return true; // An object already exists at the position
            }
        }

        return false; // No object found at the position
    }

    public void AddCustomerFoodIndex(int index)
    {
        customerFoodIndices.Enqueue(index); // Enqueue the customer food index
        if (!delayStarted)
        {
            delayStarted = true; // Start the delay when the first customer food index is added
            delayTimer = 0.0f; // Reset the delay timer
        }
        else
        {
            delayTimer = 0.0f; // Reset the delay timer for subsequent customer food indices
        }
    }

    private string RemoveCloneSuffix(string name)
    {
        // Check if the name contains the "(clone)" suffix
        int cloneIndex = name.IndexOf("(Clone)");

        if (cloneIndex != -1)
        {
            // Remove the "(clone)" suffix from the name
            name = name.Substring(0, cloneIndex);
        }

        return name;
    }
}
