using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomerAI : MonoBehaviour
{
    public EmptyChair[] chairs;

    public Transform target;

    EmptyChair tempChair;

    public float speed = 1f;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWayPoint = 0;
    public bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public delegate void ReachedEndOfPath(bool value);
    public event ReachedEndOfPath OnReachedEndOfPath;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        chairs = FindObjectsOfType<EmptyChair>();

        StartCoroutine(lookChair());

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void FixedUpdate()
    {
        if (reachedEndOfPath || path == null)
        {
            
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            
            reachedEndOfPath = true;
            animator.SetBool("IsWalking", false);
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //rb.position = Vector2.MoveTowards(rb.position, (Vector2)path.vectorPath[currentWayPoint], speed * Time.deltaTime);

        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)path.vectorPath[currentWayPoint], speed * Time.deltaTime));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    IEnumerator lookChair()
    {
        while (true)
        {
            for (int i = 0; i < chairs.Length; i++)
            {
                if (!chairs[i].isOccupied)
                {
                    target = chairs[i].transform;
                    tempChair = chairs[i];
                    tempChair.isOccupied = true;
                    
                    animator.SetBool("IsWalking", true);

                    GetComponent<ObjectAutoWalk>().enabled = false;
                    yield break;
                }
            }
            yield return new WaitForSeconds(5);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (tempChair != null && col.gameObject == tempChair.gameObject)
        {
            animator.SetBool("IsWalking", false);
            reachedEndOfPath = true;
            OnReachedEndOfPath?.Invoke(reachedEndOfPath);
            Debug.Log("Finally FOOD");
        }
    }

    private void OnDestroy()
    {
         tempChair.isOccupied = false;
    }
}