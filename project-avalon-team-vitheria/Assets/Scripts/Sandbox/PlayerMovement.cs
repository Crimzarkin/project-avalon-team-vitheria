using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        //Make it move based on rotation of y axis of camera
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Camera.main.transform.forward * verticalInput * speed * Time.deltaTime);
        transform.Translate(Camera.main.transform.right * horizontalInput * speed * Time.deltaTime);
    }

}
