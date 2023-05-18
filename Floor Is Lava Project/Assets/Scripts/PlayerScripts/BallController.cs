using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public int lifeCount = 3;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    private Rigidbody rigidBody;

    public UIManager uiManager;
    public GameObject particleEffect1;
    public GameObject particleEffect2;

    public Transform CheckPoint;
    private MeshRenderer playerVisible;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        playerVisible = GetComponent<MeshRenderer>();
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
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBody.AddForce(Vector3.right * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBody.AddForce(-Vector3.right * speed);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            rigidBody.AddForce(Vector3.forward * speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rigidBody.AddForce(-Vector3.forward * speed);
        }

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
    }
}