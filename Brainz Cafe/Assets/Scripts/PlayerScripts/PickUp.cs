using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public bool isHoldingFood;
    private GameObject currentFoodObject; // Reference to the currently picked up food object

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            // Store the reference to the food object
            currentFoodObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            // Reset the reference to the food object when it is no longer in range
            currentFoodObject = null;
        }
    }

    private void Update()
    {
        // Check if the player is pressing the "E" key and there is a food object in range
        if (Input.GetKeyDown(KeyCode.E) && currentFoodObject != null)
        {
            PickUpFood();
        }
    }

    private void PickUpFood()
    {
        // Set the food object's position to the hold spot position
        currentFoodObject.transform.position = holdSpot.position;
        currentFoodObject.transform.parent = holdSpot; // Make the food object a child of the hold spot
        isHoldingFood = true;

        // Disable the food object's rigidbody and collider to prevent physics interactions
        Rigidbody2D foodRigidbody = currentFoodObject.GetComponent<Rigidbody2D>();
        if (foodRigidbody != null)
        {
            foodRigidbody.simulated = false;
        }
        Collider2D foodCollider = currentFoodObject.GetComponent<Collider2D>();
        if (foodCollider != null)
        {
            foodCollider.enabled = false;
        }

        // Perform any additional actions or logic you need when picking up the food object
    }

    public void DropOffFood()
    {

    }





}
