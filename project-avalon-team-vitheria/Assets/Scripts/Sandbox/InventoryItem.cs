using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public Item myItem;
    Image itemIcon;

    public void Initialize(Item item)
    {
        myItem = item;
        itemIcon.sprite = item.sprite;
    }
}
