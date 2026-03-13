using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRExitToMainMenu : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand; // controller
    public string mainMenuSceneName = "MainMenu";    // Scene to load
    public float holdTime = 2f;                      // Seconds to hold

    private float holdTimer = 0f;
    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
        Debug.Log($"VRExitToMainMenu started. Controller valid: {controller.isValid}");
    }

    void Update()
    {
        // Retry controller if invalid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (!controller.isValid)
            return; // No valid controller

        bool axisPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out axisPressed) && axisPressed)
        {
            holdTimer += Time.deltaTime;
            // Optional debug
            // Debug.Log($"Thumbstick held: {holdTimer:F2}s");

            if (holdTimer >= holdTime)
            {
                Debug.Log("Loading main menu...");
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
        else
        {
            holdTimer = 0f; // Reset timer when released
        }
    }
}