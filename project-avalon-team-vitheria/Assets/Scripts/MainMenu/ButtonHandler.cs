using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRButtonSceneLoader : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand;
    public float rayLength = 10f;

    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(controllerNode);

        // Check if trigger is pressed
        bool triggerPressed = false;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                // Check if the object has a Button component
                Button button = hit.collider.GetComponent<Button>();
                if (button != null)
                {
                    // Look for Text component on the button
                    Text text = button.GetComponentInChildren<Text>();
                    if (text != null && !string.IsNullOrEmpty(text.text))
                    {
                        // Load scene with the name from the text
                        SceneManager.LoadScene(text.text);
                    }
                    else
                    {
                        Debug.LogWarning("Button has no Text component or scene name is empty!");
                    }
                }
            }
        }
    }
}