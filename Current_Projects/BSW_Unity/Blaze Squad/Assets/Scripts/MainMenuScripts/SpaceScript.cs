using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceScript : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.localScale.x > 0.1f)
        {
            transform.localScale -= new Vector3(x, y, z);
        }
    }
}
