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

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        RaycastHit hit;
        if (!rayInteractor.GetCurrentRaycastHit(out hit))
        {
            lastTriggerState = triggerPressed;
            return;
        }

        // If Trigger JUST pressed then select colour
        if (triggerPressed && !lastTriggerState)
        {
            ColorObject colorObj = hit.collider.GetComponent<ColorObject>();
            if (colorObj != null)
            {
                selectedColor = colorObj.color;
            }
        }

        // If Trigger HELD then paint
        if (triggerPressed)
        {
            PaintCanvas canvas = hit.collider.GetComponent<PaintCanvas>();
            if (canvas != null)
            {
                canvas.Paint(hit.point, selectedColor);
            }
        }

        lastTriggerState = triggerPressed;
    }
}
