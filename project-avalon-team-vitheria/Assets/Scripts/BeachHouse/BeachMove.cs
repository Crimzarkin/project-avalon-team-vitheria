using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class BeachMove : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float snapTurnAngle = 15f;

    private float triggerHoldTime = 0f;
    private float requiredHoldTime = 0.8f;

    private InputDevice controller;
    private bool triggerPressed = false;

    private Vector2 axis;
    private bool leftSwipe = false;
    private bool rightSwipe = false;
    public float swipeThreshold = 0.6f;

    private CharacterController characterController;

    // ----- Main Menu Hold Variables -----
    public float axisHoldTime = 2f;
    private float axisHoldTimer = 0f;
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // ---------- MOVEMENT ----------
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed)
        {
            triggerHoldTime += Time.deltaTime;

            if (triggerHoldTime >= requiredHoldTime)
                MoveForward();
        }
        else
        {
            triggerHoldTime = 0f;
        }

        // ---------- SNAP TURN ----------
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis))
        {
            // Snap turn left
            if (axis.x < -swipeThreshold && !leftSwipe)
            {
                SnapTurn(-snapTurnAngle);
                leftSwipe = true;
            }
            else if (axis.x > -swipeThreshold)
            {
                leftSwipe = false;
            }

            // Snap turn right
            if (axis.x > swipeThreshold && !rightSwipe)
            {
                SnapTurn(+snapTurnAngle);
                rightSwipe = true;
            }
            else if (axis.x < swipeThreshold)
            {
                rightSwipe = false;
            }
        }

        // ---------- HOLD STICK BUTTON TO RETURN TO MAIN MENU ----------
        bool axisPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out axisPressed) && axisPressed)
        {
            axisHoldTimer += Time.deltaTime;

            if (axisHoldTimer >= axisHoldTime)
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
        else
        {
            axisHoldTimer = 0f;
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = 7f;
        transform.position = pos;
    }

    void MoveForward()
    {
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0f;

        characterController.Move(direction.normalized * moveSpeed * Time.deltaTime);
    }

    void SnapTurn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
}