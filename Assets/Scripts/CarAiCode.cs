using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarAiCode : MonoBehaviour
{
    public Waypoint NextWaypoint;
    public float speed = 3;
    Vector3 currentDirection;
    public float totalRotationTime = 1;
    float rotationTimer = 0;
    bool ifRotating;
    public Transform nextWaypointTransform;
    Vector3 targetForward;
    public float distanceToTriggerRotation = 2;
    public void OnReachedNextWayPoint()
    {
        NextWaypoint = NextWaypoint.ConnectedWaypoints[0];
        currentDirection = NextWaypoint.transform.position - transform.position;
        currentDirection.Normalize();
        transform.LookAt(NextWaypoint.transform);
        Debug.LogWarning("x = "+currentDirection.x+" y = "+ currentDirection.y +" z = "+currentDirection.z);
    }
   
    void Start()
    {
        currentDirection = NextWaypoint.transform.position - transform.position;
        currentDirection.Normalize();
        transform.LookAt(NextWaypoint.transform);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(currentDirection * Time.deltaTime * speed, Space.World);
        // Debug.Log(NextWaypoint.transform.position);
        return;
        float distanceToTarget = Vector3.Distance(NextWaypoint.transform.position, this.transform.position);
        Debug.Log(distanceToTarget);

        if (distanceToTarget < distanceToTriggerRotation)
        {
            ifRotating = true;
            targetForward = NextWaypoint.ConnectedWaypoints[0].transform.position - NextWaypoint.transform.position;
            
            targetForward.Normalize();
        }
        if (ifRotating == true && rotationTimer < totalRotationTime)
        {
            Debug.LogWarning(rotationTimer);
            rotationTimer += Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, targetForward, rotationTimer / totalRotationTime);
           
            if (rotationTimer > totalRotationTime)
            {
                rotationTimer = 0;
                ifRotating = false;
            }

        }
    }

}
