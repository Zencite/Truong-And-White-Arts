using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
        {
            Destroy(other.gameObject);
        }
    }
}