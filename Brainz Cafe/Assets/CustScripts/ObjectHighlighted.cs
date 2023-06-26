using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighted : MonoBehaviour
{
    public GameObject player;
    public float highlightDistance = 2f;
    //public CallOrder callorder;
    public CustomerAI customerAI;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //callorder = GetComponent<CallOrder>();
        customerAI = GetComponent<CustomerAI>();
        player = GameObject.Find("Player_Testing(Unity)");
    }

    private void Update()
    {

        //Check if the player is destroy
        if (player != null)
        {
            // Calculate the distance between the player and the object
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // Check if the player is within the highlighting distance
            if (distance <= highlightDistance && /*callorder.isSitting*/ customerAI.reachedEndOfPath)
            {
                // Highlight the object
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                // Remove the highlight
                spriteRenderer.color = Color.white;
            }
        }
        else
        {
            // Remove the highlight
            spriteRenderer.color = Color.white;
        }

    }


}