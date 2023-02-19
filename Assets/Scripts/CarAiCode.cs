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

    public Transform rayCastPoint;
    private float acceleration = 1f;

    public TrafficManager trafficManager;

    public void OnReachedNextWayPoint()
    {
        NextWaypoint = NextWaypoint.ConnectedWaypoints[0];
        currentDirection = NextWaypoint.transform.position - transform.position;
        currentDirection.Normalize();

        if (speed >= 20f && acceleration > 1f)
            acceleration = 0.6f;
        else if (speed >= 8f && acceleration > 1f)
            acceleration = 0.9f;
        else
            acceleration = 1f;

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
        // transform.Translate(currentDirection * Time.deltaTime * speed, Space.World);

        RaycastHit hitInfo;

        Ray ray = new Ray(transform.position + new Vector3(0, 0.5f , 0), currentDirection);
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0.5f, 0) + 20 * currentDirection);
        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(ray, out hitInfo, 20, ~layerMask))
        {
            if (hitInfo.collider.gameObject != gameObject)
            {
                if (speed >= 20f)
                    acceleration = -1f;
                else if (speed >= 10f)
                    acceleration = 0.2f;
                else
                    acceleration = 0.3f;

                speed = Math.Clamp(speed, 0.01f, trafficManager.maxSpeed);
            }
        }
        else
        {
            acceleration = 1.5f;
            speed = Math.Clamp(speed, trafficManager.minSpeed, trafficManager.maxSpeed);
        }

        speed = speed + speed * (acceleration - 1) * Time.deltaTime;

        // speed = Math.Clamp(speed, trafficManager.minSpeed, trafficManager.maxSpeed);
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
