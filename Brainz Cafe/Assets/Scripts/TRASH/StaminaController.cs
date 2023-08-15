//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class StaminaController : MonoBehaviour
//{
//    public float playerStamina = 100.0f;
//    [SerializeField] private float maxStamina = 100.01f;
//    [HideInInspector] public bool hasRegenerated = true;
//    [HideInInspector] public bool weAreSprinting = false;

//    [Range(0, 50)][SerializeField] private float staminaDrain = 0.5f;
//    [Range(0, 50)][SerializeField] private float staminaRegen = 0.5f;

//    [SerializeField] private int slowedRunSpeed = 5;
//    [SerializeField] private int normalRunSpeed = 20;

//    [SerializeField] private Image staminaProgressUI = null;
//    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

//    private PlayerMovement playerController;

//    private void Start()
//    {
//        playerController = GetComponent<PlayerMovement>();
//    }

//    private void Update()
//    {
//        if (!weAreSprinting)
//        {
//            if (playerStamina <= maxStamina - 0.01)
//            {
//                playerStamina += staminaRegen * Time.deltaTime;
//                UpdateStamina(1);

//                if (playerStamina >= maxStamina)
//                {
//                    playerController.SetRunSpeed(normalRunSpeed);
//                    sliderCanvasGroup.alpha = 0;
//                    hasRegenerated = true;
//                }
//            }
//        }
//    }

//    public void Sprinting()
//    {
//        if (hasRegenerated)
//        {
//            weAreSprinting = true;
//            playerStamina -= staminaDrain * Time.deltaTime;
//            UpdateStamina(1);

//            if (playerStamina <= 0.01)
//            {
//                hasRegenerated = false;
//                playerController.SetRunSpeed(slowedRunSpeed);
//                sliderCanvasGroup.alpha = 0;
//            }
//        }
//    }

//    void UpdateStamina(int value)
//    {
//        staminaProgressUI.fillAmount = playerStamina / maxStamina;

//        if (value == 0)
//        {
//            sliderCanvasGroup.alpha = 0;
//        }
//        else
//        {
//            sliderCanvasGroup.alpha = 1;
//        }
//    }

//}
