using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    Animator animator;

    public GameObject jumpScare;

    private bool hasSpawnedImage = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        jumpScare.SetActive(false);
    }

    public void SpawnImage()
    {
        if (!hasSpawnedImage)
        {
            jumpScare.SetActive(true);

            animator.Play("JumpScare");

            GameObject spawnedImage = Instantiate(jumpScare, transform.position, Quaternion.identity);
            AudioManager.Instance.Play("Jumpscare");

            StartCoroutine(DestroyImageAfterDelay(spawnedImage, imageDuration));

            hasSpawnedImage = true;
            ResetSpawnedFlag();
        }
    }

    public void ResetSpawnedFlag()
    {
        hasSpawnedImage = false;
    }

    public float imageDuration = 5f; // Time in seconds before the spawned tile disappears

    private IEnumerator DestroyImageAfterDelay(GameObject image, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(image);
    }
}
