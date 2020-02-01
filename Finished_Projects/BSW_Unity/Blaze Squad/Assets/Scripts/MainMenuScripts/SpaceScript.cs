using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceScript : MonoBehaviour
{
    //TIE FIGHTER MOVES FORWARD AND SCALES DOWN

    public float x;
    public float y;
    public float z;
    public float speed;

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
