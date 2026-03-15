using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f; 
    private DepthLeverAnimation leverAnimation;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
         leverAnimation = GameObject.Find("AnimationController").GetComponent<DepthLeverAnimation>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.A)) transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        if (Input.GetKey(KeyCode.D)) transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.F))
        {
            transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
            leverAnimation.forwardLever();
        } 
        else if (Input.GetKey(KeyCode.C)) 
        {
            transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
            leverAnimation.backwardLever();
        }
        else
        {
            leverAnimation.resetLever();
        }
    }
}
