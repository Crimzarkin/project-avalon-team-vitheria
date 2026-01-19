using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ControlLever : MonoBehaviour
{
    public GameObject Lever;
    public GameObject Hand;
    // Negative X rotation is up and postive x rotation is down
    public float xDeadzone = 20;

    void LateUpdate()
    {
        controlDepth();
    }

    void controlDepth()
    {
        float handXRotation = Hand.transform.rotation.x;
        if (handXRotation < -xDeadzone)
        {
            transform.Translate(Vector3.up*Time.deltaTime*5);
        }
        else if (handXRotation > xDeadzone)
        {
            transform.Translate(Vector3.down*Time.deltaTime*5);
        }
    }
}
