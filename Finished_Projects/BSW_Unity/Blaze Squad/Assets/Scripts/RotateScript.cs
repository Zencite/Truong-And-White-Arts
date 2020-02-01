using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float yAng;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        yAng = this.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        yAng += Time.deltaTime * speed;
        this.transform.localRotation = Quaternion.Euler(0.0f, yAng, 0.0f);
    }
}
