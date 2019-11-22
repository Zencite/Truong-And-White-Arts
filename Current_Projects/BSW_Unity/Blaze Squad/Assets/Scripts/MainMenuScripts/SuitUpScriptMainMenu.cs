using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitUpScriptMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.transform.childCount > 3)
        {
            other.transform.parent.transform.GetChild(3).gameObject.SetActive(true);
            other.transform.parent.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
