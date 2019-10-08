using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject swordObject;
    public Transform swordTransform;
    public float distanceFromSword;
    public GameObject playerObject;

    private bool held = false;
    private bool thisAction = false;
    private bool swordInUse = false;

    private string publicSwordDirection = "";

    private bool upTrue, downTrue, leftTrue, rightTrue = false;
    double rotX, rotY, rotZ = 0;
    Vector3 pRotation = new Vector3(0, 0, 0);
    Vector3 pPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        swordObject.SetActive(false);
        pRotation = new Vector3(0, -75, 0);
        pPosition = new Vector3(0.5f, 0.7f, distanceFromSword);

    }

    // Update is called once per frame
    void Update()
    {


    }
    void FixedUpdate()
    {


        //The sword needs to not move once it's been placed, so the if statements are set up so that
        //The shield will only be placed IF:
        //  A direction is picked
        //  The direction has yet to be chosen
        //Or
        //  The direction was chosen before

        if ((!held && (Input.GetKey("w"))) || upTrue)
        {
            upTrue = true;
            publicSwordDirection = "up";

            pRotation = new Vector3(0, 10, 0);
            pPosition = new Vector3(0.5f, 0.1f, distanceFromSword);
            held = true;


        }

        if (!held && (Input.GetKey("d")) || rightTrue)
        {
            rightTrue = true;
            publicSwordDirection = "right";

            pRotation = new Vector3(0, 93, 0);
            pPosition = new Vector3(distanceFromSword, 0.1f, -0.5f);
            held = true;

        }
        if (!held && (Input.GetKey("a")) || leftTrue)
        {
            leftTrue = true;
            publicSwordDirection = "left";

            pRotation = new Vector3(0, -87, 0);
            pPosition = new Vector3(-distanceFromSword, 0.1f, 0.5f);
            held = true;

        }

        if (!held && (Input.GetKey("s")) || downTrue)
        {
            downTrue = true;
            publicSwordDirection = "down";

            pRotation = new Vector3(0, 190, 0);
            pPosition = new Vector3(-0.5f, 0.1f, -distanceFromSword);
            held = true;


        }

        if (((Input.GetKeyDown("space")) && !(Input.GetKey("right shift")) && !(Input.GetKey("left shift"))))
        {
            if ((playerObject.GetComponent<PlayerScript>().isSwordActive()))
            {
                StartCoroutine("swordAttack");
                held = false;
            }
        }
        
    }
    
    //==================================================
    //Getters for swordInUse and direction
    //==================================================
    public bool getSwordInUse()
    {
        return swordInUse;
    }

    public string getPublicSwordDirection()
    {
        return publicSwordDirection;
    }


    private IEnumerator swordAttack()
    {
        swordObject.SetActive(true);

        held = true;
        swordInUse = true;

        swordTransform.transform.rotation = Quaternion.Euler(pRotation);
        swordTransform.transform.position = transform.position;
        swordTransform.transform.position += pPosition;




        yield return new WaitForSeconds(0.1f);

        held = false;
        thisAction = false;
        swordInUse = false;
        upTrue = false;
        downTrue = false;
        leftTrue = false;
        rightTrue = false;
        swordObject.SetActive(false);
    }
}
