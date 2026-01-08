using UnityEngine;

public class StationaryXRGravity : MonoBehaviour
{
    public float gravity = -9.81f;
    private CharacterController controller;
    private float yVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && yVelocity < 0)
            yVelocity = -2f;

        yVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}
