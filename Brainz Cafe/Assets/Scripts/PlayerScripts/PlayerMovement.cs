using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    BuyItem BT;

    //[HideInInspector] public StaminaController _staminaController;

    Rigidbody2D rb;
    //public static bool isSprinting;

    public float walkSpeed = 5f;
    float tempSpeed;

    //float runSpeed = 20f;
    float speedLimiter = 0.8f;
    float inputHorizontal;
    float inputVertical;

    float boostTimer = 0f;

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

    bool isMoving = false;

    private void Start()
    {
       //_staminaController = GetComponent<StaminaController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        tempSpeed = walkSpeed;
    }

    //public void SetRunSpeed(float speed)
    //{
    //    runSpeed = speed;
    //}


    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        // Check if the player is moving
        if (inputHorizontal != 0f || inputVertical != 0f)
        {
            isMoving = true;

            // Rest of the movement code...
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

        if (BT.boost)
        {
            boostTimer = Time.deltaTime;
        }
        if (BT.boost && boostTimer >= 15)
        {
            walkSpeed = tempSpeed;
            BT.boost = false;
        }

        //if (PlayerMovement.isSprinting)
        //{
        //    walkSpeed = 20f;
        //}
        
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

            //if (!Input.GetKey(KeyCode.LeftShift))
            //{
            //    _staminaController.weAreSprinting = false;
            //}

            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    if (_staminaController.playerStamina > 0)
            //    {
            //        rb.velocity = new Vector2(inputHorizontal * runSpeed, inputVertical * runSpeed);
            //        _staminaController.weAreSprinting = true;
            //        _staminaController.Sprinting();
            //    }
            //}
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player is colliding with the screen boundaries
        if (collision.CompareTag("Boundary"))
        {
            // Clamp the player's position within the screen boundaries
            float clampedX = Mathf.Clamp(transform.position.x, collision.bounds.min.x, collision.bounds.max.x);
            float clampedY = Mathf.Clamp(transform.position.y, collision.bounds.min.y, collision.bounds.max.y);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
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
}
