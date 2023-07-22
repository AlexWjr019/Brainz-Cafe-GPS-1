using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField]
    GameObject itemUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemUI.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        itemUI.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {

    }
}
