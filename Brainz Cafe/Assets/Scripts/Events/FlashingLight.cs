using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashingLight : MonoBehaviour
{
    [SerializeField] float minLightIntensity;
    [SerializeField] float maxLightIntensity;
    [SerializeField] float betweenLightFlash;
    [SerializeField] float beginningTime;

    Light2D mylight;

    private void Start()
    {
        mylight = GetComponent<Light2D>();
        StartCoroutine(startScene());
    }

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(beginningTime);
        StartCoroutine(lightFlicker());
    }

    IEnumerator lightFlicker()
    {
        yield return new WaitForSeconds(0.5f);
        mylight.intensity = minLightIntensity;

        yield return new WaitForSeconds(betweenLightFlash);
        mylight.intensity = maxLightIntensity;

        StartCoroutine(lightFlicker());
    }
}
