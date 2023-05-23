using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportDestination; // Drag and drop the teleport destination object to this variable in the Inspector
    public GameObject particleEffect1; // Drag and drop the first particle effect object to this variable in the Inspector
    public GameObject particleEffect2; // Drag and drop the second particle effect object to this variable in the Inspector

    private bool playerIsInside = false;
    private Coroutine disableParticleEffect2Coroutine;

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
        Invoke("TeleportPlayer", 2.2f); // Wait for another 2 seconds and then teleport the player
    }

    private void TeleportPlayer()
    {
        if (playerIsInside)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = teleportDestination.position; // Teleport the player to the specified destination
            particleEffect1.SetActive(false); // Disable particle effect 1 after teleporting
            particleEffect2.SetActive(true); // Enable particle effect 2
        }        // Disable particle effect 2 after 10 seconds

        disableParticleEffect2Coroutine = StartCoroutine(DisableParticleEffect2AfterDelay(10f));
    }
    private IEnumerator DisableParticleEffect2AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        particleEffect2.SetActive(false);
    }

    private void OnDestroy()
    {
        // Stop the coroutine if the script is destroyed (e.g., when the object is disabled or destroyed)
        if (disableParticleEffect2Coroutine != null)
        {
            StopCoroutine(disableParticleEffect2Coroutine);
        }
    }
}