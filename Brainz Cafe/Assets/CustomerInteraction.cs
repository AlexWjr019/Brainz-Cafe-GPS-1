using System.Collections;
using UnityEngine;

public class CustomerInteraction : MonoBehaviour
{
    public GameObject menuImage; // Reference to the menu image GameObject
    public GameObject[] foodImages; // Array of food image GameObjects

    private bool isSitting = false; // Flag to track if the customer is sitting
    private bool hasShownFoodImage = false; // Flag to track if the food image has been shown
    private FoodSpawn foodSpawn; // Reference to the FoodSpawn script
    private int randomIndex; // Variable to store the index of the shown food image
    private CustomerSatisfactionTimer timer;
    private bool playerEnteredCollision = false; // Flag to track if the player has entered the collision

    private void Start()
    {
        // Deactivate the menu and food images at the beginning
        menuImage.SetActive(false);
        SetFoodImagesActive(false);

        // Find and store a reference to the FoodSpawn script
        foodSpawn = FindObjectOfType<FoodSpawn>();

        timer = GetComponent<CustomerSatisfactionTimer>();
    }

    private void Update()
    {
        if (isSitting && !hasShownFoodImage && playerEnteredCollision)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Player pressed the Enter key
                menuImage.SetActive(false); // Deactivate the menu image
                ShowRandomFoodImage(); // Show a random food image
                hasShownFoodImage = true; // Set the flag to true

            }
        }
    }

    private IEnumerator ActivateMenuImageDelayed()
    {
        yield return new WaitForSeconds(2.0f);
        menuImage.SetActive(true);

        if (timer.isChasing)
        {
            menuImage.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chairs"))
        {
            // Customer is sitting on a chair
            isSitting = true;

            // Delay the activation of the menu image
            StartCoroutine(ActivateMenuImageDelayed());

        }

        if (collision.CompareTag("Player"))
        {
            playerEnteredCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Chairs"))
        {
            // Customer is leaving the chair
            isSitting = false;

            // Reset the food image flag
            hasShownFoodImage = false;

            // Deactivate the menu and food images
            menuImage.SetActive(false);
            SetFoodImagesActive(false);

            // Reset the customer food index in the FoodSpawn script
            //foodSpawn.AddCustomerFoodIndex(-1);
        }

        if (collision.CompareTag("Player"))
        {
            playerEnteredCollision = false;
        }
    }

    private void ShowRandomFoodImage()
    {
        // Activate a random food image from the array
        randomIndex = Random.Range(0, foodImages.Length);
        SetFoodImagesActive(false);
        foodImages[randomIndex].SetActive(true);

        // Pass the shown food image index to the FoodSpawn script
        foodSpawn.AddCustomerFoodIndex(randomIndex);
    }

    private void SetFoodImagesActive(bool isActive)
    {
        // Set all food images in the array active/inactive
        foreach (GameObject foodImage in foodImages)
        {
            foodImage.SetActive(isActive);
        }
    }
}

