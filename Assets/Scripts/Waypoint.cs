using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> ConnectedWaypoints;
    public float safeDistance = 10f;
    bool isWrongSide;
    bool isVehicleNear;
    Vector3 nextWaypointDirection;
    RaycastHit hitInfo;

    private void Start()
    {
        nextWaypointDirection = ConnectedWaypoints[0].transform.position - transform.position;
        nextWaypointDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, nextWaypointDirection * safeDistance, Color.green);
        if (Physics.Raycast(transform.position, nextWaypointDirection, out hitInfo, safeDistance))
        {
            if (hitInfo.collider.tag == "AiCar")
            {
                isVehicleNear = true;
            }
            else
            {
                isVehicleNear = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AiCar")
        {
            //CarAiCode car = other.gameObject.GetComponent<CarAiCode>();
            //car.OnReachedNextWayPoint();
            VehicleAI car = other.gameObject.GetComponent<VehicleAI>();

            if (isVehicleNear)
            {
                car.Brake(car.deceleration * 1.5f);
            }
        }
    }
}
