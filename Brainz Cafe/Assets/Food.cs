using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isFoodInside = false;

    private void Update()
    {
        // Check for a specific input to interact (e.g., pressing the "E" key).
        if (isFoodInside && Input.GetKeyDown(KeyCode.E))
        {
            RemoveFoodFromScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            isFoodInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            isFoodInside = false;
        }
    }

    private void RemoveFoodFromScene()
    {
        // Implement your logic to remove the food object from the scene or increment score
        Destroy(gameObject); // For demonstration purposes, the dustbin will be removed. Change this to remove the food.
    }
}
