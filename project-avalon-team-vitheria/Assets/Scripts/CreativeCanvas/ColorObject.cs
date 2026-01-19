using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public Color color;

    private Renderer rend;
    private Material originalMaterial;
    public Material highlightMaterial;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void Highlight(bool enable)
    {
        if (enable)
            rend.material = highlightMaterial;
        else
            rend.material = originalMaterial;
    }
}
