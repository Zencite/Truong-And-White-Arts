using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public GameObject bridge;
    public GameObject lever1;
    // Start is called before the first frame update
    void Start()
    {
        bridge.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((lever1.GetComponent<LeverScript>().getActivated()))
        {

            bridge.gameObject.SetActive(true);
        }
    }
}
