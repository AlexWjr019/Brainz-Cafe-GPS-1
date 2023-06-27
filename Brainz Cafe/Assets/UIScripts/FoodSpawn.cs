//using UnityEngine;

//public class FoodSpawn : MonoBehaviour
//{
//    public GameObject[] foodPrefabs;
//    public Transform[] spawnPositions;
//    public float spawnInterval = 1.0f;
//    public bool canSpawnFood = true;
//    private bool delayStarted = false;
//    private float timer = 0.0f;
//    private int spawnIndex = 0;
//    public ObjectInteraction objectInteraction;
//    int r;

//    private void Update()
//    {
//        timer += Time.deltaTime;

//        if (!delayStarted && Input.GetKeyDown(KeyCode.Return))
//        {
//            timer = 0.0f;
//            delayStarted = true;
//        }

//        if (delayStarted && timer >= 5.0f)
//        {
//            if (timer >= spawnInterval)
//            {
//                SpawnFood();
//                timer = 0.0f;
//            }
//        }

//        int i = objectInteraction.randomIndex;
//        r = i;

//    }

//    private void SpawnFood()
//    {
//        if (foodPrefabs.Length == 0)
//        {
//            Debug.LogWarning("No food prefabs assigned to the spawner.");
//            return;
//        }

//        if (spawnPositions.Length == 0)
//        {
//            Debug.LogWarning("No spawn positions assigned to the spawner.");
//            return;
//        }

//        bool hasObject = CheckForExistingObject(spawnPositions[spawnIndex].position);
//        if (hasObject)
//        {
//            // Skip spawning if an object is already present
//            spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position
//            return;
//        }


//        GameObject FoodPrefab = foodPrefabs[r];

//        Transform spawnPosition = spawnPositions[spawnIndex];

//        Instantiate(foodPrefabs[r], spawnPosition.position, spawnPosition.rotation);

//        spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

//        canSpawnFood = false;
//        delayStarted = false;


//    }

//    private bool CheckForExistingObject(Vector3 position)
//    {
//        Collider2D[] colliders = Physics2D.OverlapPointAll(position);
//        foreach (Collider2D collider in colliders)
//        {
//            // Check if the collider belongs to a food object or any other object you want to exclude
//            if (collider.CompareTag("Food"))
//            {
//                return true; // An object already exists at the position
//            }
//        }

//        return false; // No object found at the position
//    }


//}


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
    //public int r;

    private void Start()
    {
        //objectInteraction = GetComponent<ObjectInteraction>();
        objectInteraction = FindObjectOfType<ObjectInteraction>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!delayStarted && Input.GetKeyDown(KeyCode.Return))
        {
            timer = 0.0f;
            delayStarted = true;
        }

        if (delayStarted && timer >= 5.0f)
        {
            if (timer >= spawnInterval)
            {
                SpawnFood();
                timer = 0.0f;
            }
        }

        //int i = objectInteraction.randomIndex;
        //r = i;

    }


    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    bool hasObject = CheckForExistingObject(spawnPositions[r].position);
    //    if (hasObject)
    //    {
    //        // Skip spawning if an object is already present
    //        r = (r + 1) % spawnPositions.Length; // Move to the next spawn position
    //        return;
    //    }


    //    //GameObject foodPrefab = foodPrefabs[r];

    //    Transform spawnPosition = spawnPositions[r];

    //    Instantiate(foodPrefabs[r], spawnPosition.position, spawnPosition.rotation);

    //    r = (r + 1) % spawnPositions.Length; // Move to the next spawn position

    //    canSpawnFood = false;
    //    delayStarted = false;


    //}
    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    bool hasObject = CheckForExistingObject(spawnPositions[objectInteraction.randomIndex].position);
    //    if (hasObject)
    //    {
    //        // Skip spawning if an object is already present
    //        //r = (r + 1) % spawnPositions.Length; // Move to the next spawn position
    //        return;
    //    }

    //    //GameObject foodPrefab = foodPrefabs[r];
    //    //Transform spawnPosition = spawnPositions[r];
    //    //Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //    //r = (r + 1) % spawnPositions.Length; // Move to the next spawn position

    //    GameObject foodPrefab = foodPrefabs[objectInteraction.randomIndex];
    //    Transform spawnPosition = spawnPositions[objectInteraction.randomIndex];
    //    Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //    objectInteraction.randomIndex = (objectInteraction.randomIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //    canSpawnFood = false;
    //    delayStarted = false;
    //}

    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    bool hasObject = CheckForExistingObject(spawnPositions[spawnIndex].position);
    //    if (hasObject)
    //    {
    //        // Skip spawning if an object is already present
    //        spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position
    //        return;
    //    }

    //    GameObject foodPrefab = foodPrefabs[spawnIndex];
    //    Transform spawnPosition = spawnPositions[spawnIndex];
    //    Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //    spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //    canSpawnFood = false;
    //    delayStarted = false;
    //}

    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    bool hasObject = CheckForExistingObject(spawnPositions[spawnIndex].position);
    //    if (hasObject)
    //    {
    //        // Skip spawning if an object is already present
    //        spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position
    //        return;
    //    }

    //    GameObject foodPrefab = foodPrefabs[objectInteraction.randomIndex]; // Use the randomIndex from ObjectInteraction
    //    Transform spawnPosition = spawnPositions[spawnIndex];
    //    Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //    spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //    canSpawnFood = false;
    //    delayStarted = false;
    //}


    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    int startingIndex = spawnIndex;
    //    int currentIndex = spawnIndex;

    //    do
    //    {
    //        bool hasObject = CheckForExistingObject(spawnPositions[currentIndex].position);
    //        if (!hasObject)
    //        {
    //            GameObject foodPrefab = foodPrefabs[currentIndex];
    //            Transform spawnPosition = spawnPositions[currentIndex];
    //            Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //            spawnIndex = (currentIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //            canSpawnFood = false;
    //            delayStarted = false;

    //            return;
    //        }

    //        currentIndex = (currentIndex + 1) % spawnPositions.Length;
    //    } while (currentIndex != startingIndex);

    //    Debug.LogWarning("All spawn positions are occupied. Cannot spawn food.");
    //}

    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    bool hasObject = CheckForExistingObject(spawnPositions[spawnIndex].position);
    //    if (hasObject)
    //    {
    //        // Skip spawning if an object is already present
    //        return;
    //    }

    //    GameObject foodPrefab = foodPrefabs[spawnIndex];
    //    Transform spawnPosition = spawnPositions[spawnIndex];
    //    Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //    spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //    canSpawnFood = false;
    //    delayStarted = false;
    //}

    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    int initialSpawnIndex = spawnIndex; // Store the initial spawn index

    //    Loop through the spawn positions to find an empty position to spawn the food
    //    while (true)
    //    {
    //        bool hasObject = CheckForExistingObject(spawnPositions[spawnIndex].position);
    //        if (!hasObject)
    //        {
    //            Found an empty position, spawn the food
    //            GameObject foodPrefab = foodPrefabs[spawnIndex];
    //            Transform spawnPosition = spawnPositions[spawnIndex];
    //            Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

    //            spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //            canSpawnFood = false;
    //            delayStarted = false;
    //            return;
    //        }

    //        spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // Move to the next spawn position

    //        If we have looped through all positions and none are empty, exit the loop
    //        if (spawnIndex == initialSpawnIndex)
    //        {
    //            Debug.LogWarning("All spawn positions are occupied.");
    //            return;
    //        }
    //    }
    //}

    //private void SpawnFood()
    //{
    //    if (foodPrefabs.Length == 0)
    //    {
    //        Debug.LogWarning("No food prefabs assigned to the spawner.");
    //        return;
    //    }

    //    if (spawnPositions.Length == 0)
    //    {
    //        Debug.LogWarning("No spawn positions assigned to the spawner.");
    //        return;
    //    }

    //    for (int i = 0; i < spawnPositions.Length; i++)
    //    {
    //        bool hasObject = CheckForExistingObject(spawnPositions[i].position);
    //        if (!hasObject)
    //        {
    //            // Spawn the food at the first empty position
    //            GameObject foodPrefab = foodPrefabs[spawnIndex];
    //            Transform spawnPosition = spawnPositions[i];
    //            Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);
    //            return;
    //        }
    //    }

    //    Debug.LogWarning("All spawn positions are occupied.");
    //}

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
            // Find the next available position
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                int nextIndex = (spawnIndex + i) % spawnPositions.Length;
                if (!CheckForExistingObject(spawnPositions[nextIndex].position))
                {
                    spawnIndex = nextIndex;
                    break;
                }
            }
        }

        GameObject foodPrefab = foodPrefabs[spawnIndex];
        Transform spawnPosition = spawnPositions[spawnIndex];
        Instantiate(foodPrefab, spawnPosition.position, spawnPosition.rotation);

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

    //public void SetRandomIndex(int index)
    //{
    //    r = index;
    //}

    //public void SetRandomIndex(int index)
    //{
    //    spawnIndex = index;
    //}

    public void SetRandomIndex(int index)
    {
        spawnIndex = index;
        objectInteraction.randomIndex = index;
    }


}

