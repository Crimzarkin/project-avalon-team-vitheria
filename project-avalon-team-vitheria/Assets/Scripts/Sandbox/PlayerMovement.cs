using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;

    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // Ensure controller stays valid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Read trigger button
        bool triggerPressed = false;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Move forward WHILE holding trigger
        if (triggerPressed)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0; // prevent flying upward
            transform.position += forward.normalized * speed * Time.deltaTime;
        }
    }
}
