using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlLever : MonoBehaviour
{
    [SerializeField] private GameObject Hand;
    [SerializeField] private float verticalSpeed = 5f;
    
    private Rigidbody playerRB;
    private DepthLeverAnimation leverAnimation;
    // Negative X rotation is up and postive x rotation is down
    public float xDeadzone = 10;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        leverAnimation = GameObject.Find("AnimationController").GetComponent<DepthLeverAnimation>();
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
            transform.Translate(Vector3.up * Time.deltaTime * verticalSpeed);
            leverAnimation.forwardLever();
        }
        else if (180 > handXRotation && handXRotation > xDeadzone)
        {
            transform.Translate(Vector3.down * Time.deltaTime * verticalSpeed);
            leverAnimation.backwardLever();
        }
        else
        {
            leverAnimation.resetLever();
        }
    }
}
