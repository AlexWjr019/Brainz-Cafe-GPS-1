using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAutoWalk : MonoBehaviour
{
    public Transform startPoint; // Reference to the start point
    public Transform endPoint; // Reference to the end point
    public float speed = 2f; // Speed at which the object moves

    private bool isMoving = false; // Flag to track if the object is currently moving
    private bool hasReachedDestination = false; // Flag to track if the object has reached the destination

    private float checkInterval = 5f; // Interval in seconds to check for the first point occupancy
    private float timer = 0f; // Timer to track the elapsed time

    private void Start()
    {
        // Assign the start and end points programmatically
        startPoint = GameObject.Find("StartPoint").transform;
        endPoint = GameObject.Find("EndPoint").transform;
    }

    private void Update()
    {
        if (!isMoving && !hasReachedDestination)
        {
            timer += Time.deltaTime;

            if (timer >= checkInterval)
            {
                timer = 0f;

                if (IsEndPointClear())
                {
                    StartCoroutine(MoveObjectRoutine());
                }
                else
                {
                    Debug.Log("End point is occupied. Object will not move.");
                    hasReachedDestination = true;
                }
            }
        }
    }

    private bool IsEndPointClear()
    {
        Collider2D[] endColliders = Physics2D.OverlapPointAll(endPoint.position);
        foreach (var collider in endColliders)
        {
            if (collider.gameObject != gameObject)
            {
                // End point is occupied by another object
                return false;
            }
        }

        // End point is clear
        return true;
    }

    private IEnumerator MoveObjectRoutine()
    {
        isMoving = true;

        Vector2 currentPos = startPoint.position;
        Vector2 targetPos = endPoint.position;

        float distance = Vector2.Distance(currentPos, targetPos);
        float totalTime = distance / speed;
        float currentTime = 0f;

        while (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / totalTime;

            transform.position = Vector2.Lerp(currentPos, targetPos, t);

            yield return null;
        }

        // Ensure the object reaches the destination precisely
        transform.position = targetPos;

        hasReachedDestination = true;
        isMoving = false;
    }
}
