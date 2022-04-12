using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2D;

    bool scale; float lerp;
    public float maxLerp; public float minLerp;
    void FixedUpdate()
    {
        if (lerp <= minLerp) scale = true; /**/ if (lerp >= maxLerp) scale = false;
        if (scale) lerp = Mathf.Lerp(lerp, maxLerp + 1, 0.01f);
        else lerp = Mathf.Lerp(lerp, minLerp - 1, 0.02f);

        light2D.pointLightOuterRadius = lerp;
    }
}
