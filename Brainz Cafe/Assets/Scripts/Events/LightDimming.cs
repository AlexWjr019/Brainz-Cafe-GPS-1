using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightDimming : MonoBehaviour
{
    [SerializeField]
    float maxLightIntensity;

    Light2D mylight;

    void Start()
    {
        mylight = GetComponent<Light2D>();
    }

    void Update()
    {
        mylight.intensity = Mathf.PingPong(Time.time, maxLightIntensity);
    }
}
