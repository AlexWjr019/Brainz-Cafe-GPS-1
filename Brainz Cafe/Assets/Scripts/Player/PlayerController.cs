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

    private bool canBeDestroyed = false;

    //[SerializeField] private AudioSource throwingFoodSoundEffect;
    //[SerializeField] private AudioSource servingFoodSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 raycastDirection = isFacingLeft ? Vector2.left : Vector2.right;
        customerHit = Physics2D.Raycast(transform.position, raycastDirection, customerDetectionDistance, customerLayer);
        Debug.DrawRay(transform.position, raycastDirection * customerDetectionDistance, Color.red);
        
        if (customerHit)
        {
            CustomerInteraction customerAction = customerHit.collider.gameObject.GetComponent<CustomerInteraction>();
            PickUp pickUp = customerHit.collider.gameObject.GetComponent<PickUp>();

            if (customerAction != null && !customerAction.menuImage.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //servingFoodSoundEffect.Play();
                    customerAction.serveCustomer();
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

        // Check if the player presses the 'E' key
        if (canBeDestroyed && Input.GetKeyDown(KeyCode.E))
        {
            DestroyFoodObject(); // Destroy the food object
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the food object collides with the dustbin
        if (collision.gameObject.CompareTag("Trash"))
        {
            Debug.Log("OnTriggerEnter2D: " + collision.gameObject.name);
            canBeDestroyed = true;
        }
    }

    // OnTriggerExit2D is called when the food object stops colliding with something
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the food object stops colliding with the dustbin
        if (collision.gameObject.CompareTag("Trash"))
        {
            
            Debug.Log("OnTriggerExit2D: " + collision.gameObject.name);
            canBeDestroyed = false;
        }
    }

    private void DestroyFoodObject()
    {
        // Loop through all child objects of the player
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Check if the child has the "Food" tag
            if (child.CompareTag("Food"))
            {
                
                //throwingFoodSoundEffect.Play();
                Destroy(child.gameObject); // Destroy the food object
                AudioManager.Instance.Play("ThrowFood");
                return; // Exit the function to avoid destroying multiple food objects
            }
        }
    }
}
