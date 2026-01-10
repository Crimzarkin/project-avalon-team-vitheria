using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UnderwaterDepth : MonoBehaviour
{
    public Transform mainCamera;
    public int depth = 0;
    public PostProcessVolume volume;
    public PostProcessProfile surfaceProfile;
    public PostProcessProfile underwaterProfile;

    void Update()
    {
        if(mainCamera.position.y < depth)
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
            volume.profile = underwaterProfile;
        }
        else
        {
            volume.profile = surfaceProfile;
        }
    }
}
