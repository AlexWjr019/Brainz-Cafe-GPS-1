using System.Collections;
using UnityEngine;

public class OneLastPush : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public FoodSpawn foodSpawn;

    public float playerBoostDuration = 30f;
    private float playerBoost = 7.5f;
    private float foodSpawnSpeedIncrease = 2.1f;


    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        foodSpawn = FindObjectOfType<FoodSpawn>();
       
    }

    public void TriggerOneLastPushEvent()
    {
        StartBoostEvent();
    }

    public void StartBoostEvent()
    {
        StartCoroutine(BoostEventRoutine());
    }

    private IEnumerator BoostEventRoutine()
    {
        // Boost player movement speed
        float originalWalkSpeed = playerMovement.walkSpeed;
        playerMovement.walkSpeed = playerBoost;

        // Increase food spawn speed
        float originalSpawnDelay = foodSpawn.spawnDelay;
        foodSpawn.spawnDelay = foodSpawnSpeedIncrease;

        yield return new WaitForSeconds(playerBoostDuration);

        // Restore player movement speed
        playerMovement.walkSpeed = originalWalkSpeed;

        // Restore food spawn speed
        foodSpawn.spawnDelay = originalSpawnDelay;
    }
}
