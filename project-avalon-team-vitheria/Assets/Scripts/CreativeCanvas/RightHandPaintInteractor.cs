using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RightHandPaintInteractor : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    private InputDevice controller;
    private bool triggerPressed;
    private bool lastTriggerState;

    public static Color selectedColor = Color.black;

    private ColorObject currentHoveredColorObject;

    void Start()
    {   
        if (rayInteractor == null)
        {
            rayInteractor = GetComponent<XRRayInteractor>();
            if (rayInteractor == null)
            {
                Debug.LogError("No XRRayInteractor found on Right Hand Controller!");
            }
        }
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        Debug.Log("RightHandPaintInteractor started, controller valid: " + controller.isValid);
    }

    void Update()
    {
        // Re-acquire controller if invalid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Raycast hit
        RaycastHit hit;
        bool hasHit = rayInteractor.GetCurrentRaycastHit(out hit);

        if (hasHit)
        {
            // Highlight palette color objects
            ColorObject hitColorObj = hit.collider.GetComponent<ColorObject>();
            if (hitColorObj != currentHoveredColorObject)
            {
                if (currentHoveredColorObject != null)
                    currentHoveredColorObject.Highlight(false);

                if (hitColorObj != null)
                    hitColorObj.Highlight(true);

                currentHoveredColorObject = hitColorObj;
            }

            // Trigger just pressed? Select color
            if (triggerPressed && !lastTriggerState && hitColorObj != null)
            {
                selectedColor = hitColorObj.color;
                Debug.Log("Selected color: " + selectedColor);
            }

            // Trigger held? Paint on canvas
            PaintCanvas canvas = hit.collider.GetComponent<PaintCanvas>();
            if (triggerPressed && canvas != null)
            {
                canvas.Paint(hit.point, selectedColor);
            }
        }
        else
        {
            // No hit: remove highlight
            if (currentHoveredColorObject != null)
            {
                currentHoveredColorObject.Highlight(false);
                currentHoveredColorObject = null;
            }
        }

        lastTriggerState = triggerPressed;
    }
}
