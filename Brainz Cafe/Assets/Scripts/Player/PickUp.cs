using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public Transform holdSpot2;
    public LayerMask pickUpMask;

    public Vector3 Direction { get; set; }
    public GameObject itemHolding;
    public GameObject itemHolding2;
    public string currentFoodObjectName;
    public string currentFoodObjectName2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!itemHolding)
            {
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .6f, pickUpMask);
                if (pickUpItem)
                {
                    itemHolding = pickUpItem.gameObject;
                    itemHolding.transform.position = holdSpot.position;
                    itemHolding.transform.parent = transform;
                    if (itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = false;

                    currentFoodObjectName = itemHolding.name;
                }
            }

            if (!itemHolding2)
            {
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                if (pickUpItem)
                {
                    itemHolding2 = pickUpItem.gameObject;
                    itemHolding2.transform.position = holdSpot2.position;
                    itemHolding2.transform.parent = transform;
                    if (itemHolding2.GetComponent<Rigidbody2D>())
                        itemHolding2.GetComponent<Rigidbody2D>().simulated = false;

                    currentFoodObjectName2 = itemHolding2.name;
                }
            }
        }
    }
}
