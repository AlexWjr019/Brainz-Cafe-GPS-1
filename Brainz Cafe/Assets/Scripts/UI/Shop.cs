using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private bool inRange = false, shopOpen = false;

    [SerializeField]
    GameObject shopUI;

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !shopOpen)
            {
                OpenShop();
            }
            else if (Input.GetKeyDown(KeyCode.E) && shopOpen)
            {
                OpenShop();
            }
        }
        else if (!inRange)
        {
            if (shopOpen)
            {
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        if (!shopOpen)
        {
            shopUI.SetActive(true);
            shopOpen = true;
        }
        else if (shopOpen)
        {
            shopUI.SetActive(false);
            shopOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
