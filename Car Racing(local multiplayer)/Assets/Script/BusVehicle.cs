using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusVehicle : MonoBehaviour
{
    private float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Move the Bus Vehicle
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
