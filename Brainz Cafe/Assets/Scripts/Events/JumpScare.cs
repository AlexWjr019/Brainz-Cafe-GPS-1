using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    private CustomerSatisfactionTimer SatisfactionTImer1;

    public GameObject jumpScare;
    //public GameObject fadeInOut;

    [SerializeField]
    GameObject darkMode;

    public float fadeDuration = 1.0f; // Time in seconds to fade from 0 to 0.98
    public CanvasGroup canvasGroup;
    private float targetAlpha = 0.98f;
    private bool isDarkMode = false;
    private bool hasSpawnedImage = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am close");
        SatisfactionTImer1 = GetComponent<CustomerSatisfactionTimer>();
        jumpScare.SetActive(false);
        //fadeInOut.SetActive(false);
        canvasGroup = darkMode.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Start with alpha 0
        isDarkMode = false;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if(SatisfactionTImer1.time_remaining <=0)
    //    {
    //        Debug.Log("Here");
    //        jumpScare.SetActive(true);
    //    }
    //}

    public void SpawnImage()
    {
        //jumpScare.SetActive(true);

        //GameObject spawnedImage = Instantiate(jumpScare, transform.position, Quaternion.identity);
        //AudioManager.instance.PlayClownZombieJumpScareAudio();
        //StartCoroutine(DestroyImageAfterDelay(spawnedImage, imageDuration));
        //darkMode.SetActive(true);
        //isDarkMode = true;
        //DarkMode();
        if (!hasSpawnedImage)
        {
            jumpScare.SetActive(true);

            GameObject spawnedImage = Instantiate(jumpScare, transform.position, Quaternion.identity);
            AudioManager.instance.PlayClownZombieJumpScareAudio();
            StartCoroutine(DestroyImageAfterDelay(spawnedImage, imageDuration));
            darkMode.SetActive(true);
            isDarkMode = true;
            DarkMode();

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
        //jumpScare.SetActive(false);
        Destroy(image);

        //darkMode.SetActive(true);
        //isDarkMode = true;
        //DarkMode();

        //fadeInOut.SetActive(true);
        //StartCoroutine(DestroyFadeAfterDelay());
    }

    private void DarkMode()
    {
        if (isDarkMode)
        {
            StartCoroutine(FadeInCanvas(0.2f));
        }
    }


    private IEnumerator FadeInCanvas(float delay)
    {
        yield return new WaitForSeconds(delay);
        //float startAlpha = canvasGroup.alpha;
        //float timePassed = 0f;

        //while (timePassed < fadeDuration)
        //{
        //    timePassed += Time.deltaTime;
        //    float t = timePassed / fadeDuration;
        //    canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
        //    yield return null;
        //}

        // Ensure the alpha value is set to the target value after the loop ends
        canvasGroup.alpha = targetAlpha;

        //StartCoroutine(ActivateLightDelayed());
        StartFadeOut();
    }

    private IEnumerator FadeOutCanvas()
    {
        float startAlpha = canvasGroup.alpha;
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t); // Fade to alpha 0
            yield return null;
        }

        // Ensure the alpha value is set to 0 after the loop ends
        canvasGroup.alpha = 0f;


    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOutCanvas());

    }
    //private IEnumerator DestroyFadeAfterDelay()
    //{
    //    yield return new WaitForSeconds(2f);
    //    //fadeInOut.SetActive(false);
    //}
}
