using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    Animator animator;

    public GameObject jumpScareImage;

    private bool hasSpawnedImage = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        jumpScareImage.SetActive(false);
    }

    public void SpawnImage()
    {
        if (!hasSpawnedImage)
        {
            jumpScareImage.SetActive(true);

            GameObject spawnedImage = Instantiate(jumpScareImage, transform.position, Quaternion.identity);

            AudioManager.Instance.Play("Jumpscare");

            StartCoroutine(DestroyImageAfterDelay(spawnedImage, imageDuration));

            hasSpawnedImage = true;
            ResetSpawnedFlag();

            animator.Play("JumpScare");
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
