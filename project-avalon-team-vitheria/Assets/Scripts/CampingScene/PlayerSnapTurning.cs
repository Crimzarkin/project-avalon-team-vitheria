using UnityEngine;
using UnityEngine.XR;

public class PlayerSnapTurning : MonoBehaviour
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


    void SnapTurn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }


}

