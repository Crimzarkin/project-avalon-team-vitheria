using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public InventorySlot[] Slots;
    public GameObject inventoryItemPrefab;

    void Awake()
    {
        Singleton = this;
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            InventorySlot slot = Slots[i];
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
            if(iteminSlot == null)
            {
                GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
                InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
                inventoryItem.Initialize(item);
                return;
            }
        }
    }
}
