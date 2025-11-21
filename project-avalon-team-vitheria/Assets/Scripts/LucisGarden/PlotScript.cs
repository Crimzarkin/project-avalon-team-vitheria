using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlotScript : MonoBehaviour
{
    private MeshRenderer rend;
    public bool isSoiled = false;
    public bool hasSeed = false;
    public bool isWatered = false;

    public MeshRenderer seedRenderer;
    public GameObject RedPhotinia;
    public GameObject MapleBush;
    public GameObject ArabianJasmine;
    public GameObject WaterObject;
    private Material plantedSeedMaterial;
    private bool isIrrigated = false;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend == null)
            rend = GetComponentInChildren<MeshRenderer>();
        
        RedPhotinia.SetActive(false);
        MapleBush.SetActive(false);
        ArabianJasmine.SetActive(false);
        Debug.Log("Plot debug test: " + rend.material.name);
    }

    public void ApplySoil(Material newSoilMaterial)
    {
        if (isSoiled) return;
        rend.material = newSoilMaterial;
        isSoiled = true;
    }

    public void PlantSeed(Material seedMaterial)
    {
        if (!isSoiled || hasSeed) return;
        seedRenderer.material = seedMaterial;
        plantedSeedMaterial = seedMaterial;
        hasSeed = true;

        StartCoroutine(GrowPlant(5.0f)); // Start growing plant after 5 seconds
    }
    IEnumerator GrowPlant(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // wait number of second till plant grows

        string seedName = plantedSeedMaterial.name;
        seedName = seedName.Replace(" (Instance)", "");
        Debug.Log("Planted seed material: " + seedName);

        // Matching material name to plant
        if (seedName.Contains("RedPhotinia"))
        {
            RedPhotinia.SetActive(true);
            StartCoroutine(GrowthOverTime(RedPhotinia, 3f, 3)); // 3 minutes, 3 steps
        }
        else if (seedName.Contains("MapleBush"))
        {
            MapleBush.SetActive(true);
            StartCoroutine(GrowthOverTime(MapleBush, 4f, 4)); // 4 minutes, 4 steps
        }
        else if (seedName.Contains("ArabianJasmine"))
        {
            ArabianJasmine.SetActive(true);
            StartCoroutine(GrowthOverTime(ArabianJasmine, 2f, 2)); // 2 minutes, 2 steps
        }
        else
        {
            Debug.LogWarning("No matching plant found for seed: " + seedName);
        }

        // Hide the seed renderer
        seedRenderer.enabled = false;
    }

    IEnumerator GrowthOverTime(GameObject plant, float growthDuration, float steps)
    {
        growthDuration *= 60f; // convert minutes to seconds
        Vector3 initialScale = plant.transform.localScale;
        Vector3 targetScale = initialScale * 1.5f; //grow to 150% of original size
        Vector3 scaleStep = (targetScale - initialScale) / steps;

        float waitTime = growthDuration / steps;
        for (int i = 0; i < steps; i++)
        {
            plant.transform.localScale += scaleStep;
            yield return new WaitForSeconds(waitTime);
        }
        plant.transform.localScale = targetScale; //final scale is exact
    }

    public void WaterPlot()
    {
        if (isIrrigated) return;

        isIrrigated = true;

        if (WaterObject != null)
            WaterObject.SetActive(true);

        Debug.Log("Plot watered!");
    }

}

