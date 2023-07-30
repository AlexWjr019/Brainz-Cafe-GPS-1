using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using System;

public class Timer : MonoBehaviour
{
    public bool timerOn = false;

    [SerializeField]
    private bool countDown = true;

    [SerializeField]
    private float timerDuration = 0;
    [SerializeField]
    private float timerStart = 0;
    private float timer;

    public int sceneID;

    public TMP_Text timerTxt;

    public GameObject customerSpawnObject;

    public bool nightShift = false;

    public GameObject darkMode;
    public GameObject lights;

    public static bool playerBreakTime = false;

    public float fadeDuration = 2.0f; // Time in seconds to fade from 0 to 0.98
    public CanvasGroup canvasGroup;
    private float targetAlpha = 0.98f;
    //private float currentAlpha = 0f;
    //private float ctimer = 0f;
    public TMP_Text textToFade; // Reference to the TextMeshPro text you want to fade
    private Color originalTextColor; // Store the original text color

    // Start is called before the first frame update
    void Start()
    {
        resetTimer();
        nightShift = false;
        canvasGroup = darkMode.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Start with alpha 0

        // Store the original text color
        originalTextColor = textToFade.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (countDown && timer >= 0)
            {
                timer -= Time.deltaTime;
                updateTimerDisplay(timer);
            }
            else if (!countDown && ((timer - timerStart) <= timerDuration))
            {
                timer += Time.deltaTime;
                updateTimerDisplay(timer);
            }
            else
            {
                Debug.Log("Time is UP!");
                timerDuration = 0;
                timerOn = false;
                //SceneManager.LoadScene(sceneID);
            }
        }

        
    }

    private void resetTimer()
    {
        if (countDown)
        {
            timer = timerDuration;
            timerOn = true;
        }
        else
        {
            Debug.Log(timer);
            timer = timerStart;
            timerOn = true;
        }
    }

    private void updateTimerDisplay(float currentTime)
    {
        if (currentTime < 0)
        {
            currentTime = 0;
        }

        float min = Mathf.FloorToInt(currentTime / 60);
        float sec = Mathf.FloorToInt(currentTime % 60);

        if (currentTime <= 720)
        {
            timerTxt.text = string.Format("{0:00} : {1:00} AM", min, sec);
        }
        else
        {
            timerTxt.text = string.Format("{0:00} : {1:00} PM", min, sec);
            if (min == 12 && sec == 0)
            {
                playerBreakTime = true;
                Debug.Log("Player Break Time"); // Display break time message
                // Deactivate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(false);
            }
            if (min == 13 && sec == 0)
            {
                playerBreakTime = false;
                Debug.Log("Player Afternoon shift"); // Display break time message
                // Activate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(true);
            }
            if (min == 17 && sec == 0)
            {
                playerBreakTime = true;
                Debug.Log("Player Break Time"); // Display break time message
                // Deactivate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(false);
            }
            if (min == 18 && sec == 0)
            {
                playerBreakTime = false;
                Debug.Log("Player Night shift"); // Display break time message
                // Activate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(true);
                darkMode.SetActive(true);


                //ctimer += Time.deltaTime;
                StartCoroutine(FadeInCanvas());
                //float t = ctimer / fadeDuration;
                //currentAlpha = Mathf.Lerp(0f, targetAlpha, t);
                //canvasGroup.alpha = currentAlpha;

                //if (t >= 1.0f) // Use 1 instead of 0.98 to ensure reaching the targetAlpha
                //{
                //    canvasGroup.alpha = targetAlpha;
                //}
                //else
                //{
                //    StartCoroutine(ActivateLightDelayed());
                //}

                //StartCoroutine(ActivateLightDelayed());


                //// Activate the lights at 6:00 PM
                //foreach (GameObject lightObject in lights)
                //{
                //    lightObject.SetActive(true);
                //}

                nightShift = true;
                EventManager.isAfter5PM = true;
            }
        }
    }

    private IEnumerator FadeInCanvas()
    {
        float startAlpha = canvasGroup.alpha;
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        // Ensure the alpha value is set to the target value after the loop ends
        canvasGroup.alpha = targetAlpha;

        // Set the text color alpha based on the original alpha, not the canvas alpha
        textToFade.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, originalTextColor.a);

        StartCoroutine(ActivateLightDelayed());
    }

    private IEnumerator ActivateLightDelayed()
    {
        yield return new WaitForSeconds(1f);
        lights.SetActive(true);
    }
}
