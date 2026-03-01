using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuHandler : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand;
    public float rayLength = 10f;

    private InputDevice controller;

    private Dictionary<string, string> sceneNameMap = new Dictionary<string, string>()
    {
        { "Lucis Garden", "LucisGarden" },
        { "Sand Box", "SandBox" },
        { "Creative Canvas", "CreativeCanvas" },
        { "Beach", "BeachHouse" },
        { "Camping", "CampingScene" },
        { "Stargazing", "Stargazing" },
        { "Coral Reef", "CoralReef" }
    };

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(controllerNode);

        bool triggerPressed = false;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                Button button = hit.collider.GetComponent<Button>();
                if (button != null)
                {
                    Text text = button.GetComponentInChildren<Text>();
                    if (text != null)
                    {
                        string displayName = text.text.Trim();

                        if (sceneNameMap.TryGetValue(displayName, out string sceneName))
                        {
                            Debug.Log($"Loading scene: {sceneName} from button text: {displayName}");
                            SceneManager.LoadScene(sceneName);
                        }
                        else
                        {
                            Debug.LogWarning($"Scene name mapping not found for button text: {displayName}");
                        }
                    }
                }
            }
        }
    }
}