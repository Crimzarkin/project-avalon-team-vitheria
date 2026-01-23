using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlLever : MonoBehaviour
{
    [SerializeField] private GameObject Lever;
    [SerializeField] private GameObject Hand;
    [SerializeField] private float verticalSpeed = 1f;
    private Rigidbody playerRB;
    // Negative X rotation is up and postive x rotation is down
    public float xDeadzone = 10;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    void LateUpdate()
    {
        controlDepth();
    }

    void controlDepth()
    {
        float handXRotation = Hand.transform.localEulerAngles.x;
        if ( 180 < handXRotation && handXRotation < 360-xDeadzone)
        {
            playerRB.AddForce(transform.up * verticalSpeed);
        }
        else if (180 > handXRotation && handXRotation > xDeadzone)
        {
            playerRB.AddForce(transform.up * -verticalSpeed);
        }
    }
}
