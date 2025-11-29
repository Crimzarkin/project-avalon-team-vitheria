using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildSystem : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject blockObject = null;

    public bool inventoryClosed = true;
    public Transform parent;

    private Color lastColor;
    public Color highlightedColor;
    private GameObject lastHightlightedBlock;

    // Block placement
    private bool ZoneObject = false;
    private float RaycastLength = 5.0f;

    // XR Input
    private InputDevice controller;
    private bool triggerPressed = false;
    private bool lastTriggerPressed = false;

    // Double-click timing
    public float doublePressWindow = 0.40f;  
    private float lastPressTime = 0f;

    // <-- Missing variable added
    private bool waitingForSecondPress = false;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Update()
    {
        if (!controller.isValid)
            controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Detect rising edge
        if (triggerPressed && !lastTriggerPressed)
        {
            float timeSinceLastPress = Time.time - lastPressTime;

            // Second press → double action
            if (waitingForSecondPress && timeSinceLastPress <= doublePressWindow)
            {
                waitingForSecondPress = false;
                if (inventoryClosed)
                    DestroyBlock();
            }
            else
            {
                // First press
                waitingForSecondPress = true;
                lastPressTime = Time.time;
            }
        }

        // If single press timer expired → single action
        if (waitingForSecondPress && Time.time - lastPressTime > doublePressWindow)
        {
            waitingForSecondPress = false;

            if (inventoryClosed && blockObject != null)
                BuildBlock(blockObject);
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
                spawnPosition = hitInfo.point + hitInfo.normal;
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
                    lastColor = lastHightlightedBlock.GetComponent<Renderer>().material.color;
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = highlightedColor;
                }
                else if (lastHightlightedBlock != hitInfo.transform.gameObject)
                {
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = lastColor;
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                    lastColor = lastHightlightedBlock.GetComponent<Renderer>().material.color;
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = highlightedColor;
                }

                return;
            }
        }

        // If no block is hit → restore highlight
        if (lastHightlightedBlock != null)
        {
            lastHightlightedBlock.GetComponent<Renderer>().material.color = lastColor;
            lastHightlightedBlock = null;
        }
    }

    public void changeBlock(GameObject block)
    {
        blockObject = block;
    }
}
