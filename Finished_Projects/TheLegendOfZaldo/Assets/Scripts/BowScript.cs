using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    //For the bow
    public GameObject bowObject;
    public Transform bowTransform;
    public Transform ShootFrom;
    public float distanceFromBow;
    public GameObject playerObject;

    //For making sure only one tool is used
    private bool held = false;
    private bool thisAction = false;

    //For Arrows
    public Rigidbody projectile;
    public Transform shotPos;
    public float shotForce = 1000f;
    public float moveSpeed = 10f;

    //For remembering what direction the player is facing
    private bool upTrue, downTrue, leftTrue, rightTrue = false;
    double rotX, rotY, rotZ = 0;
    Vector3 pRotation = new Vector3(0, 0, 0);
    Vector3 pPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        bowObject.SetActive(false);
        pRotation = new Vector3(0, -75, 0);
        pPosition = new Vector3(0, 0.05f, distanceFromBow);

    }

    // Update is called once per frame
    void Update()
    {


    }
    void FixedUpdate()
    {

        //The bow needs to not move once it's been placed, so the if statements are set up so that
        //The shield will only be placed IF:
        //  A direction is picked
        //  The direction has yet to be chosen
        //Or
        //  The direction was chosen before

        if ((!held && (Input.GetKey("w"))) || upTrue)
        {
            upTrue = true;

            pRotation = new Vector3(0, 0, 0);
            pPosition = new Vector3(0.5f, 0.05f, distanceFromBow);
            held = true;
        }

        if (!held && (Input.GetKey("d")) || rightTrue)
        {
            rightTrue = true;
            pRotation = new Vector3(0, 90, 0);
            pPosition = new Vector3(distanceFromBow, 0.05f, -0.5f);
            held = true;

        }
        if (!held && (Input.GetKey("a")) || leftTrue)
        {
            leftTrue = true;
            pRotation = new Vector3(0, -90, 0);
            pPosition = new Vector3(-distanceFromBow, 0.05f, 0.5f);
            held = true;

        }

        if (!held && (Input.GetKey("s")) || downTrue)
        {
            downTrue = true;

            pRotation = new Vector3(0, -180, 0);
            pPosition = new Vector3(-0.5f, 0.05f, -distanceFromBow);
            held = true;


        }

        if (((Input.GetKey("right shift")) && !(Input.GetKey("left shift"))) || thisAction)
        {
            if ((playerObject.GetComponent<PlayerScript>().isBowActive()))
            {
                bowObject.SetActive(true);
                held = true;
                thisAction = true;
                bowTransform.transform.rotation = Quaternion.Euler(pRotation);
                bowTransform.transform.position = transform.position;
                bowTransform.transform.position += pPosition;
            }
        }

        if (((Input.GetKey("right shift")) && thisAction && ((Input.GetKeyDown("space")))))
        {
            Quaternion bowRotation = bowTransform.transform.rotation;

            Rigidbody shot = Instantiate(projectile, shotPos.position, bowRotation) as Rigidbody;

            shot.transform.LookAt(ShootFrom);



            shot.AddForce(shotPos.forward * shotForce);
        }


        if (!(Input.GetKey("right shift")))
        {
            held = false;
            thisAction = false;
            upTrue = false;
            downTrue = false;
            leftTrue = false;
            rightTrue = false;
            bowObject.SetActive(false);
        }
    }
}
