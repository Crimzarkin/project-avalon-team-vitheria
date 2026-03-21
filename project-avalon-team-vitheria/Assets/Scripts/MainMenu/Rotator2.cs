using UnityEngine;

public class Rotator2 : MonoBehaviour
{
    // Editable speed in the Inspector
    [SerializeField] private float rotationSpeed = 100f;

    void LateUpdate()
    {
        // Rotates the object around the Y axis (Green axis)
        // Time.deltaTime ensures it spins smoothly at the same speed on all computers
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}