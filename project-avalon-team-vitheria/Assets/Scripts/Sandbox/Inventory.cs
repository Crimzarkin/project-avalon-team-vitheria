using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public InventorySlot[] Slots;

    public void Start()
    {
        Singleton = this;
        Slots = GetComponentsInChildren<InventorySlot>();
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            InventorySlot slot = Slots[i];
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
            if(iteminSlot == null)
            {
                //Add item to an available slot
                return;
            }
        }
    }
}
