using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollider : MonoBehaviour
{
    public GameObject collider1; //title theme
    public GameObject collider2; //village theme
    public GameObject collider3; //Main theme
    public GameObject collider4; //Cave theme



    // Start is called before the first frame update
    void Start()
    {
        collider1.gameObject.SetActive(true);
        collider2.gameObject.SetActive(false);
        collider3.gameObject.SetActive(false);
        collider4.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Indoors")
        {
            collider1.gameObject.SetActive(false);
            collider2.gameObject.SetActive(true);
            collider3.gameObject.SetActive(false);
            collider4.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Cave")
        {
            collider1.gameObject.SetActive(false);
            collider2.gameObject.SetActive(false);
            collider3.gameObject.SetActive(false);
            collider4.gameObject.SetActive(true);
        }
        else
        {
            //Source1.gameObject.SetActive(true); //Main Theme
            //Source2.gameObject.SetActive(false); //Dungeons
            //Source3.gameObject.SetActive(false); //Homes
        }
    }
}
