using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> ConnectedWaypoints;
    bool isWrongSide;

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AiCar")
        {
            CarAiCode car = other.gameObject.GetComponent<CarAiCode>();
            car.OnReachedNextWayPoint();
        }
    }
}
