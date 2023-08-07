using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    BuyItem BT;

    Rigidbody2D rb;

    public float walkSpeed = 5f;
    float tempSpeed;

    float speedLimiter = 0.8f;
    float inputHorizontal;
    float inputVertical;

    float boostTimer = 0f;

    public bool isSlowedDown = false;
    float slowedSpeed;
    [SerializeField]
    float slowdownFactor = 0.5f;

    Animator animator;

    string currentState;

    const string PLAYER_IDLE_FRONT = "Player_Idle_Front";
    const string PLAYER_IDLE_BACK = "Player_Idle_Back";
    const string PLAYER_IDLE_LEFT = "Player_Idle_Left";
    const string PLAYER_IDLE_RIGHT = "Player_Idle_Right";

    const string PLAYER_WALK_LEFT = "Player_Walk_Left";
    const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    const string PLAYER_WALK_FRONT = "Player_Walk_Front";
    const string PLAYER_WALK_BACK = "Player_Walk_Back";

    [HideInInspector]
    public bool isMoving = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        tempSpeed = walkSpeed;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        SpeedPill();

        // Check if the player is moving
        if (inputHorizontal != 0f || inputVertical != 0f)
        {
            isMoving = true;       
        }
        else
        {
            isMoving = false;

            // Set idle animation based on the last movement direction
            if (currentState == PLAYER_WALK_RIGHT || currentState == PLAYER_IDLE_RIGHT)
            {
                changeAnimationState(PLAYER_IDLE_RIGHT);
            }
            else if (currentState == PLAYER_WALK_LEFT || currentState == PLAYER_IDLE_LEFT)
            {
                changeAnimationState(PLAYER_IDLE_LEFT);
            }
            else if (currentState == PLAYER_WALK_BACK || currentState == PLAYER_IDLE_BACK)
            {
                changeAnimationState(PLAYER_IDLE_BACK);
            }
            else if (currentState == PLAYER_WALK_FRONT || currentState == PLAYER_IDLE_FRONT)
            {
                changeAnimationState(PLAYER_IDLE_FRONT);
            }

            // Reset velocity to stop the player's movement
            rb.velocity = Vector2.zero;
        }

    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

            if (inputHorizontal > 0)
            {
                changeAnimationState(PLAYER_WALK_RIGHT);
            }
            else if (inputHorizontal < 0)
            {
                changeAnimationState(PLAYER_WALK_LEFT);
            }
            else if (inputVertical > 0)
            {
                changeAnimationState(PLAYER_WALK_BACK);
            }
            else if (inputVertical < 0)
            {
                changeAnimationState(PLAYER_WALK_FRONT);
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    public void SpeedPill()
    {
        if (BT.boost)
        {
            boostTimer = Time.deltaTime;
        }
        if (BT.boost && boostTimer >= BT.boostDuration)
        {
            walkSpeed = tempSpeed;
            BT.boost = false;
        }
    }

    public void AcidSlow()
    {
        if (isSlowedDown)
        {
            if (walkSpeed == slowedSpeed)
            {
                return;
            }
            else
            {
                walkSpeed *= slowdownFactor;
                slowedSpeed = walkSpeed;
            }
        }
        else
        {
            walkSpeed = tempSpeed;
        }
    }

    void changeAnimationState(string newState)
    {
        // Stop animation from interrupting itself
        if (currentState == newState) return;

        //Play new animation
        animator.Play(newState);

        //Update current state
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Acid Puddle"))
        {
            isSlowedDown = true;
            AcidSlow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Acid Puddle"))
        {
            isSlowedDown = false;
            AcidSlow();
        }
    }
}
