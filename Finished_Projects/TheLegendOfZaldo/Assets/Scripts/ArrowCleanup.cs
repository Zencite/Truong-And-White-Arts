using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowCleanup : MonoBehaviour
{
    public float ArrowDespawnTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ArrowDespawnTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Ouch!");
        }
        if (other.tag == "Turret")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
