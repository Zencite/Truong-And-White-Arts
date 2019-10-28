using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingObject : MonoBehaviour
{
    public Transform[] Locations;
    public float Speed = 2f;

    Transform myTransform;


    void Start()
    {
        myTransform = transform;
    }

    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * Speed, 1);
        myTransform.position = Vector3.Lerp(Locations[0].position, Locations[1].position, pingPong);
    }
}