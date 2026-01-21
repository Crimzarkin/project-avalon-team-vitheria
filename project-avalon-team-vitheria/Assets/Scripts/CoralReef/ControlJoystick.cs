using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControlJoystick : MonoBehaviour
{
    [SerializeField] private GameObject Joystick;
    [SerializeField] private GameObject Hand;
    // Negative X rotation is up and postive X rotation is down
    [SerializeField] private float xDeadzone = 20;
    // Negative Y rotation is left and postive Y rotation is right
    [SerializeField] private float yDeadzone = 20;
    [SerializeField] private float movementSpeed = 300;
    [SerializeField] private float rotationSpeed = 500; 
    void LateUpdate()
    {
        controlRotation();
        controlMovement();
    }

    private void controlMovement()
    {
        float handXRotation = Hand.transform.rotation.eulerAngles.x;
        if (180 < handXRotation && handXRotation < 360 - xDeadzone)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        else if (180 > handXRotation && handXRotation > xDeadzone)
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
    }
    
    void controlRotation()
    {
        float handYRotation =  Hand.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
        Debug.Log(handYRotation);
        if (180 < handYRotation && handYRotation < 360 - yDeadzone)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        }
        else if (180 > handYRotation && handYRotation > yDeadzone)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }        
    }
}
