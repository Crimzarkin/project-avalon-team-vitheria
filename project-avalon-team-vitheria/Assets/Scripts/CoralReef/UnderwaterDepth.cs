using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UnderwaterDepth : MonoBehaviour
{
    public Transform mainCamera;
    public float isUnderwaterAtY;
    float minDepth = 0f;   // deepest point
    float maxDepth = 140f;  // surface
    public PostProcessVolume volume;
    public PostProcessProfile surfaceProfile;
    public PostProcessProfile underwaterProfile;

    public void Update()
    {
        if(mainCamera.position.y < isUnderwaterAtY)
        {
            EnableUnderwaterEffects(true);
        }
        else
        {
            EnableUnderwaterEffects(false);
        }
        
        float depthT = Mathf.InverseLerp(maxDepth, minDepth, mainCamera.position.y);
        float tempValue = Mathf.Lerp(0f, 100f, depthT);
        underwaterProfile.GetSetting<ColorGrading>().temperature.value = tempValue;
    }

    private void EnableUnderwaterEffects(bool active)
    {
        if (active)
        {
            RenderSettings.fog = true;
            volume.profile = underwaterProfile;
        }
        else
        {
            RenderSettings.fog = false;
            volume.profile = surfaceProfile;
        }
    }
}
