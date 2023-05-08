using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed of rotation in degrees per second

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // Rotate the object on the X axis
    }
}