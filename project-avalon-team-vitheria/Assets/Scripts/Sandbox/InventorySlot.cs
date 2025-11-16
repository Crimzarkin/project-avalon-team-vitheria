using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem myItem;

    public void Start()
    {
        myItem = GetComponentInChildren<InventoryItem>();
    }

}
