using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{

    [Header("Fog Control Booleans")]
    [Tooltip("This is Controlled through the Script. It's visible for testing purposes")]
    public bool fogBool;
    [Tooltip("Thick this if you want to have a smoother transition once enable/disable")]
    public bool shouldSmooth;

    [Header("Smoothness of Fog Enable/Disable")]
    [SerializeField] float fogMaxDensity;
    [SerializeField] float fogMinDensity;
    [SerializeField] float lerpDuration;

    private Coroutine fogCoroutine;

    public void EnableFog()
    {
        fogBool = true;
        RenderSettings.fog = fogBool;

        if (shouldSmooth)
        { 
            if(fogCoroutine != null)
            {
                StopCoroutine(fogCoroutine);
            }
            fogCoroutine = StartCoroutine(LerpFogDensity(fogMinDensity, fogMaxDensity, lerpDuration));
        }
        else
        {
            RenderSettings.fogDensity = fogMaxDensity;
        }
       
    }

    public void DisableFog()
    {

        if (shouldSmooth)
        {
            if(fogCoroutine != null)
            {
                StopCoroutine(fogCoroutine);
            }
            fogCoroutine = StartCoroutine(LerpFogDensity(fogMaxDensity, fogMinDensity, lerpDuration, false));
        }
        else
        {
            fogBool = false;
            RenderSettings .fog = fogBool;
            RenderSettings.fogDensity = fogMinDensity;
        }

    }

    private IEnumerator LerpFogDensity(float startDensity, float endDensity, float duration, bool enableFog = true)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(startDensity, endDensity, elapsed / duration);
            yield return null;
        }

        RenderSettings.fogDensity = endDensity;
        fogBool = enableFog;
        RenderSettings.fog = fogBool;


    }

}
