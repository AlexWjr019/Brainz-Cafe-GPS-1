using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static bool isAfter5PM = true;
    private float timeSinceLastEvent = 0f;
    private float eventInterval = 30f; // 30 minutes
    private float eventChance = 0.5f; // 50% chance

    //private float currentHour;

    // References to the three event scripts
    public OneLastPush oneLastPush;
    public RushHour rushHour;
    public AcidGangEvent acidGangEvent;

    private List<System.Action> eventsList = new List<System.Action>();

    private void Start()
    {
        // Populate the list with the event functions
        PopulateEventsList();

        // Randomly shuffle the events list
        Shuffle(eventsList);
    }

    private void PopulateEventsList()
    {
        eventsList.Clear();
        eventsList.Add(oneLastPush.TriggerOneLastPushEvent);
        eventsList.Add(rushHour.TriggerRushHourEvent);
        eventsList.Add(acidGangEvent.TriggerAcidGang);
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void Update()
    {
        //CheckTime();

        if (isAfter5PM == true)
        {
            //Debug.Log("Start Event");
            timeSinceLastEvent += Time.deltaTime;

            if (timeSinceLastEvent >= eventInterval)
            {
                timeSinceLastEvent = 0f;

                if (Random.value <= eventChance)
                {
                    Debug.Log("Event Triggered");
                    ActivateRandomEvent();
                }
                else
                {
                    Debug.Log("No Event");
                }
            }
        }
    }

    private void ActivateRandomEvent()
    {
        Debug.Log(eventsList[0] + "Event");

        // If all events have been activated, reset the list and shuffle again
        if (eventsList.Count == 0)
        {
            PopulateEventsList();
            Shuffle(eventsList);
        }

        // Get the first event in the shuffled list
        System.Action eventFunction = eventsList[0];

        // Call the event function
        eventFunction();

        // Remove the event from the list so it won't be chosen again immediately
        eventsList.RemoveAt(0);

        // Shuffle the list again to randomize the order for the next activation
        Shuffle(eventsList);
    }

    //private void CheckTime()
    //{
    //    // Get the current time in 24-hour format
    //    currentHour += Time.deltaTime;

    //    float min = Mathf.FloorToInt(currentHour / 60);

    //    // Check if it's after 6 PM (18:00)
    //    isAfter5PM = min >= 9;
    //}

}
