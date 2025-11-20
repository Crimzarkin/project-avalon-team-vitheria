using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlotScript : MonoBehaviour
{
    private MeshRenderer rend;
    public bool isSoiled = false;
    public bool hasSeed = false;

    public MeshRenderer seedRenderer;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend == null)
            rend = GetComponentInChildren<MeshRenderer>();
    }

    public void ApplySoil(Material newSoilMaterial)
    {
        if (isSoiled) return;
        rend.material = newSoilMaterial;
        isSoiled = true;
    }

    public void PlantSeed(Material seedMaterial)
    {
        if (!isSoiled) return;
        if (hasSeed) return;

        seedRenderer.material = seedMaterial;
        hasSeed = true;
    }
}


