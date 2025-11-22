using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public Item myItem;

    public void Start()
    {
        this.gameObject.AddComponent<Image>();
        this.gameObject.GetComponent<Image>().sprite = myItem.sprite;
    }
    public void Initialize(Item item)
    {
        myItem = item;
    }
}
