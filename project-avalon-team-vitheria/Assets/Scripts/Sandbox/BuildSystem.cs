using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BuildSystem : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject blockObject = null;

    public bool inventoryClosed = true;
    public Transform parent;

    private Color lastColor;
    public Color highlightedColor;
    private GameObject lastHightlightedBlock;

    private bool ZoneObject = false;
    private float RaycastLength = 5.0f;

    // XR Input
    private InputDevice controller;
    private bool triggerPressed = false;
    private bool lastTriggerPressed = false;

    // Double-click timing
    public float doublePressWindow = 0.35f;  
    private float lastPressTime = 0f;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Detect RISING EDGE (pressed once)
        if (triggerPressed && !lastTriggerPressed)
        {
            float timeSinceLastPress = Time.time - lastPressTime;

            if (timeSinceLastPress <= doublePressWindow)
            {
                // DOUBLE PRESS → Destroy block
                if (inventoryClosed)
                    DestroyBlock();
            }
            else
            {
                // SINGLE PRESS → Build block
                if (inventoryClosed && blockObject != null)
                    BuildBlock(blockObject);
            }

            lastPressTime = Time.time;
        }

        lastTriggerPressed = triggerPressed;

        HighlightBlock();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
            ZoneObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
            ZoneObject = false;
    }

    void BuildBlock(GameObject block)
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo, RaycastLength) &&
            ZoneObject)
        {
            Vector3 spawnPosition;

            if (hitInfo.transform.CompareTag("Block"))
            {
                spawnPosition = hitInfo.point + hitInfo.normal * 0.5f;
                spawnPosition = new Vector3(
                    Mathf.RoundToInt(spawnPosition.x),
                    Mathf.RoundToInt(spawnPosition.y),
                    Mathf.RoundToInt(spawnPosition.z)
                );
            }
            else
            {
                spawnPosition = new Vector3(
                    Mathf.RoundToInt(hitInfo.point.x),
                    Mathf.RoundToInt(hitInfo.point.y),
                    Mathf.RoundToInt(hitInfo.point.z)
                );
            }

            Instantiate(block, spawnPosition, Quaternion.identity, parent);
        }
    }

    void DestroyBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo, RaycastLength))
        {
            if (hitInfo.transform.CompareTag("Block"))
                Destroy(hitInfo.transform.gameObject);
        }
    }

    void HighlightBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo, RaycastLength))
        {
            if (hitInfo.transform.CompareTag("Block"))
            {
                if (lastHightlightedBlock == null)
                {
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                    lastColor = hitInfo.transform.gameObject.GetComponent<Renderer>().material.color;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                }
                else if (lastHightlightedBlock != hitInfo.transform.gameObject)
                {
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = lastColor;
                    lastColor = hitInfo.transform.gameObject.GetComponent<Renderer>().material.color;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                }
            }
        }
    }

    public void changeBlock(GameObject block)
    {
        blockObject = block;
    }
}
