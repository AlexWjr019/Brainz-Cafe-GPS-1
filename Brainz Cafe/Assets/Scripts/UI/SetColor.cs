using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour
{
    public Image image;

    public void SetColourDarkGrey()
    {
        image.color = new Color32(70, 70, 70, 225);
    }
    public void SetColourWhite()
    {
        image.color = Color.white;
    }
}
