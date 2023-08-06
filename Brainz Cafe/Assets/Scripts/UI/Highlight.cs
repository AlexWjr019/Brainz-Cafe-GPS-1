using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Highlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    Image image;

    [SerializeField]
    Sprite sp_default, sp_highlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = sp_highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = sp_default;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}