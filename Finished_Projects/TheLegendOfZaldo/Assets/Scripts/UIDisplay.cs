using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    public GameObject health;
    public GameObject lupee;

    // Start is called before the first frame update
    void Start()
    {
        health.gameObject.SetActive(false);
        lupee.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            health.gameObject.SetActive(true);
        }
    }
}
