using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DifferenceVector : MonoBehaviour
{
    public GameObject B;
    Vector3 Difference;
    public float Speed =100;
    // Start is called before the first frame update
    void Start()
    {
       
       

    }

    // Update is called once per frame
    void Update()
    {
        Difference = B.transform.position - transform.position;
        Debug.Log(Difference);
        Difference.Normalize();
        Difference = Difference * Speed * Time.deltaTime;
        transform.position += Difference;
    }
}
