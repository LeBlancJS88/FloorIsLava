using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapController : MonoBehaviour
{
    public Camera[] cameraArray;
    public float switchInterval = 5f;
    public float rotationSpeed = 30f;

    private int currentCameraIndex = 0;
    private Quaternion[] cameraStartRotations;
    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private float switchTimer = 0f;
    private bool rotateRight = true; // Flag to indicate the direction of rotation

    private void Start()
    {
        // Store the initial rotation of each camera
        cameraStartRotations = new Quaternion[cameraArray.Length];
        for (int i = 0; i < cameraArray.Length; i++)
        {
            cameraStartRotations[i] = cameraArray[i].transform.rotation;
        }

        // Set the initial camera and rotation
        SwitchCamera(currentCameraIndex);
    }

    private void Update()
    {
        // Update the timer
        switchTimer += Time.deltaTime;

        // Check if it's time to switch cameras
        if (switchTimer >= switchInterval)
        {
            switchTimer = 0f;
            SwitchToNextCamera();
            RandomizeRotationDirection();
        }

        // Rotate the current camera
        RotateCamera();
    }

    private void SwitchToNextCamera()
    {
        // Reset the previous camera's rotation
        cameraArray[currentCameraIndex].transform.rotation = cameraStartRotations[currentCameraIndex];

        // Switch to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % cameraArray.Length;
        SwitchCamera(currentCameraIndex);
    }

    private void SwitchCamera(int index)
    {
        // Activate the specified camera
        for (int i = 0; i < cameraArray.Length; i++)
        {
            cameraArray[i].gameObject.SetActive(i == index);
        }

        // Set the target rotation based on the current camera's initial rotation
        initialRotation = cameraArray[index].transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0f, rotateRight ? 180f : -180f, 0f); // Rotate 180 degrees on the y-axis
    }

    private void RotateCamera()
    {
        // Calculate the new rotation
        Quaternion newRotation = Quaternion.RotateTowards(cameraArray[currentCameraIndex].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the rotation
        cameraArray[currentCameraIndex].transform.rotation = newRotation;
    }

    private void RandomizeRotationDirection()
    {
        rotateRight = Random.value < 0.5f; // Randomly set the direction to either right or left
    }
}
