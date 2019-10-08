using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOnCollision : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private bool locked = false;

    void OnTriggerEnter(Collider other)
    {
        //once it collides lock in place
        if (other.tag == "Environment" && !locked && (playerRigidbody != null))
        {
            playerRigidbody.freezeRotation = true;
            playerRigidbody.useGravity = false;
            playerRigidbody.velocity = Vector3.zero;
            locked = true;
        }
    }

    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }
}