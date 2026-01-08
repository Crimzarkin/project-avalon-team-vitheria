using UnityEngine;
using UnityEngine.XR;

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
            if (axis.x < -swipeThreshold && !leftSwipe)
            {
                SnapTurn(-snapTurnAngle);
                leftSwipe = true;
            }
            else if (axis.x > -swipeThreshold)
            {
                leftSwipe = false;
            }

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
    }

    void MoveForward()
    {
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0f;

        characterController.Move(
            direction.normalized * moveSpeed * Time.deltaTime
        );
    }

    void SnapTurn(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
}
