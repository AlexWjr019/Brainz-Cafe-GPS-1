using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerInteraction : MonoBehaviour
{
    public GameObject menuImage; // Reference to the menu image GameObject
    public GameObject[] foodImages; // Array of food image GameObjects
    public GameObject[] foodImages2;

    //private bool isSitting = false; // Flag to track if the customer is sitting
    private bool hasShownFoodImage = false; // Flag to track if the food image has been shown
    private bool hasShownFoodImage2 = false; // Flag to track if the food image has been shown
    private FoodSpawn foodSpawn; // Reference to the FoodSpawn script
    private int randomIndex; // Variable to store the index of the shown food image
    private CustomerSatisfactionTimer timer;
    //private bool playerEnteredCollision = false; // Flag to track if the player has entered the collision
    public string foodName;
    public string foodName2;
    private PlayerController playercontroller;


    //NEW ADDED
    private PickUp pickup;

    private Rigidbody2D rb2D;

    private bool isCustomerStopMoving = false;
    private Vector2 previousPosition;

    private void Start()
    {
        // Deactivate the menu and food images at the beginning
        menuImage.SetActive(false);
        SetFoodImagesActive(false);
        SetFoodImages2Active(false);

        // Find and store a reference to the FoodSpawn script
        foodSpawn = FindObjectOfType<FoodSpawn>();
        playercontroller = GetComponent<PlayerController>();

        timer = GetComponent<CustomerSatisfactionTimer>();

        pickup = FindObjectOfType<PickUp>();
        if (pickup == null)
        {
            Debug.LogError("PickUp component not found!");
        }

        // Find and store a reference to the Rigidbody2D component
        rb2D = GetComponent<Rigidbody2D>();
        isCustomerStopMoving = false;
        previousPosition = rb2D.position;

    }

    private void Update()
    {

        // Check if the customer's position is not changing anymore
        if (rb2D.position == previousPosition)
        {
            isCustomerStopMoving = true;
        }
        else
        {
            isCustomerStopMoving = false;
        }

        // Update the previous position for the next frame
        previousPosition = rb2D.position;
    }

    public void serveCustomer()
    {
        DropOffFood();
    }


    private IEnumerator ActivateMenuImageDelayed()
    {
        yield return new WaitForSeconds(2.0f);
        //menuImage.SetActive(true);
        ShowRandomFoodImage(); // Show a random food image
        ShowRandomFoodImage2();
        hasShownFoodImage = true; // Set the flag to true
        hasShownFoodImage2 = true;

        if (timer.isAttacking)
        {
            menuImage.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chairs"))
        {
            Debug.Log("Touch");
            StartCoroutine(ActivateMenuImageDelayed());

        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chairs"))
        {
            isCustomerStopMoving = false;
            // Reset the food image flag
            hasShownFoodImage = false;
            hasShownFoodImage2 = false;

            // Deactivate the menu and food images
            menuImage.SetActive(false);
            SetFoodImagesActive(false);
            SetFoodImages2Active(false);
        }

    }

    private void ShowRandomFoodImage()
    {
        // Activate a random food image1 from the array
        randomIndex = UnityEngine.Random.Range(0, foodImages.Length);
        SetFoodImagesActive(false);
        foodImages[randomIndex].SetActive(true);

        // Pass the shown food image index to the FoodSpawn script
        foodSpawn.AddCustomerFoodIndex(randomIndex);

        // Log the name of the shown food image
        foodName = foodImages[randomIndex].name;
        Debug.Log("Food Name: " + foodName);

    }

    private void ShowRandomFoodImage2()
    {
        // Activate a random food image2 from the array
        randomIndex = UnityEngine.Random.Range(0, foodImages2.Length);
        SetFoodImages2Active(false);
        foodImages2[randomIndex].SetActive(true);

        // Pass the shown food image index to the FoodSpawn script
        foodSpawn.AddCustomerFoodIndex(randomIndex);

        // Log the name of the shown food image
        foodName2 = foodImages2[randomIndex].name;
        Debug.Log("Food 2 Name: " + foodName2);

    }

    private void SetFoodImagesActive(bool isActive)
    {
        // Set all food images in the array active/inactive
        foreach (GameObject foodImage in foodImages)
        {
            foodImage.SetActive(isActive);
        }
    }

    private void SetFoodImages2Active(bool isActive)
    {
        // Set all food images in the array active/inactive
        foreach (GameObject foodImage2 in foodImages2)
        {
            foodImage2.SetActive(isActive);
        }
    }


    //NEW ADDED
    private void DropOffFood()
    {

        bool foodImageActive = IsFoodImagesActive();
        bool foodImage2Active = IsFoodImages2Active();
        bool foodImageActive2 = IsFoodImagesActive2();
        bool foodImage2Active2 = IsFoodImages2Active2();

        // Compare the names of the picked up food object and the food image
        if (pickup.currentFoodObjectName == foodName && foodImageActive)
        {
            // Deactivate the first food image if it matches the last served food
            if (pickup.currentFoodObjectName == foodName)
            {
                //pickup.itemHolding.transform.position = transform.position + pickup.Direction;
                pickup.itemHolding.transform.parent = null;
                if (pickup.itemHolding.GetComponent<Rigidbody2D>())
                    pickup.itemHolding.GetComponent<Rigidbody2D>().simulated = true;

                SetFoodImagesActive(false);
                Destroy(pickup.itemHolding);
                pickup.itemHolding = null;
                pickup.currentFoodObjectName = null;
                foodName = null;
                return; // Exit the method after deactivating the image
            }
        }

        // Compare the names of the picked up food object and the food image
        if (pickup.currentFoodObjectName == foodName2 && foodImage2Active)
        {
            // Deactivate the second food image if it matches the last served food
            if (pickup.currentFoodObjectName == foodName2)
            {
                pickup.itemHolding.transform.parent = null;
                if (pickup.itemHolding.GetComponent<Rigidbody2D>())
                    pickup.itemHolding.GetComponent<Rigidbody2D>().simulated = true;

                SetFoodImages2Active(false);
                Destroy(pickup.itemHolding);
                pickup.itemHolding = null;
                pickup.currentFoodObjectName = null;
                foodName2 = null;
                return; // Exit the method after deactivating the image
            }
        }

        // Compare the names of the picked up food object and the food image
        if (pickup.currentFoodObjectName2 == foodName && foodImageActive2)
        {
            // Deactivate the first food image if it matches the last served food
            if (pickup.currentFoodObjectName2 == foodName)
            {
                pickup.itemHolding2.transform.parent = null;
                if (pickup.itemHolding2.GetComponent<Rigidbody2D>())
                    pickup.itemHolding2.GetComponent<Rigidbody2D>().simulated = true;

                SetFoodImagesActive(false);
                Destroy(pickup.itemHolding2);
                pickup.itemHolding2 = null;
                pickup.currentFoodObjectName2 = null;
                foodName = null;
                return; // Exit the method after deactivating the image
            }
        }

        // Compare the names of the picked up food object and the food image
        if (pickup.currentFoodObjectName2 == foodName2 && foodImage2Active2)
        {
            // Deactivate the second food image if it matches the last served food
            if (pickup.currentFoodObjectName2 == foodName2)
            {
                pickup.itemHolding2.transform.parent = null;
                if (pickup.itemHolding2.GetComponent<Rigidbody2D>())
                    pickup.itemHolding2.GetComponent<Rigidbody2D>().simulated = true;

                SetFoodImages2Active(false);
                Destroy(pickup.itemHolding2);
                pickup.itemHolding2 = null;
                pickup.currentFoodObjectName2 = null;
                foodName2 = null;
                return; // Exit the method after deactivating the image
            }
        }

        // If the food doesn't match the served food or the corresponding food image is not active, log a message
        Debug.Log("The food does not match the served food or the food image is not active.");

    }

    private bool IsFoodImagesActive()
    {
        foreach (GameObject foodImage in foodImages)
        {
            if (foodImage.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsFoodImages2Active()
    {
        foreach (GameObject foodImage2 in foodImages2)
        {
            if (foodImage2.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsFoodImagesActive2()
    {
        foreach (GameObject foodImage in foodImages)
        {
            if (foodImage.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsFoodImages2Active2()
    {
        foreach (GameObject foodImage2 in foodImages2)
        {
            if (foodImage2.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
