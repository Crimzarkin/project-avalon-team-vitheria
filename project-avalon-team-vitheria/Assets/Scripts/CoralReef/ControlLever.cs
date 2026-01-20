using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.Interaction.Toolkit;

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
}
