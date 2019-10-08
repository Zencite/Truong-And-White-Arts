using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardmans : MonoBehaviour
{
    //facing neg x direction 
    public float TargetDistance;
    public GameObject target;
    bool once = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit TheHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out TheHit))
        {
            TargetDistance = TheHit.distance;
            if (TargetDistance > 0 && TargetDistance < 12)
            {
                Destroy(target.gameObject);
                if (once == false)
                {
                    GetComponent<AudioSource>().Play();
                    once = true;
                }
            }
        }
    }
}
