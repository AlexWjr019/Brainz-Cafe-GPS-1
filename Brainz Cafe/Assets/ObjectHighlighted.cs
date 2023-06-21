using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighted : MonoBehaviour
{
    public GameObject player;
    public float highlightDistance = 2f;
    public CallOrder callorder;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        callorder = GetComponent<CallOrder>();
    }

    private void Update()
    {

        //Check if the player is destroy
        if (player != null)
        {
            // Calculate the distance between the player and the object
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // Check if the player is within the highlighting distance
            if (distance <= highlightDistance && callorder.isSitting)
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
