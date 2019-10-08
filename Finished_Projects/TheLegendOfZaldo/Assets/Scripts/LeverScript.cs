using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    private bool activated = false;
    public GameObject leverActive;
    public GameObject leverDeactive;

    void Start()
    {
        leverActive.gameObject.SetActive(true);
        leverDeactive.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Friendly Projectile")
        {
            if (!(activated))
            {
                activated = true;
                leverActive.gameObject.SetActive(false);
                leverDeactive.gameObject.SetActive(true);
            }
        }
    }

    public bool getActivated()
    {
        return activated;
    }
}
