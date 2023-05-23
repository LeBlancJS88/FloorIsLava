using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip boltPickupSound;
    public AudioClip nutPickupSound;
    public int lifeCount = 3;
    public float speed = 25f;
    public float rotationSpeed = 5f;
    private Rigidbody rigidBody;

    public UIManager uiManager;
    public GameObject particleEffect1;
    public GameObject particleEffect2;

    public Transform CheckPoint;
    private MeshRenderer playerVisible;

    private bool canMove = true; // Flag to control whether input should be processed or not

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        playerVisible = GetComponent<MeshRenderer>();
        canMove = true; // Enable input after respawn

        ResetBallSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            CheckPoint = other.GetComponent<CheckpointHandler>().checkPoint;
        }

        if (other.CompareTag("Goal"))
        {
            uiManager.timeRunning = false;
            uiManager.SetBestTime();
            uiManager.ActivateVictoryPanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove || uiManager.isVictoryPanelActive)
        {
            return; // Skip movement code if canMove is false or victory panel is active
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        rigidBody.AddForce(movement);

        Vector3 velocity = rigidBody.velocity;
        velocity.y = 0f; // Ignore vertical velocity

        if (velocity.magnitude > 0.1f) // Check if velocity is above a certain threshold
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, velocity.normalized); // Calculate rotation axis using cross product
            Quaternion targetRotation = Quaternion.LookRotation(velocity); // Calculate target rotation based on velocity

            Quaternion rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis) * rigidBody.rotation; // Rotate the ball around the rotation axis
            rigidBody.MoveRotation(rotation); // Apply the rotation to the rigidbody
        }
    }

    public void PlayBoltPickupSound()
    {
        if (audioSource != null && boltPickupSound != null)
        {
            audioSource.clip = boltPickupSound;
            audioSource.Play();
        }
    }

    public void PlayNutPickupSound()
    {
        if (audioSource != null && nutPickupSound != null)
        {
            audioSource.clip = nutPickupSound;
            audioSource.Play();
        }
    }
    internal void PlayerDeath(Vector3 deathPosition)
    {
        playerVisible.enabled = false;

        GameObject effectHolder = new GameObject("EffectHolder");
        effectHolder.transform.position = deathPosition;


        GameObject effect1 = Instantiate(particleEffect1, effectHolder.transform);
        GameObject effect2 = Instantiate(particleEffect2, effectHolder.transform);

        effect1.SetActive(true);

        Destroy(effectHolder, 3f);

        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(3f);

        lifeCount -= 1;
        uiManager.LoseLife();
        transform.position = CheckPoint.position;

        playerVisible.enabled = true;
        canMove = true; // Enable input after respawn
        ResetBallSpeed();

    }

    private void ResetBallSpeed()
    {
        rigidBody.velocity = Vector3.zero; // Reset the ball's velocity to zero
        rigidBody.angularVelocity = Vector3.zero; // Reset the ball's angular velocity to zero
    }
}