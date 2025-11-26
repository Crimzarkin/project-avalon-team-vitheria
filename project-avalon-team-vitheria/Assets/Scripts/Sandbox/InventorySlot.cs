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
        Debug.Log("Setting Block");
        if(myItem == null)
        {
            return;
        }
        BuildSystem buildSystem = FindObjectOfType<BuildSystem>();
        buildSystem.changeBlock(myItem.myItem.itemPrefab);
    }
}
