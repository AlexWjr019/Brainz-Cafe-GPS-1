using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform[] spawnPoints; // Array of spawn points where the tiles can appear

    public void SpawnTile()
    {
        if (spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 randomSpawnPoint = spawnPoints[randomIndex].position;
            GameObject spawnedTile = Instantiate(tilePrefab, randomSpawnPoint, Quaternion.identity);
            StartCoroutine(DestroyTileAfterDelay(spawnedTile, tileDuration));
        }
        else
        {
            Debug.LogWarning("No spawn points assigned for the TileSpawner.");
        }
    }

    public float tileDuration = 30f; // Time in seconds before the spawned tile disappears

    private IEnumerator DestroyTileAfterDelay(GameObject tile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tile);
    }
}
