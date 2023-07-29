using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidGang : MonoBehaviour
{
    public GameObject acidZombiePrefab;
    public Transform[] spawnPositions;
    public int minSpawnCount = 3;
    public int maxSpawnCount = 4;

    
    public void TriggerAcidGang()
    {
        // Generate a random number between the min and max spawn counts
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

        // Ensure we don't spawn more zombies than available positions
        spawnCount = Mathf.Min(spawnCount, spawnPositions.Length);

        // Spawn the acid zombies
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(acidZombiePrefab, spawnPositions[i].position, spawnPositions[i].rotation);
        }
    }

}
