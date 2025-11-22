using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBlock : MonoBehaviour
{

    public GameObject InventoryObject;
    public Item item;

    // Update is called once per frame
    public void OnMouseDown()
    {
        InventoryObject.GetComponent<Inventory>().AddItem(item);
    }
}
