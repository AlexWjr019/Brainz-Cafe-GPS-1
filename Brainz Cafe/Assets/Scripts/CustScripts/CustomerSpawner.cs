using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public Transform spawnpoint; // Spawnpoints
    public GameObject[] cusPrefabs; // Customer list

    public Vector2 spawnVal;

    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public float startWait;

    public bool stop;

    public Timer t;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop || !t.timerOn)
        {
            int randCus = Random.Range(0, cusPrefabs.Length); // Randomize the zombie type
            int randCusGrp = Random.Range(1, 3); // Randomize the grp size

            if (randCusGrp == 1)
            {
                Instantiate(cusPrefabs[randCus], spawnpoint.position, Quaternion.identity);
            }
            else if (randCusGrp == 2)
            {
                Instantiate(cusPrefabs[randCus], spawnpoint.position, Quaternion.identity);
                Instantiate(cusPrefabs[randCus], spawnpoint.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
