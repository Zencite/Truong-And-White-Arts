using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitUpScriptMainMenu : MonoBehaviour
{
    // ACTIVATES THEIR SUITS
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.transform.childCount > 3)
        {
            other.transform.parent.transform.GetChild(3).gameObject.SetActive(true);
            other.transform.parent.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
