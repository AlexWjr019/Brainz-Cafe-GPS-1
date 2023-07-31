using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static bool isAfter5PM = false;
    private float timeSinceLastEvent = 0f;
    private float eventInterval = 30f; // 30 minutes
    private float eventChance = 0.5f; // 50% chance
    

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
                    TriggerRandomSpecialEvent();
                }
                else
                {
                    Debug.Log("No Event");
                }
            }
        }
    }

    //private void CheckTime()
    //{
    //    // Get the current time in 24-hour format
    //    int currentHour = System.DateTime.Now.Hour;

    //    // Check if it's after 6 PM (18:00)
    //    isAfter5PM = currentHour >= 18;
    //}

    private void TriggerRandomSpecialEvent()
    {
        int eventId = Random.Range(0, 3); // Generate a random event ID from 0 to 2 (inclusive)
        switch (eventId)
        {
            case 0:
                TriggerAcidGangEvent();
                break;
            case 1:
                TriggerOneLastPushEvent();
                break;
            case 2:
                TriggerRushHourEvent();
                break;
        }
    }

    private void TriggerAcidGangEvent()
    {
        // Logic for Special Event 1
        Debug.Log("Special Event 1 triggered!");

        // Trigger the Acid Gang event in the AcidGang script
        AcidGang acidGang = GetComponent<AcidGang>();
        if (acidGang != null)
        {
            acidGang.TriggerAcidGang();
        }
    }

    private void TriggerOneLastPushEvent()
    {
        // Logic for Special Event 2
        Debug.Log("Special Event 2 triggered!");

        // Trigger the One Last Push event in the OneLastPush script
        OneLastPush oneLastPush = GetComponent<OneLastPush>();
        if (oneLastPush != null)
        {
            oneLastPush.TriggerOneLastPushEvent();
        }
    }

    private void TriggerRushHourEvent()
    {
        // Logic for Special Event 3
        Debug.Log("Special Event 3 triggered!");

        // Trigger the One Last Push event in the OneLastPush script
        RushHour rushHour = GetComponent<RushHour>();
        if (rushHour != null)
        {
            rushHour.TriggerRushHourEvent();
        }
    }
}
