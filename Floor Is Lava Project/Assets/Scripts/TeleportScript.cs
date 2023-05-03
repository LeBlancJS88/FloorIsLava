using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportDestination; // Drag and drop the teleport destination object to this variable in the Inspector
    public GameObject particleEffect1; // Drag and drop the first particle effect object to this variable in the Inspector
    public GameObject particleEffect2; // Drag and drop the second particle effect object to this variable in the Inspector

    private bool playerIsInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            Invoke("EnableParticleEffect1", 1f); // Wait for 1 second and then enable particle effect 1
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            particleEffect1.SetActive(false); // Disable particle effect 1 when the player exits the trigger area
        }
    }

    private void EnableParticleEffect1()
    {
        particleEffect1.SetActive(true); // Enable particle effect 1 after 1 second delay
        Invoke("TeleportPlayer", 1f); // Wait for another 1 second and then teleport the player
    }

    private void TeleportPlayer()
    {
        if (playerIsInside)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = teleportDestination.position; // Teleport the player to the specified destination
            particleEffect1.SetActive(false); // Disable particle effect 1 after teleporting
            particleEffect2.SetActive(true); // Enable particle effect 2
        }
    }
}