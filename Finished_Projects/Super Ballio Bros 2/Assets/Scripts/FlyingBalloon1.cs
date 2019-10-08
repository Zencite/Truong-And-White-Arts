using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBalloon1 : MonoBehaviour
{
    //public GameObject airBalloon;
    public Rigidbody balloonRigidbody;
    private bool down = false;

    void Awake()
    {
        balloonRigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (down == false)
        {
            StartCoroutine(Upward());
        }
        else
        {
            StartCoroutine(Downward());
        }
    }

    IEnumerator Upward()
    {
        Debug.Log("Going Right Now");
        balloonRigidbody.velocity = (-balloonRigidbody.transform.right * 1.0f);
        yield return new WaitForSeconds(8);
        down = true;
    }
    IEnumerator Downward()
    {
        Debug.Log("Doing Left Now");
        balloonRigidbody.velocity = (balloonRigidbody.transform.right * 1.0f);
        yield return new WaitForSeconds(8);
        down = false;
    }
}
