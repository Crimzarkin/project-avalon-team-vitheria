using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem myItem;

    public void Start()
    {
        myItem = GetComponentInChildren<InventoryItem>();
        Debug.Log("Inventory Slot initialized with item: " + (myItem != null ? myItem.myItem.name : "None"));
    }

    public void setBlock()
    {
        if(myItem == null)
        {
            Debug.LogWarning("No item in this inventory slot to set as block.");
            return;
        }
        BuildSystem buildSystem = FindObjectOfType<BuildSystem>();
        Debug.Log("Found BuildSystem: " + (buildSystem != null ? "Yes" : "No"));
        buildSystem.changeBlock(myItem.myItem.itemPrefab);
        Debug.Log("Block changed to: " + myItem.myItem.itemPrefab.name);
    }
}
