using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFacingLeft;
    public float customerDetectionDistance = 5f;
    public LayerMask customerLayer;
    private Transform player;
    public RaycastHit2D customerHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction based on player's facing
        Vector2 raycastDirection = rb.velocity.x > 0 ? Vector2.right : Vector2.left;

        // Perform the raycast in the calculated direction
        customerHit = Physics2D.Raycast(transform.position, raycastDirection, customerDetectionDistance, customerLayer);
        Debug.DrawRay(transform.position, raycastDirection * customerDetectionDistance, Color.red);

        //customerHit = Physics2D.Raycast(transform.position, raycastDirection, customerDetectionDistance, customerLayer);
        //Debug.DrawRay(transform.position, raycastDirection * customerDetectionDistance, Color.red);
        if (customerHit)
        {
            if (customerHit)
            {
                CustomerInteraction customerAction = customerHit.collider.gameObject.GetComponent<CustomerInteraction>();
                PickUp pickUp = customerHit.collider.gameObject.GetComponent<PickUp>();
                if (customerAction != null && !customerAction.menuImage.activeInHierarchy)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        customerAction.serveCustomer();
                    }
                    //customerAction.customerOrder();

                    //if (!customerAction.menuImage.activeInHierarchy)
                    //{
                    //    if (Input.GetKeyDown(KeyCode.E))
                    //    {
                    //        customerAction.serveCustomer();
                    //    }
                    //}
                }
            }
        }

        Vector3 playerPosition = player.transform.position;

        if (playerPosition.x > transform.position.x)
        {
            isFacingLeft = false;
        }
        else
        {
            isFacingLeft = true;
        }

    }
}
