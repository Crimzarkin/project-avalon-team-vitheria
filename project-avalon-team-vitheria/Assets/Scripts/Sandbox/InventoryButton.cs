using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryUI;

    public BuildSystem buildSystem;

    public void Start()
    {
        inventoryUI.SetActive(false);
        buildSystem = FindObjectOfType<BuildSystem>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            buildSystem.inventoryClosed = !inventoryUI.activeSelf;
        }
    }
}
