using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRExitToMainMenu_OculusGo : MonoBehaviour
{
    [Header("Settings")]
    public float holdTime = 2f;              // Seconds to hold thumbstick
    public string mainMenuSceneName = "MainMenu"; 
    public XRNode controllerNode = XRNode.RightHand; // Controller to track

    private float holdTimer = 0f;
    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (!controller.isValid)
            return;

        bool axisPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out axisPressed) && axisPressed)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTime)
            {
                LoadMainMenu();
            }
        }
        else
        {
            holdTimer = 0f; // Reset if released
        }
    }

    private void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}