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
    [HideInInspector]
    public bool timerOn = false;

    [SerializeField]
    private bool countDown = true;

    [SerializeField]
    private float timerDuration = 0;
    [SerializeField]
    private float timerStart = 0;
    private float timer;

    [SerializeField]
    int sceneID;

    [SerializeField]
    TMP_Text timerTxt;

    [SerializeField]
    GameObject customerSpawnObject;

    public bool nightShift = false;

    [SerializeField]
    GameObject darkMode;
    [SerializeField]
    GameObject lights;
    [SerializeField]
    Image clockImage;


    public static bool playerBreakTime = false;

    public float fadeDuration = 2.0f; // Time in seconds to fade from 0 to 0.98
    public CanvasGroup canvasGroup;
    private float targetAlpha = 0.98f;
    private bool isDarkMode = false;


    //[SerializeField] AudioSource daySound;
    //[SerializeField] AudioSource nightSound;

    void Start()
    {
        resetTimer();
        nightShift = false;
        canvasGroup = darkMode.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Start with alpha 0
        isDarkMode = false;

    }

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
                SceneManager.LoadScene(sceneID);
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

            //daySound.Play();

        }
        else
        {
            timerTxt.text = string.Format("{0:00} : {1:00} PM", min, sec);
            if (min == 12 && sec == 0)
            {
                playerBreakTime = true;

                Debug.Log("Player Break Time"); // Display break time message

                customerSpawnObject.SetActive(false);
            }
            if (min == 13 && sec == 0)
            {
                playerBreakTime = false;

                Debug.Log("Player Afternoon shift"); // Display break time message

                customerSpawnObject.SetActive(true);
            }
            if (min == 17 && sec == 0)
            {
                playerBreakTime = true;

                Debug.Log("Player Break Time"); // Display break time message

                customerSpawnObject.SetActive(false);
            }
            if (min == 18 && sec == 0)
            {
                playerBreakTime = false;

                Debug.Log("Player Night shift"); // Display break time message

                //nightSound.Play();
                AudioManager.instance.PlayNightAudio(1);
                

                customerSpawnObject.SetActive(true);
                darkMode.SetActive(true);
                isDarkMode = true;
                DarkMode();
                clockImage.color = Color.gray;

                nightShift = true;
                EventManager.isAfter5PM = true;
            }
        }
    }

    private void DarkMode()
    {
        if(isDarkMode)
        {
            StartCoroutine(FadeInCanvas());
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

        StartCoroutine(ActivateLightDelayed());
    }

    private IEnumerator ActivateLightDelayed()
    {
        yield return new WaitForSeconds(1f);
        lights.SetActive(true);
    }
}
