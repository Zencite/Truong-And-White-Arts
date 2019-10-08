using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public GameObject shieldObject;
    public Transform shieldTransform;
    public float distanceFromShield;
    public GameObject playerObject;

    private bool held = false;
    private bool thisAction = false;

    private bool upTrue, downTrue, leftTrue, rightTrue = false;
    double rotX, rotY, rotZ = 0;
    Vector3 pRotation = new Vector3(0, 0, 0);
    Vector3 pPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        shieldObject.SetActive(false);
        pRotation = new Vector3(0, -75, 0);
        pPosition = new Vector3(0.5f, 0.7f, distanceFromShield);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {


        //The shield needs to not move once it's been placed, so the if statements are set up so that
        //The shield will only be placed IF:
        //  A direction is picked
        //  The direction has yet to be chosen
        //Or
        //  The direction was chosen before

        if ((!held && (Input.GetKey("w"))) || upTrue)
        {
            upTrue = true;

            pRotation = new Vector3(0, -75, 0);
            pPosition = new Vector3(0.5f, 0.7f, distanceFromShield);
            held = true;


        }

        if (!held && (Input.GetKey("d")) || rightTrue)
        {
            rightTrue = true;
            pRotation = new Vector3(0, 13, 0);
            pPosition = new Vector3(distanceFromShield, 0.7f, -0.5f);
            held = true;

        }
        if (!held && (Input.GetKey("a")) || leftTrue)
        {
            leftTrue = true;
            pRotation = new Vector3(0, -165, 0);
            pPosition = new Vector3(-distanceFromShield, 0.7f, 0.5f);
            held = true;

        }

        if (!held && (Input.GetKey("s")) || downTrue)
        {
            downTrue = true;

            pRotation = new Vector3(0, 105, 0);
            pPosition = new Vector3(-0.5f, 0.7f, -distanceFromShield);
            held = true;


        }

        if (((Input.GetKey("left shift")) && !(Input.GetKey("right shift"))) || thisAction)
        {
            if ((playerObject.GetComponent<PlayerScript>().isShieldActive()))
            {
                shieldObject.SetActive(true);
                held = true;
                thisAction = true;
                shieldTransform.transform.rotation = Quaternion.Euler(pRotation);
                shieldTransform.transform.position = transform.position;
                shieldTransform.transform.position += pPosition;
            }
        }

        if (!(Input.GetKey("left shift")))
        {
            held = false;
            thisAction = false;
            upTrue = false;
            downTrue = false;
            leftTrue = false;
            rightTrue = false;
            shieldObject.SetActive(false);
        }
    }
}