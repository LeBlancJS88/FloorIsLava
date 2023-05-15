using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private UIManager uiManager;
    private bool isActive = true;

    private void Start()
    {
        // Get the UIManager component
        uiManager = FindObjectOfType<UIManager>();

        // Enable the object on scene start
        gameObject.SetActive(true);

        // Set the initial rotation
        transform.rotation = Quaternion.Euler(Random.Range(0f, -90f), 0f, 0f);
    }

    private void Update()
    {
        // Rotate the object continuously along the Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Bolt"))
            {
                // Add 10 to the Score variable
                uiManager.score += 10;
            }
            else if (gameObject.CompareTag("Nut"))
            {
                // Add 1 to the Score Multiplier variable
                uiManager.scoreMultiplier += 1;
            }

            // Disable the object
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
