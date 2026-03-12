using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Inventory Singleton;
    public InventorySlot[] Slots;

    public void Awake()
    {
        Singleton = this;
        UpdateSlots();
    }

    void UpdateSlots()
    {
        Slots = GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in Slots)
        {
            slot.UpdateSlot();
        }
    }

    public bool CheckDuplicates(Item item)
    {
        foreach (InventorySlot slot in Slots)
        {
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
            if(iteminSlot != null)
            {
                if(iteminSlot.myItem.Compare(item))
                {
                    Debug.Log("Duplicate item found: " + item.name);
                    return true;
                }
            }
        }
        return false;
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            if (CheckDuplicates(item))
            {
                return;
            }

            InventorySlot slot = Slots[i];
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();

            if(iteminSlot == null)
            {
                //Add item to an available slot
                GameObject itemObj = new GameObject("Item");
                itemObj.transform.SetParent(slot.transform, false);
                
                RectTransform rect = itemObj.AddComponent<RectTransform>();
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
                rect.sizeDelta = new Vector2(20, 20);

                itemObj.AddComponent<CanvasRenderer>();

                InventoryItem invItem = itemObj.AddComponent<InventoryItem>();
                invItem.Initialize(item);

                UpdateSlots();
                return;
            }
        }
    }
}
