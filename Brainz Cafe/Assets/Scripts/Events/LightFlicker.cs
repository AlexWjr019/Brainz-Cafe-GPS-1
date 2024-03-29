using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] float minLightIntensity;
    [SerializeField] float maxLightIntensity;
    [SerializeField] float betweenLightFlickers;
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
        yield return new WaitForSeconds(betweenLightFlickers);
        mylight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
        StartCoroutine(lightFlicker());
    }
}
