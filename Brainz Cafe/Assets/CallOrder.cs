using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallOrder : MonoBehaviour
{
    public Image popupImage;
    public bool isSitting = false;
    public float delay = 5f;

    private void Start()
    {
        Invoke("CallForOrder", delay);
    }

    private void CallForOrder()
    {
        isSitting = true;
        callOrder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            popupImage.gameObject.SetActive(false);
        }
    }

    private void callOrder()
    {
        if (isSitting)
        {
            popupImage.gameObject.SetActive(true);
        }
        else
        {
            popupImage.gameObject.SetActive(false);
        }
    }
}
