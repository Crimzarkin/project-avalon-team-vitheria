using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class BuildSystem : MonoBehaviour
{
    public Transform shootingPoint;
    public bool inventoryClosed = true;
    
    // Block placement
    private bool ZoneObject = false;
    private float RaycastLength = 5.0f;
    public GameObject blockObject = null;
    public Transform parent;
    private Color lastColor;
    public Color highlightedColor;
    private GameObject lastHightlightedBlock;

    // XR Input
    private InputDevice controller;
    private bool triggerPressed = false;
    private bool lastTriggerPressed = false;

    // Double-click timing
    public float doublePressWindow = 0.40f;  
    private float lastPressTime = 0f;

    // <-- Missing variable added
    private bool waitingForSecondPress = false;
    private float blockSize = 0.5f;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    public void Update()
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
        Vector3 origin = shootingPoint.position + shootingPoint.forward * 0.01f;
        int mask = LayerMask.GetMask("Block", "Terrain");

        if (!Physics.Raycast(origin, shootingPoint.forward, out RaycastHit hitInfo, RaycastLength, mask, QueryTriggerInteraction.Ignore))
            return;

        if (!ZoneObject)
            return;

        Vector3 spawnPosition;
        Terrain terrain = hitInfo.collider.GetComponent<Terrain>();

        if (terrain != null)
        {
            float terrainHeight = terrain.SampleHeight(hitInfo.point);
            float y = Mathf.Ceil(terrainHeight / blockSize) * blockSize;

            Vector3Int grid = new Vector3Int(
                Mathf.RoundToInt(hitInfo.point.x / blockSize),
                Mathf.RoundToInt(y / blockSize),
                Mathf.RoundToInt(hitInfo.point.z / blockSize)
            );
            spawnPosition = (Vector3)grid * blockSize;
        }
        else if (hitInfo.transform.CompareTag("Block"))
        {
            Vector3Int blockGrid = Vector3Int.RoundToInt(hitInfo.transform.position / blockSize);
            Vector3 n = hitInfo.normal;

            if (n.sqrMagnitude < 0.01)
                return;

            if (Mathf.Abs(n.x) > Mathf.Abs(n.y) && Mathf.Abs(n.x) > Mathf.Abs(n.z))
                n = new Vector3(Mathf.Sign(n.x), 0, 0);
            else if (Mathf.Abs(n.y) > Mathf.Abs(n.x) && Mathf.Abs(n.y) > Mathf.Abs(n.z))
                n = new Vector3(0, Mathf.Sign(n.y), 0);
            else
                n = new Vector3(0, 0, Mathf.Sign(n.z));
            Vector3Int normalGrid = Vector3Int.RoundToInt(n);
            Vector3Int targetGrid = blockGrid + normalGrid;
            spawnPosition = (Vector3)targetGrid * blockSize;
        }
        else
        {
            return;
        }

        spawnPosition = new Vector3(
            Mathf.Round(spawnPosition.x / blockSize) * blockSize,
            Mathf.Round(spawnPosition.y / blockSize) * blockSize,
            Mathf.Round(spawnPosition.z / blockSize) * blockSize
        );

        GameObject blockInstance = Instantiate(block, spawnPosition, Quaternion.identity, parent);
        blockInstance.tag = "Block";
    }

    public void ResetBlocks()
    {   
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            Destroy(block);
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

        // If no block is hit, restore color
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