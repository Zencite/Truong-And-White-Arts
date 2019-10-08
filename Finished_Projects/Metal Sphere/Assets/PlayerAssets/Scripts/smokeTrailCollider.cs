using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeTrailCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Environment")
            Destroy(gameObject);
    }

    void Start()
    {
        Destroy(gameObject, 16);
    }
}
