using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour
{
    public Image image;

    public void SetColour()
    {
        image.color = Color.grey;
    }
}
