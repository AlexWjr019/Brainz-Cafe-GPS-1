using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public Transform[] spawnpoint;
    public GameObject[] cusPrefabs;

    public Vector2 spawnVal;

    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public float startWait;

    public bool stop;

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

        while (!stop)
        {
            int randCus = Random.Range(0, cusPrefabs.Length);

            Instantiate(cusPrefabs[randCus], spawnpoint[0].position, transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
