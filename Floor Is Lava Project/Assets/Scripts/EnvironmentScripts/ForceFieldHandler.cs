using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ForceFieldHandler : MonoBehaviour
{
    [SerializeField] private GameObject archForceField;
    [SerializeField] private GameObject[] redLights;
    [SerializeField] private TMP_Text hintText;

    [SerializeField] private bool isHintDisplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Display hint text
            hintText.gameObject.SetActive(true);
            isHintDisplayed = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check for Space key press to disable the ArchForceField
            if (Input.GetKey(KeyCode.Space))
            {
                archForceField.SetActive(false);

                foreach (GameObject lightObject in redLights)
                {
                    Light pointLight = lightObject.GetComponent<Light>();
                    pointLight.color = Color.green;
                }

                // Disable hint text
                hintText.gameObject.SetActive(false);
                isHintDisplayed = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable hint text if it was displayed
            if (isHintDisplayed)
            {
                hintText.gameObject.SetActive(false);
                isHintDisplayed = false;
            }

            archForceField.SetActive(true);

            foreach (GameObject lightObject in redLights)
            {
                Light pointLight = lightObject.GetComponent<Light>();
                pointLight.color = Color.red;
            }

        }
    }
}