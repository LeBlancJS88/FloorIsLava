using UnityEngine;

public class DeathloopHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BallController>().PlayerDeath(other.ClosestPoint(transform.position));
        }
    }
}