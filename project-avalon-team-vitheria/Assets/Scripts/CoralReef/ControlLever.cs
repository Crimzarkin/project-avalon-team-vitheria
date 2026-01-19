using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ControlLever : MonoBehaviour
{
    public GameObject Lever;
    public GameObject Hand;
    public GameObject Player;
    // Negative X rotation is up and postive x rotation is down
    public float xDeadzone = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        throttle();Player.transform.Translate(Vector3.down*Time.deltaTime*5);
    }
    void throttle()
    {
        float handXRotation = Hand.transform.rotation.x;
        if (handXRotation < -xDeadzone)
        {
            Player.transform.Translate(Vector3.up*Time.deltaTime*5);
        }
        else if (handXRotation > xDeadzone)
        {
            Player.transform.Translate(Vector3.down*Time.deltaTime*5);
        }
    }
}
