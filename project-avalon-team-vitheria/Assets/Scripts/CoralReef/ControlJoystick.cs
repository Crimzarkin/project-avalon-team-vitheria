using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJoystick : MonoBehaviour
{
    public GameObject Joystick;
    public GameObject Hand;
    // Negative X rotation is up and postive X rotation is down
    public float xDeadzone = 20;
    // Negative Y rotation is left and postive Y rotation is right
    public float yDeadzone = 20;

    void LateUpdate()
    {
        controlRotation();
        controlMovement();
    }

    void controlMovement()
    {
        float handXRotation = Hand.transform.rotation.eulerAngles.x;
        if ( 180 < handXRotation && handXRotation < 360-xDeadzone)
        {
            transform.Translate(Vector3.up*Time.deltaTime*5);
        }
        else if (180 > handXRotation && handXRotation > xDeadzone)
        {
            transform.Translate(Vector3.down*Time.deltaTime*5);
        }
    }
    
    void controlRotation()
    {
        float handYRotation = Hand.transform.rotation.eulerAngles.y;
        if ( 180 < handYRotation && handYRotation < 360-xDeadzone)
        {

        }
        else if (180 > handYRotation && handYRotation > xDeadzone)
        {
            
        }        
    }
}
