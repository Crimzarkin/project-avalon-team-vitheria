using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickAnimation : MonoBehaviour
{
    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject Joystick;
    private float normalizedHorizontal;
    private float normalizedVertical;
    private float Deadzone = 10;
    private float joystickDeflection = 50;
    // Update is called once per frame
    void LateUpdate()
    {
        verticallMovement();
        HorizontalMovement();
    }

    private void verticallMovement()
    {
        float xRotation = Hand.transform.localEulerAngles.x;
        if (360 - Deadzone < xRotation && xRotation < Deadzone)
        {
            normalizedVertical = 0;
        }
        else if (180 < xRotation && xRotation < 360 - Deadzone)
        {
            normalizedVertical = (360 - xRotation)/180;
        }
        else if (Deadzone < xRotation && xRotation < 180)
        {
            normalizedVertical = -xRotation/180;
        }

        Joystick.transform.Rotate(Vector3.right * normalizedVertical * joystickDeflection);
    }
        private void HorizontalMovement()
    {
        float yRotation = Hand.transform.localEulerAngles.y;
        if (360 - Deadzone < yRotation && yRotation < Deadzone)
        {
            normalizedHorizontal = 0;
        }
        else if (180 < yRotation && yRotation < 360 - Deadzone)
        {
            normalizedHorizontal = (360 - yRotation)/180;
        }
        else if (Deadzone < yRotation && yRotation < 180)
        {
            normalizedHorizontal = -yRotation/180;
        }

        Joystick.transform.Rotate(Vector3.forward * normalizedHorizontal * joystickDeflection);
    }
}
