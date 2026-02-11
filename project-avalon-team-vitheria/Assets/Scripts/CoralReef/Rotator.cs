using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Editable speed in the Inspector
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        // Rotates the object around the Y axis (Green axis)
        // Time.deltaTime ensures it spins smoothly at the same speed on all computers
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}