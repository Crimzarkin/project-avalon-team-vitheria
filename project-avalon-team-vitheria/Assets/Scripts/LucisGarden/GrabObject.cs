using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabObject : MonoBehaviour
{
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
    }

    void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Grab / Drop / Teleport
        bool buttonPressed = false;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonPressed) && buttonPressed && !buttonPreviouslyPressed)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, grabRange))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Grabbable"))
                {
                    grabRange = 4.0f; // Reset range
                    if (heldObject == null)
                        AttemptPickup(hitObject);
                    else
                        AttemptDrop();
                }
                else if (hitObject.CompareTag("Teleport"))
                {
                    grabRange = teleportRange; // Extend range for teleport
                    TeleportTo(hitObject.transform.position);
                }
                else if (hitObject.CompareTag("Plot"))
                {
                    PlotScript plot = hitObject.GetComponent<PlotScript>();
                    
                    if (heldObject != null && plot != null)
                    {
                        // SOIL
                        SoilItem soil = heldObject.GetComponent<SoilItem>();
                        if (soil != null)
                        {
                            plot.ApplySoil(soil.soilMaterial);
                            AttemptDrop();
                            return;
                        }

                        // SEEDS
                        SeedItem seed = heldObject.GetComponent<SeedItem>();
                        if (seed != null && plot.isSoiled)
                        {
                            plot.PlantSeed(seed.seedMaterial);
                            AttemptDrop();
                            return;
                        }
                    }
                }
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

        // Scale down to half
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
}
