using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffMap : MonoBehaviour
{
    public GameObject outsideBase;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            outsideBase.gameObject.SetActive(false);
        }
    }
}
