using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unstuck : MonoBehaviour
{
    private GameObject player;
    private Vector3 startLocation;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        startLocation = player.transform.position;
    }
    public void unstuck()
    {
        player.transform.position = startLocation;
    }
}
