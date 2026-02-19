using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FindBlock : MonoBehaviour
{

    public GameObject InventoryObject;
    public Item item;
    private InputDevice controller;
    private bool triggerPressed = false;
    private bool lastTriggerPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    // Update is called once per frame
    public void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Detect rising edge
        if (triggerPressed && !lastTriggerPressed)
        {
            InventoryObject.GetComponent<Inventory>().AddItem(item);
        }

        lastTriggerPressed = triggerPressed;
    }
}
