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

    private void Start()
    {
       //_staminaController = GetComponent<StaminaController>();
        rb = gameObject.GetComponent<Rigidbody2D>();

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
}
