using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class CoralReefRController : MonoBehaviour
{
    public float mainMenuHoldTime = 2f; // Seconds to hold the thumbstick
    public string mainMenuSceneName = "MainMenu";

    private float axisHoldTimer = 0f;
    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        Debug.Log("CoralReefRController started, controller valid: " + controller.isValid);
    }

    void Update()
    {
        // Retry controller acquisition if invalid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Check primary 2D axis (thumbstick) press
        bool axisPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out axisPressed) && axisPressed)
        {
            axisHoldTimer += Time.deltaTime;

            if (axisHoldTimer >= mainMenuHoldTime)
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
        else
        {
            axisHoldTimer = 0f; // Reset timer if released
        }
    }
}