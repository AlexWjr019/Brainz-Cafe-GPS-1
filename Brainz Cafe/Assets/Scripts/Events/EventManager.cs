using UnityEngine;

public class EventManager : MonoBehaviour
{
    //public static bool isAfter5PM = true;
    private float timeSinceLastEvent = 0f;
    private float eventInterval = 30f; // 30 minutes
    private float eventChance = 0.5f; // 50% chance

    private float currentHour;


    private void Update()
    {
        //CheckTime();

        //if (isAfter5PM == true)
        //{
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
        //}
    }

    //private void CheckTime()
    //{
    //    // Get the current time in 24-hour format
    //    currentHour += Time.deltaTime;

    //    float min = Mathf.FloorToInt(currentHour / 60);

    //    // Check if it's after 6 PM (18:00)
    //    isAfter5PM = min >= 9;
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


        // Get the DownwardMovement component from the same game object
        DownwardMovement downwardMovement = GetComponent<DownwardMovement>();

        // Check if the DownwardMovement component exists
        if (downwardMovement != null)
        {
            // Move the object downward using the DownwardMovement script
            downwardMovement.StartDownwardMovement();
        }

        // Trigger the Acid Gang event in the AcidGang script
        AcidGangEvent acidGang = GetComponent<AcidGangEvent>();
        if (acidGang != null)
        {
            acidGang.TriggerAcidGang();
        }
        if(acidGang == null)
        {
            Debug.Log("Acid Gang Event got problem");
        }
    }

    private void TriggerOneLastPushEvent()
    {
        // Logic for Special Event 2
        Debug.Log("Special Event 2 triggered!");

        // Get the DownwardMovement component from the same game object
        OLPDownwardMovement OLPDownwardMovement = GetComponent<OLPDownwardMovement>();

        // Check if the DownwardMovement component exists
        if (OLPDownwardMovement != null)
        {
            // Move the object downward using the DownwardMovement script
            OLPDownwardMovement.StartOLPDownwardMovement();
        }

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

        // Get the DownwardMovement component from the same game object
        RHDownwardMovement RHDownwardMovement = GetComponent<RHDownwardMovement>();

        // Check if the DownwardMovement component exists
        if (RHDownwardMovement != null)
        {
            // Move the object downward using the DownwardMovement script
            RHDownwardMovement.StartRHDownwardMovement();
        }


        // Trigger the One Last Push event in the OneLastPush script
        RushHour rushHour = GetComponent<RushHour>();
        if (rushHour != null)
        {
            rushHour.TriggerRushHourEvent();
        }
    }
}
