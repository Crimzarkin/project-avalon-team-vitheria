using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class BeachMove : MonoBehaviour
{
    private float moveSpeed = 14.0f;
    float stepHeight = 1f;        // max height you can step up
    float floatDownSpeed = 3f;    // how fast you float down
    float groundCheckDistance = 0.1f;
    private float snapTurnAngle = 15f;

    private float triggerHoldTime = 0f;
    private float requiredHoldTime = 0.8f;

    private InputDevice controller;

    private bool triggerPressed = false;

    // Touchpad axis
    private Vector2 axis;
    private bool leftSwipe = false;
    private bool rightSwipe = false;
    private float axisHoldTimer = 0f;
    public float axisHoldTime = 2f;
    // Swipe Detection Threshold
    public float swipeThreshold = 0.6f;

    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // Keep controller valid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool buttonPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonPressed))
        {
            if (buttonPressed)
            {
                axisHoldTimer += Time.deltaTime;
                leftSwipe = false;
                rightSwipe = false;

                if (axisHoldTimer >= axisHoldTime)
                {
                    LoadMainMenu();
                    return;
                }
            }
            else
            {
                axisHoldTimer = 0f;
            }
        }
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
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPos = transform.position + direction * moveSpeed * Time.deltaTime;

        // step up check
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 0.6f)) //Check raycast coming from players body, not controller
        {
            // If there's a block in front, try stepping up
            Vector3 stepCheck = transform.position + Vector3.up * stepHeight;

            // Cast again from higher position
            if (!Physics.Raycast(stepCheck, direction, 0.6f))
            {
                // Free space above → step up
                targetPos.y += stepHeight;
            }
        }

        // float down
        if (!Physics.Raycast(targetPos + Vector3.up * 0.1f, Vector3.down, groundCheckDistance))
        {
            targetPos.y -= floatDownSpeed * Time.deltaTime;
        }

        transform.position = targetPos;
    }


    void SnapTurn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
