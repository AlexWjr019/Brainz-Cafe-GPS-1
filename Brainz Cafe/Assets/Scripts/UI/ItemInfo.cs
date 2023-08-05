using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField]
    GameObject itemUI;

    [SerializeField]
    Image image;

    [SerializeField]
    Sprite sp_default, sp_highlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemUI.SetActive(true);
        image.sprite = sp_highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemUI.SetActive(false);
        image.sprite = sp_default;
    }

    public void OnSelect(BaseEventData eventData)
    {

    }
}
