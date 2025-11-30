using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float snapTurnAngle = 15f;

    private float triggerHoldTime = 0f;
    private float requiredHoldTime = 0.8f;

    private InputDevice controller;

    private bool triggerPressed = false;

    // Touchpad axis
    private Vector2 axis;
    private bool leftSwipe = false;
    private bool rightSwipe = false;

    // Swipe Detection Threshold
    public float swipeThreshold = 0.6f;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // Keep controller valid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // ---------- MOVEMENT ----------
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed)
        {
            // accumulate time while trigger is held
            triggerHoldTime += Time.deltaTime;

            // only move forward if held long enough
            if (triggerHoldTime >= requiredHoldTime)
                MoveForward();
        }
        else
        {
            // reset timer when trigger is released
            triggerHoldTime = 0f;
        }

        // ---------- SNAP TURNING ----------
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis))
        {
            // Swipe Left (axis.x < -threshold) → Turn Left

            if (axis.x < -swipeThreshold && !leftSwipe)
            {
                SnapTurn(-snapTurnAngle);  // turn right
                leftSwipe = true;
            }
            else if (axis.x > -swipeThreshold)
            {
                leftSwipe = false;
            }

            // Swipe Right (axis.x > +threshold) → Turn Right
            if (axis.x > swipeThreshold && !rightSwipe)
            {
                SnapTurn(+snapTurnAngle);  // turn left
                rightSwipe = true;
            }
            else if (axis.x < swipeThreshold)
            {
                rightSwipe = false;
            }
        }
    }

    void MoveForward()
    {
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0; // prevent vertical movement
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }

    void SnapTurn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }


}
