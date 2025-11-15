using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }
    public Item myItem { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Set build system block
        }
    }

    public void SetItem(InventoryItem item)
    {
    //Add item to an available slot
    /**
        myItem = item;
        if(item != null)
        {
            item.activeSlot = this;
            item.transform.SetParent(this.transform);
            item.transform.localPosition = Vector3.zero;
        }
    **/
    }
}
