using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public List<VehicleAI> AIVehicleList;
    public List<Waypoint> CarSpawnPoint;
    public int numberOfCars = 50;
    public float minSpeed = 10;
    public float maxSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCars(numberOfCars);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnCars(int NumberofCars)
    {
        int Length = AIVehicleList.Count;
        int SpawnCount = CarSpawnPoint.Count;
        for (int i = 0; i < NumberofCars; i++)
        {

            Waypoint spawnpoint = CarSpawnPoint[Random.Range(0, SpawnCount)];
            Vector3 SpawnPointPos = spawnpoint.transform.position;
            VehicleAI car = Instantiate(AIVehicleList[Random.Range(0, Length)],SpawnPointPos,Quaternion.identity);
            //car.speed = Random.Range(minSpeed, maxSpeed);
            car.nextWaypoint = spawnpoint;
            car.trafficManager = this;
        }
       
    }
}
