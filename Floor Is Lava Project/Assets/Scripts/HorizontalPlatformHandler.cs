using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatformHandler : MonoBehaviour
{
    [SerializeField]
    private WayPointPath _waypointPath; // Reference to a WayPointPath object that contains a set of waypoints

    [SerializeField]
    private float _speed; // Speed of movement in units per second

    private int _targetWaypointIndex; // Index of the current target waypoint in the waypoint path

    private Transform _previousWaypoint; // Reference to the previous waypoint in the path
    private Transform _targetWaypoint; // Reference to the current target waypoint in the path

    private float _timeToWaypoint; // Time required to reach the current target waypoint
    private float _elapsedTime; // Time elapsed since starting to move towards the current target waypoint

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint(); // Set the initial target waypoint to the first waypoint in the path
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _elapsedTime += Time.deltaTime; // Update the elapsed time since starting to move towards the current target waypoint
        float elapsedPercentage = _elapsedTime / _timeToWaypoint; // Calculate the percentage of time elapsed towards reaching the target waypoint
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage); // Smoothly interpolate the percentage to give a smoother movement effect
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage); // Move the object towards the target waypoint position based on the elapsed time
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage); // Rotate the object towards the target waypoint rotation based on the elapsed time

        if (elapsedPercentage >= 1) // If the object has reached the current target waypoint
        {
            TargetNextWaypoint(); // Set the next target waypoint in the path
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex); // Set the previous waypoint to the current target waypoint
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex); // Get the index of the next waypoint in the path
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex); // Set the current target waypoint to the next waypoint in the path

        _elapsedTime = 0; // Reset the elapsed time to 0

        float distancetoWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position); // Calculate the distance between the previous waypoint and the current target waypoint
        _timeToWaypoint = distancetoWaypoint / _speed; // Calculate the time required to reach the current target waypoint based on the distance and the speed
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform); // Set the object that triggered the trigger zone as a child of this object
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null); // Set the object that exited the trigger zone to have no parent
    }
}