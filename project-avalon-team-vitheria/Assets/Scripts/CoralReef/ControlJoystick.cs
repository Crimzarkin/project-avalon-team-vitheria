using UnityEngine;

public class ControlJoystick : MonoBehaviour
{
    [SerializeField] private GameObject Hand;
    // Negative X rotation is up and postive X rotation is down
    [SerializeField] private float xDeadzone = 10;
    // Negative Y rotation is left and postive Y rotation is right
    [SerializeField] private float yDeadzone = 10;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f; 
    private Rigidbody playerRB;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        controlRotation();
        controlMovement();
    }

    private void controlMovement()
    {
        float handXRotation = Hand.transform.localEulerAngles.x;
        if (180 < handXRotation && handXRotation < 360 - xDeadzone)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        else if (180 > handXRotation && handXRotation > xDeadzone)
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
    }
    
    private void controlRotation()
    {
        float handYRotation = Hand.transform.localEulerAngles.y;
        if (yDeadzone < handYRotation && handYRotation < 180)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
        else if (180 < handYRotation && handYRotation < 360 - yDeadzone)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        }        
    }
}
