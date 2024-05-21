using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomerAI : MonoBehaviour
{
    public Counter[] chairs;

    public Transform target;

    Counter tempChair;

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

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        chairs = FindObjectsOfType<Counter>();

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
            animator.SetBool("IsWalking", false);
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
            animator.SetBool("IsWalking", true);
            reachedEndOfPath = false;
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)path.vectorPath[currentWayPoint], speed * Time.deltaTime));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (rb.velocity.x > 0.01f)
        {
            animator.SetBool("isFacingRight", true);
        }
        else if (rb.velocity.x < -0.01f)
        {
            animator.SetBool("isFacingRight", false);
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

                    spriteRenderer.sortingOrder = chairs[i].sr.sortingOrder;
                    
                    animator.SetBool("IsWalking", true);

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