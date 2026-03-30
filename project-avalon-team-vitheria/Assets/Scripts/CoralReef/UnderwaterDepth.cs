using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UnderwaterDepth : MonoBehaviour
{
    public Transform mainCamera;
    public float isUnderwaterAtY;

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
