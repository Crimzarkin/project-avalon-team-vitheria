using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRButtonClick : MonoBehaviour
{
    public Camera vrCamera; // your VR camera
    public LayerMask uiLayer; // layer for UI canvas

    void Update()
    {
        // Get trigger press from device
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool triggerValue;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            // Cast ray from camera forward
            Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f, uiLayer))
            {
                // Check if hit object has a Button
                Button button = hit.collider.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke(); // manually fire the click
                    Debug.Log("VR Button clicked via device-based controller!");
                }
            }
        }
    }
}
