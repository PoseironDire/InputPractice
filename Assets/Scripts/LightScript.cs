using UnityEngine;

public class LightScript : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    public float maxLerp; public float minLerp;
    bool scale; float lerp;
    //Cool Light Effect
    void FixedUpdate()
    {
        if (lerp <= minLerp) scale = true; /**/ if (lerp >= maxLerp) scale = false;
        if (scale) lerp = Mathf.Lerp(lerp, maxLerp + 1, 0.01f);
        else lerp = Mathf.Lerp(lerp, minLerp - 1, 0.02f);

        light2D.pointLightOuterRadius = lerp;
    }
}
