using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public StaminaController _staminaController;

    //public float moveSpeed = 5f;
    //public float runSpeed = 20f;
    //public static bool isSprinting;

    //public Rigidbody2D rb;

    //Vector2 movement;

    //private void Start()
    //{
    //    _staminaController = GetComponent<StaminaController>();
    //}

    //public void SetRunSpeed(float speed)
    //{
    //    runSpeed = speed;
    //}

    //void Update()
    //{

    //    movement.x = Input.GetAxisRaw("Horizontal");
    //    movement.y = Input.GetAxisRaw("Vertical");

    //    if(PlayerMovement.isSprinting)
    //    {
    //        moveSpeed = 20f;
    //    }

    //}

    //private void FixedUpdate()
    //{

    //    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    //    if (!Input.GetKey(KeyCode.LeftShift))
    //    {

    //        _staminaController.weAreSprinting = false;
    //    }

    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        if (_staminaController.playerStamina > 0)
    //        {
    //            rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
    //            _staminaController.weAreSprinting = true;
    //            _staminaController.Sprinting();
    //        }
    //    }
    //}


    Rigidbody2D rb;
    public static bool isSprinting;

    float walkSpeed = 5f;
    float runSpeed = 20f;
    float speedLimiter = 0.8f;
    float inputHorizontal;
    float inputVertical;


    private void Start()
    {
        _staminaController = GetComponent<StaminaController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetRunSpeed(float speed)
    {
        runSpeed = speed;
    }


    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (PlayerMovement.isSprinting)
        {
            walkSpeed = 20f;
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

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                _staminaController.weAreSprinting = false;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (_staminaController.playerStamina > 0)
                {
                    rb.velocity = new Vector2(inputHorizontal * runSpeed, inputVertical * runSpeed);
                    _staminaController.weAreSprinting = true;
                    _staminaController.Sprinting();
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
