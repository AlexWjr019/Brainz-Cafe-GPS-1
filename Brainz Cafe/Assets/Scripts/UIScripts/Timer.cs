using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

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

    // Start is called before the first frame update
    void Start()
    {
        resetTimer();
        nightShift = false;

        
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
                Debug.Log("Player Break Time"); // Display break time message
                // Deactivate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(false);
            }
            if (min == 13 && sec == 0)
            {
                Debug.Log("Player Afternoon shift"); // Display break time message
                // Activate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(true);
            }
            if (min == 17 && sec == 0)
            {
                Debug.Log("Player Break Time"); // Display break time message
                // Deactivate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(false);
            }
            if (min == 18 && sec == 0)
            {
                Debug.Log("Player Night shift"); // Display break time message
                // Activate the customer spawn game object at 12:00 PM
                customerSpawnObject.SetActive(true);
                nightShift = true;
                EventManager.isAfter5PM = true;
            }
        }
    }
}
