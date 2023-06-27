//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class ObjectInteraction : MonoBehaviour
//{
//    public KeyCode interactKey = KeyCode.Return;
//    public KeyCode interactKey2 = KeyCode.E;
//    private bool canInteract = false;
//    private bool isImageShown = false;
//    public Image foodImage;
//    public Sprite[] foodSprites;
//    public int randomIndex;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            canInteract = true;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            canInteract = false;
//        }
//    }

//    private void Update()
//    {
//        if (canInteract && Input.GetKeyDown(interactKey))
//        {
//            TakeOrder();

//        }
//        if (canInteract && Input.GetKeyDown(interactKey2))
//        {
//            serveOrder();
//        }

//    }

//    private void TakeOrder()
//    {
//        Add your code here to handle taking the order
//        if (!isImageShown)
//        {
//            ShowImage();
//        }
//    }

//    private void serveOrder()
//    {
//        Add your code here to handle taking the order
//        if (isImageShown)
//        {
//            HideImage();
//            Invoke("DestroyCustomer", 3f);
//        }
//    }

//    private void ShowImage()
//    {
//        randomIndex = Random.Range(0, foodSprites.Length);
//        foodImage.sprite = foodSprites[randomIndex];
//        foodImage.gameObject.SetActive(true);
//        isImageShown = true;

//    }

//    private void HideImage()
//    {
//        randomIndex = Random.Range(0, foodSprites.Length);
//        foodImage.sprite = foodSprites[randomIndex];
//        foodImage.gameObject.SetActive(false);
//        isImageShown = false;
//    }

//    private void DestroyCustomer()
//    {
//        Destroy(gameObject); // Destroy the customer object
//    }

//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.Return;
    public KeyCode interactKey2 = KeyCode.E;
    private bool canInteract = false;
    private bool isImageShown = false;
    public Image foodImage;
    public Sprite[] foodSprites;
    public int randomIndex;
    private FoodSpawn foodSpawn;
    //public GameObject FoodSpawner;

    private void Start()
    {
        //FoodSpawner = GameObject.Find("Food Spawners");
        //foodSpawn = FoodSpawner.GetComponent<FoodSpawn>();
        foodSpawn = FindObjectOfType<FoodSpawn>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            TakeOrder();

        }
        if (canInteract && Input.GetKeyDown(interactKey2))
        {
            serveOrder();
        }

    }

    private void TakeOrder()
    {
        // Add your code here to handle taking the order
        if (!isImageShown)
        {
            //randomIndex = Random.Range(0, foodSprites.Length);
            ////foodSpawn.r = randomIndex;
            //foodSpawn.SetRandomIndex(randomIndex);
            //ShowImage();
            randomIndex = Random.Range(0, foodSprites.Length);
            foodSpawn.SetRandomIndex(randomIndex);
            ShowImage();
        }
    }

    private void serveOrder()
    {
        // Add your code here to handle taking the order
        if (isImageShown)
        {
            HideImage();
            Invoke("DestroyCustomer", 3f);
        }
    }

    private void ShowImage()
    {
        //randomIndex = Random.Range(0, foodSprites.Length);
        foodImage.sprite = foodSprites[randomIndex];
        foodImage.gameObject.SetActive(true);
        isImageShown = true;
        //foodImage.sprite = foodSprites[randomIndex];
        //foodImage.gameObject.SetActive(true);
        //isImageShown = true;

    }

    private void HideImage()
    {
        //randomIndex = Random.Range(0, foodSprites.Length);
        //foodImage.sprite = foodSprites[randomIndex];
        foodImage.gameObject.SetActive(false);
        isImageShown = false;
    }

    private void DestroyCustomer()
    {
        Destroy(gameObject); // Destroy the customer object
    }

}
