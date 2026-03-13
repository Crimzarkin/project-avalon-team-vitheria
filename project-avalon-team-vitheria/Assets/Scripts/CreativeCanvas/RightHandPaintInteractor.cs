using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


public class RightHandPaintInteractor : MonoBehaviour
{
    private ToolButton currentHoveredToolButton;
    public XRRayInteractor rayInteractor;

    private InputDevice controller;
    private bool triggerPressed;
    private bool lastTriggerState;

    public static Color selectedColor = Color.black;

    private ColorObject currentHoveredColorObject;

    // --------- Main Menu Hold Variables ----------
    public float mainMenuHoldTime = 2f;   // Time to hold thumbstick to return
    private float axisHoldTimer = 0f;
    public string mainMenuSceneName = "MainMenu";

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
        // Retry controller acquisition if invalid
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // ---------- Trigger Button for Painting ----------
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Raycast hit
        RaycastHit hit;
        bool hasHit = rayInteractor.GetCurrentRaycastHit(out hit);

        if (hasHit)
        {
            ToolButton hitTool = hit.collider.GetComponent<ToolButton>();
            if (hitTool != currentHoveredToolButton)
            {
                if (currentHoveredToolButton != null)
                    currentHoveredToolButton.Highlight(false);

                if (hitTool != null)
                    hitTool.Highlight(true);

                currentHoveredToolButton = hitTool;
            }

            ToolButton tool = hit.collider.GetComponent<ToolButton>();
            if (triggerPressed && !lastTriggerState && tool != null)
            {
                tool.Activate();
                return;
            }
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

            // If Trigger just pressed → select color
            if (triggerPressed && !lastTriggerState && hitColorObj != null)
            {
                selectedColor = hitColorObj.color;
                Debug.Log("Selected color: " + selectedColor);
            }

            // If Trigger held → paint on canvas
            PaintCanvas canvas = hit.collider.GetComponent<PaintCanvas>();
            if (triggerPressed && canvas != null)
            {
                canvas.Paint(hit, selectedColor);
            }
        }
        else
        {
            // Remove highlight
            if (currentHoveredColorObject != null)
            {
                currentHoveredColorObject.Highlight(false);
                currentHoveredColorObject = null;
            }
            if (currentHoveredToolButton != null)
            {
                currentHoveredToolButton.Highlight(false);
                currentHoveredToolButton = null;
            }
        }

        lastTriggerState = triggerPressed;

        // ---------- Hold Thumbstick to Return to Main Menu ----------
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
            axisHoldTimer = 0f;
        }
    }
}