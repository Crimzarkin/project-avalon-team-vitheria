using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
