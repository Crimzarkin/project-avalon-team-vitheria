using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlock : MonoBehaviour
{
    public GameObject inventoryObject;
    public Item item;
    public bool addedToInventory = false;
    public void AddBlockToInventory()
    {
        if (inventoryObject != null && addedToInventory == false)
        {
            inventoryObject.GetComponent<Inventory>().AddItem(item);
            addedToInventory = true;
        }
    }
}
