using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public List<CarAiCode> AiCarList;
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
        int Length = AiCarList.Count;
        int SpawnCount = CarSpawnPoint.Count;
        for (int i = 0; i < NumberofCars; i++)
        {

            Waypoint spawnpoint = CarSpawnPoint[Random.Range(0, SpawnCount)];
            Vector3 SpawnPointPos = spawnpoint.transform.position;
            CarAiCode car = Instantiate(AiCarList[Random.Range(0, Length)],SpawnPointPos,Quaternion.identity);
            car.speed = Random.Range(minSpeed, maxSpeed);
            car.NextWaypoint = spawnpoint;
            car.trafficManager = this;
        }
       
    }
}
