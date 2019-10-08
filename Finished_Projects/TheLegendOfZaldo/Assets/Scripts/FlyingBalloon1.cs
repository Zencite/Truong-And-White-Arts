using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBalloon1 : MonoBehaviour
{
    //public GameObject airBalloon;
    public Rigidbody carriage;
    private bool once = false;
    private bool done = false;
    public GameObject teleporter;
    public float scrollTime;
    //68.21
    void Awake()
    {
        carriage = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        teleporter.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (once == false)
        {
            StartCoroutine(Rightward());
        }
        else
        {
            if (done == false)
            {
                StartCoroutine(Stop());
            }
        }
    }
    IEnumerator Rightward()
    {
        carriage.velocity = (carriage.transform.right * scrollTime);
        yield return new WaitForSeconds(22);
        once = true;
    }
    IEnumerator Stop()
    {
        carriage.velocity = (carriage.transform.right * 0.0f);
        yield return new WaitForSeconds(5);
        teleporter.gameObject.SetActive(true);
        done = true;
    }
}
