using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
   
    public InventoryItem myItem;

    public void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        myItem = GetComponentInChildren<InventoryItem>();
    }

    public void setBlock()
    {
        if(myItem == null)
        {
            Debug.Log("No item in this slot!");
            return;
        }
        BuildSystem buildSystem = FindObjectOfType<BuildSystem>();
        if(buildSystem == null)
        {
            Debug.Log("No BuildSystem found in the scene!");
        }
        buildSystem.changeBlock(myItem.myItem.itemPrefab);
    }
}
