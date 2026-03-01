using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class GrabObject : MonoBehaviour
{
    private float axisHoldTimer = 0f;
    public float axisHoldTime = 2f;
    public string mainMenuSceneName = "MainMenu";
    public GameObject tutorialPanel;

    private bool tutorialOpen = false;

    public Transform handPosition;
    public GameObject heldObject = null;
    public Transform XRRig;
    public Material highlightMaterial;

    public float grabRange = 4.0f;
    public float teleportRange = 100.0f;
    public float snapTurnAngle = 45f; // Angle to turn right when trigger pressed

    private Material originalMaterial = null;
    private GameObject lastHighlighted = null;

    private InputDevice controller;
    private bool buttonPreviouslyPressed = false;
    private bool triggerPreviouslyPressed = false;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);


        // Grab / Drop / Teleport Using 2D Axis
        bool buttonPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonPressed))
        {
            if (buttonPressed)
            {
                axisHoldTimer += Time.deltaTime;

                // If held long enough → Load Main Menu
                if (axisHoldTimer >= axisHoldTime)
                {
                    LoadMainMenu();
                    return;
                }
            }
            else
            {
                // If released before hold time → Perform normal grab logic
                if (axisHoldTimer > 0f && axisHoldTimer < axisHoldTime)
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, grabRange))
                    {
                        GameObject hitObject = hit.collider.gameObject;

                        if (hitObject.CompareTag("Grabbable"))
                        {
                            grabRange = 4.0f;
                            if (heldObject == null)
                                AttemptPickup(hitObject);
                            else
                                AttemptDrop();
                        }
                        else if (hitObject.CompareTag("Teleport"))
                        {
                            grabRange = teleportRange;
                            TeleportTo(hitObject.transform.position);
                        }
                        else if (hitObject.CompareTag("Plot"))
                        {
                            PlotScript plot = hitObject.GetComponent<PlotScript>();

                            if (heldObject != null && plot != null)
                            {
                                SoilItem soil = heldObject.GetComponent<SoilItem>();
                                if (soil != null)
                                {
                                    plot.ApplySoil(soil.soilMaterial);
                                    AttemptDrop();
                                    axisHoldTimer = 0f;
                                    return;
                                }

                                SeedItem seed = heldObject.GetComponent<SeedItem>();
                                if (seed != null && plot.isSoiled && !plot.hasSeed)
                                {
                                    plot.PlantSeed(seed.seedMaterial);
                                    AttemptDrop();
                                    axisHoldTimer = 0f;
                                    return;
                                }

                                WaterItem water = heldObject.GetComponent<WaterItem>();
                                if (water != null && plot.isSoiled && !plot.hasSeed && !plot.isWatered)
                                {
                                    plot.WaterPlot();
                                    AttemptDrop();
                                    axisHoldTimer = 0f;
                                    return;
                                }
                            }
                        }
                    }
                }

                axisHoldTimer = 0f;
            }
        }
        buttonPreviouslyPressed = buttonPressed;

        // Snap turn using trigger (right turn)
        bool triggerPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed && !triggerPreviouslyPressed)
        {
            SnapTurn(snapTurnAngle); // Turn right
        }
        triggerPreviouslyPressed = triggerPressed;

        HighlightObject();
    }

    void AttemptPickup(GameObject target)
    {
        heldObject = Instantiate(target, handPosition.position, handPosition.rotation);
        heldObject.transform.SetParent(handPosition);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        // Disable collider and make kinematic to avoid physics issues with raycasts
        /*heldObjectCollider = heldObject.GetComponent<Collider>();
        heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
        if (heldObjectCollider != null)
            heldObjectCollider.enabled = false;
        if (heldObjectRigidbody != null)
            heldObjectRigidbody.isKinematic = true;*/


        // Scale down to 40% of original size
        heldObject.transform.localScale = target.transform.localScale * 0.4f;

        // Restore original material if highlighted
        Renderer rend = heldObject.GetComponent<Renderer>();
        if (rend != null && originalMaterial != null)
            rend.material = originalMaterial;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }

    void AttemptDrop()
    {
        //Drop the held object once utilized
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            Destroy(heldObject);
            heldObject = null;
        }
    }

    void TeleportTo(Vector3 targetPos)
    {
        if (XRRig != null)
        {
            XRRig.position = targetPos;
        }
        else
        {
            Debug.LogWarning("XR Rig reference not set in GrabObject script!");
        }
    }

    void SnapTurn(float angle)
    {
        if (XRRig != null)
            XRRig.Rotate(Vector3.up, angle);
    }

    void HighlightObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, teleportRange))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Grabbable") || hitObject.CompareTag("Teleport"))
            {
                if (lastHighlighted != hitObject)
                {
                    ClearHighlight();
                    lastHighlighted = hitObject;

                    Renderer rend = hitObject.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        originalMaterial = rend.material;
                        rend.material = highlightMaterial;
                    }
                }
                return;
            }
        }

        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (lastHighlighted != null)
        {
            Renderer rend = lastHighlighted.GetComponent<Renderer>();
            if (rend != null && originalMaterial != null)
                rend.material = originalMaterial;

            lastHighlighted = null;
            originalMaterial = null;
        }
    }

    void ToggleTutorial()
    {
        tutorialOpen = !tutorialOpen;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(tutorialOpen);
            
        // Pause game while tutorial open
        //Time.timeScale = tutorialOpen ? 0f : 1f;
    }

    void LoadMainMenu()
    {
        //unpause game if paused
        //Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);
    }


}
