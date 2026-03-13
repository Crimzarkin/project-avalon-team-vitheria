using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRExitToMainMenu : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand; // Which controller to check
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
                Debug.Log("Loading main menu...");
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
        else
        {
            holdTimer = 0f; 
        }
    }
}