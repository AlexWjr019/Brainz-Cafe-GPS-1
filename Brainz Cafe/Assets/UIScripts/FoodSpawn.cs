using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public Transform[] spawnPositions;
    public float spawnInterval = 1.0f;
    public bool canSpawnFood = true;
    private bool delayStarted = false;
    private float timer = 0.0f;
    private int spawnIndex = 0;
    public ObjectInteraction objectInteraction;
    int r;

    private void Update()
    {
        timer += Time.deltaTime;

        if (!delayStarted && Input.GetKeyDown(KeyCode.Return))
        {
            timer = 0.0f;
            delayStarted = true;
        }

        if (delayStarted && timer >= 2.0f)
        {
            if (timer >= spawnInterval)
            {
                SpawnFood();
                timer = 0.0f;
            }
        }

        int i = objectInteraction.randomIndex;
        r = i;
    }

    private void SpawnFood()
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

        bool hasObject = CheckForExistingObject(spawnPositions[spawnIndex].position);
        if (hasObject)
        {
            // Skip spawning if an object is already present
            spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position
            return;
        }


        GameObject FoodPrefab = foodPrefabs[r];

        Transform spawnPosition = spawnPositions[spawnIndex];

        Instantiate(foodPrefabs[r], spawnPosition.position, spawnPosition.rotation);

        spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

        canSpawnFood = false;
        delayStarted = false;


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


}
