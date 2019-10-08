using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    private bool teleported = false;
    //public Flowchart flowchart;
    public string teleport;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Lonk2OW")
        {
            Debug.Log(col.gameObject.tag);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!teleported)
            {
                flowchart.ExecuteBlock(teleport);
                teleported = true;
            }
        }
    }*/
}
