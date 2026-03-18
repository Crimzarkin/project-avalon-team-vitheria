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
                itemObj.transform.SetParent(slot.transform);

                itemObj.AddComponent<InventoryItem>();  
                itemObj.GetComponent<InventoryItem>().Initialize(item);

                itemObj.AddComponent<RectTransform>();
                itemObj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                itemObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                itemObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                itemObj.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
                itemObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

                itemObj.AddComponent<CanvasRenderer>();

                UpdateSlots();
                Debug.Log("Item added to slot " + i);
                return;
            }
        }
    }
}
