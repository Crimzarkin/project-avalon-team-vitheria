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
    private float axisHoldTimer = 0f;  // Tracks how long the thumbstick is held
    public float holdTime = 2f;        // Seconds to hold before exiting
    public string mainMenuSceneName = "MainMenu"; // Scene to load

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
        // Re-acquire controller if invalid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (!controller.isValid)
            return;

        // ---------- SNAP TURN ----------
        Vector2 axis;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis))
        {
            // Left swipe
            if (axis.x < -swipeThreshold && !leftSwipe)
            {
                SnapTurn(-snapTurnAngle);
                leftSwipe = true;
            }
            else if (axis.x > -swipeThreshold)
                leftSwipe = false;

            // Right swipe
            if (axis.x > swipeThreshold && !rightSwipe)
            {
                SnapTurn(snapTurnAngle);
                rightSwipe = true;
            }
            else if (axis.x < swipeThreshold)
                rightSwipe = false;
        }

        // ---------- EXIT TO MAIN MENU ----------
        bool axisClick = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out axisClick) && axisClick)
        {
            axisHoldTimer += Time.deltaTime;
            if (axisHoldTimer >= holdTime)
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
        else
        {
            axisHoldTimer = 0f;
        }
    }


    void SnapTurn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }


}

