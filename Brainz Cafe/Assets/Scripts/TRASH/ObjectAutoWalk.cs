//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectAutoWalk : MonoBehaviour
//{
//    public Transform startPoint; // Reference to the start point
//    public Transform endPoint; // Reference to the end point
//    public float speed = 2f; // Speed at which the object moves

//    private bool isMoving = false; // Flag to track if the object is currently moving
//    private bool hasReachedDestination = false; // Flag to track if the object has reached the destination

//    private float checkInterval = 5f; // Interval in seconds to check for the first point occupancy
//    private float timer = 0f; // Timer to track the elapsed time

//    //private GameObject chairParent; // Reference to the parent object holding the chairs
//    //private List<Transform> chairs = new List<Transform>(); // List to store references to the individual chairs


//    private void Start()
//    {
//        startPoint = GameObject.Find("StartPoint").transform;
//        endPoint = GameObject.Find("EndPoint").transform;
//        //chairParent = GameObject.Find("Chairs"); // Replace "Tables & Chairs" with the actual name of the chair parent GameObject

//        //if (chairParent != null)
//        //{
//        //    // Store references to the individual chairs
//        //    foreach (Transform chair in chairParent.transform)
//        //    {
//        //        chairs.Add(chair);
//        //    }
//        //}
//        //else
//        //{
//        //    Debug.LogError("Chair parent not found!");
//        //}
//    }


//    private void Update()
//    {
//        if (!isMoving && !hasReachedDestination)
//        {
//            timer += Time.deltaTime;

//            if (timer >= checkInterval)
//            {
//                timer = 0f;

//                if (IsEndPointClear())
//                {
//                    StartCoroutine(MoveObjectRoutine());
//                }
//                else
//                {
//                    Debug.Log("End point is occupied. Object will not move.");
//                    hasReachedDestination = true;
//                }
//            }
//        }
//    }

//    private bool IsEndPointClear()
//    {
//        Collider2D[] endColliders = Physics2D.OverlapPointAll(endPoint.position);
//        foreach (var collider in endColliders)
//        {
//            if (collider.gameObject != gameObject)
//            {
//                // End point is occupied by another object
//                return false;
//            }
//        }

//        // End point is clear
//        return true;
//    }

//    private IEnumerator MoveObjectRoutine()
//    {
//        isMoving = true;

//        Vector2 currentPos = startPoint.position;
//        Vector2 targetPos = endPoint.position;

//        float distance = Vector2.Distance(currentPos, targetPos);
//        float totalTime = distance / speed;
//        float currentTime = 0f;

//        while (currentTime < totalTime)
//        {
//            currentTime += Time.deltaTime;
//            float t = currentTime / totalTime;

//            transform.position = Vector2.Lerp(currentPos, targetPos, t);

//            yield return null;
//        }

//        transform.position = targetPos;

//        //    // Find an empty chair
//        //    Transform emptyChair = GetEmptyChair();
//        //    if (emptyChair != null)
//        //    {
//        //        // Move to the empty chair
//        //        Vector2 chairPosition = emptyChair.position;
//        //        while (Vector2.Distance(transform.position, chairPosition) > 0.01f)
//        //        {
//        //            transform.position = Vector2.MoveTowards(transform.position, chairPosition, speed * Time.deltaTime);
//        //            yield return null;
//        //        }

//        //        // Sit down on the chair
//        //        hasReachedDestination = true;
//        //        isMoving = false;

//        //        // TODO: Perform the sitting down animation or logic here
//        //        emptyChair.GetComponent<ChairOccupancy>().OccupyChair(); // Set the chair as occupied
//        //    }
//        //    else
//        //    {
//        //        Debug.Log("No empty chairs available.");
//        //    }
//    }


//    //private Transform GetEmptyChair()
//    //{
//    //    foreach (Transform chair in chairs)
//    //    {
//    //        if (!chair.GetComponent<ChairOccupancy>().IsOccupied())
//    //        {
//    //            return chair;
//    //        }
//    //    }

//    //    return null; // No empty chairs found
//    //}
//}
