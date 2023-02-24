using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleRunningState : IState
{
    public TrafficManager trafficManager;

    public float maxSpeed; // The maximum speed of the vehicle
    public float acceleration = 10f; // The acceleration of the vehicle
    public float deceleration = 18f; // The deceleration of the vehicle
    public float brakingDistance = 15f; // The distance to start decelerating the vehicle
    public float turnSpeed = 4f; // The turning speed of the vehicle
    public float turnRadius = 3f; // The turning radius of the vehicle
    public LayerMask obstacleLayer; // The layer mask for detecting obstacles
    public Waypoint nextWaypoint; // The target waypoint for the vehicle
    public float minBrakingRate = 1f;
    public float maxBrakingRate = 5f;

    private Rigidbody rb; // The rigidbody component of the vehicle
    [SerializeField] private float currentSpeed; // The current speed of the vehicle
    private bool turning; // Whether the vehicle is currently turning
    private Vector3 turnPoint; // The point where the vehicle is turning

    private GameObject currentObject;
    private Transform transform;

    void IState.DestroyState()
    {
        return;
    }

    void IState.SetupState()
    {
        return;
    }

    void IState.UpdateState()
    {
        if (nextWaypoint == null)
        {
            return;
        }

        Vector3 waypointDirection = nextWaypoint.transform.position - transform.position;

        if (waypointDirection.magnitude < turnRadius)
        {
            turning = true;
            turnPoint = transform.position;
            OnReachedNextWayPoint();
        }

        // Calculate the desired speed based on the AI behavior
        float desiredSpeed = maxSpeed;

        // Check for obstacles in front of the vehicle and slow down if necessary
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * brakingDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, brakingDistance, obstacleLayer))
        {
            if (currentSpeed > 0)
            {
                Brake(deceleration);
            }
        }
        else if (currentSpeed <= maxSpeed)
        {
            if (turning && currentSpeed > 0)
            {
                Brake(deceleration / 2f);
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);
            }
        }

        // Rotate the car towards the current waypoint
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, waypointDirection, turnSpeed * Time.fixedDeltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Move the car forward
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        Vector3 velocity = transform.forward * currentSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        turning = false;
    }

    public void Brake(float decelerationValue)
    {

        currentSpeed -= decelerationValue * Time.fixedDeltaTime;
    }

    public void OnReachedNextWayPoint()
    {
        nextWaypoint = nextWaypoint.ConnectedWaypoints[0];
        //Vector3 waypointDirection = nextWaypoint.transform.position - transform.position;
    }

    public VehicleRunningState(GameObject currentObject, Waypoint spawnPoint, float maxSpeed)
    {
        this.currentObject = currentObject;
        transform = currentObject.transform;
        nextWaypoint = spawnPoint;
        rb = currentObject.GetComponent<Rigidbody>();
        obstacleLayer = LayerMask.GetMask("Vehicle");
        this.maxSpeed = maxSpeed;
    }

}
