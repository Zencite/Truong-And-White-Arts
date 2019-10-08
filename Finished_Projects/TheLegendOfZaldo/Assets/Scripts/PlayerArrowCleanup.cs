using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerArrowCleanup : MonoBehaviour
{
    public float ArrowDespawnTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ArrowDespawnTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        if (other.tag == "Bow")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}