using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject Cam1;
    //public GameObject Cam2;
    //public GameObject Cam3;
    //public GameObject Cam4;

    public GameObject camera;
    public int movementSpeed;
    private bool qPressed = false;
    private bool qPressed2 = false;

    // Start is called before the first frame update
    void Start()
    {
        //Cam1.gameObject.SetActive(true);
        //Cam2.gameObject.SetActive(false);
        //Cam3.gameObject.SetActive(false);
        //Cam4.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPlayerDifference = transform.position - camera.transform.position;
        cameraPlayerDifference.y = 0;
        cameraPlayerDifference.Normalize();

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 movement = (cameraPlayerDifference * moveVertical + camera.transform.right * moveHorizontal) * movementSpeed;
        transform.rotation = Quaternion.LookRotation(movement);

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        //ControlMovement();

        /*if (Input.GetKeyDown("q"))
        {
            if(qPressed == false && qPressed2 == false)
            {
                Cam1.gameObject.SetActive(false);
                Cam2.gameObject.SetActive(true);
                qPressed = true;
            }
            else if (qPressed == true && qPressed2 == false)
            {
                Cam2.gameObject.SetActive(false);
                Cam3.gameObject.SetActive(true);
                qPressed2 = true;
            }
            else if (qPressed == true && qPressed2 == true)
            {
                Cam3.gameObject.SetActive(false);
                Cam4.gameObject.SetActive(true);
                qPressed = false;
            }
            else if(qPressed == false && qPressed2 == true)
            {
                Cam4.gameObject.SetActive(false);
                Cam1.gameObject.SetActive(true);
                qPressed2 = false;
            }
        }*/
    }

}
