using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material newMaterial;
    public Material oldMaterial;

    public void updateMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && newMaterial != null)
        {
            renderer.material = newMaterial;
        }
    }

    public void returnMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && oldMaterial != null)
        {
            renderer.material = oldMaterial;
        }
    }
}
