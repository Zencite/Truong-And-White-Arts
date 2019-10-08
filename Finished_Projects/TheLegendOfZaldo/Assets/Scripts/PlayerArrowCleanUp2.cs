using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowCleanUp2 : MonoBehaviour
{
    public float ArrowDespawnTime;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ArrowDespawnTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.tag);
            Debug.Log("Expected to be Player");
            return;
        }
        else if (other.tag == "Bow")
        {
            Debug.Log(other.tag);
            Debug.Log("Expected to be Bow");
            return;
        }
        else if (other.tag == "Friendly Projectile")
        {
            Debug.Log(other.tag);
            Debug.Log("Expected to be Friendly Projectile");
            return;
        }
        else
        {
            Debug.Log(other.tag);
            Debug.Log("^ What is this? ^");
            Destroy(gameObject);
        }
    }
}
