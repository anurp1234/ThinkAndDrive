using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public List<CarAiCode> AiCarList;
    public List<Waypoint> CarSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCars(4);
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

            Waypoint CurrentSpawnPoint = CarSpawnPoint[Random.Range(0, SpawnCount)];
            Vector3 SpawnPointPos = CurrentSpawnPoint.transform.position;
            CarAiCode car = Instantiate(AiCarList[Random.Range(0, Length)],SpawnPointPos,Quaternion.identity);
            car.NextWaypoint = CurrentSpawnPoint;

        }
       
    }
}
