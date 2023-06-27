using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallOrder : MonoBehaviour
{
    public Image popupImage;
    private CustomerAI customerAI;

    private void Start()
    {
        customerAI = GetComponent<CustomerAI>();
        customerAI.OnReachedEndOfPath += HandleReachedEndOfPath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && popupImage.gameObject.activeSelf)
        {
            DeactivatePopupImage();
        }
    }

    private void HandleReachedEndOfPath(bool value)
    {
        Debug.Log("ReachedEndOfPath: " + value);
        if (value)
        {
            ActivatePopupImage();
        }
        else
        {
            DeactivatePopupImage();
        }
    }

    private void ActivatePopupImage()
    {
        Debug.Log("Activating Popup Image");
        popupImage.gameObject.SetActive(true);
    }

    private void DeactivatePopupImage()
    {
        Debug.Log("Deactivating Popup Image");
        popupImage.gameObject.SetActive(false);
    }
}