using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject blockObject = null;

    public Transform parent;

    public Color normalColor;
    public Color highlightedColor;

    GameObject lastHightlightedBlock;

    public bool ZoneObject = false;
    private float RaycastLength = 5.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && blockObject != null)
        {
            BuildBlock(blockObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            ZoneObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            ZoneObject = false;
        }
    }

    void BuildBlock(GameObject block)
    {
        if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo, RaycastLength) && ZoneObject)
        {

            if(hitInfo.transform.tag == "Block") //Tag to check if we hit a 'block' object
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x/2), Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z /2));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x), Mathf.RoundToInt(hitInfo.point.y), Mathf.RoundToInt(hitInfo.point.z));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);
            }
        }
    }

    void DestroyBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block") //Another tag check
            {
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

    void HighlightBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block") //Another tag check
            {
                if(lastHightlightedBlock == null)
                {
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                }
                else if (lastHightlightedBlock != hitInfo.transform.gameObject)
                {
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = normalColor;
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