using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryUI;
    public BuildSystem buildSystem;

    private InputDevice controller;
    private bool buttonPressed = false;
    private bool lastButtonPressed = false;

    void Start()
    {
        inventoryUI.SetActive(false);
        buildSystem = FindObjectOfType<BuildSystem>();

        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()

    {
  
        // Ensure controller stays valid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool buttonPressed = false;
        // Read touchpad click (primary2DAxisClick)
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonPressed) && buttonPressed && !lastButtonPressed)
        
     
        {
       ;
            // Toggle UI
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            // Update build system (block placement only when closed)
            buildSystem.inventoryClosed = !inventoryUI.activeSelf;
        }

        lastButtonPressed = buttonPressed;
    }
}
