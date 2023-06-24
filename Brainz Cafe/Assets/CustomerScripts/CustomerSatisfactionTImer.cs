using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSatisfactionTImer : MonoBehaviour
{
    public Image timer_radial_image;
    float time_remaining;
    public float max_time = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        time_remaining = max_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            timer_radial_image.fillAmount = time_remaining / max_time;
        }
    }
}

