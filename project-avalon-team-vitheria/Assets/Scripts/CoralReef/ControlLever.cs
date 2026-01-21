using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlLever : MonoBehaviour
{
    [SerializeField] private GameObject Lever;
    [SerializeField] private GameObject Hand;
    [SerializeField] private float ySpeed = 5;
    // Negative X rotation is up and postive x rotation is down
    public float xDeadzone = 10;

    void LateUpdate()
    {
        controlDepth();
    }

    void controlDepth()
    {
        float handXRotation = Hand.transform.localEulerAngles.x;
        if ( 180 < handXRotation && handXRotation < 360-xDeadzone)
        {
            transform.Translate(Vector3.up*Time.deltaTime*ySpeed);
        }
        else if (180 > handXRotation && handXRotation > xDeadzone)
        {
            transform.Translate(Vector3.down*Time.deltaTime*ySpeed);
        }
    }
}
