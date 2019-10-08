using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScript : MonoBehaviour
{
    public GameObject trapDoor;
    public GameObject ColliderTrigger;
    public GameObject lever1;
    public GameObject lever2;
    public GameObject bridge;

    // Start is called before the first frame update
    void Start()
    {
        trapDoor.gameObject.SetActive(false);
        bridge.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((lever1.GetComponent<LeverScript>().getActivated()) && (lever2.GetComponent<LeverScript>().getActivated()))
        {
            //Debug.Log(lever1.GetComponent<LeverScript>().getActivated());
            //Debug.Log(lever2.GetComponent<LeverScript>().getActivated());
            bridge.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (trapDoor)
            {
                trapDoor.gameObject.SetActive(true);
            }
        }
    }
}
