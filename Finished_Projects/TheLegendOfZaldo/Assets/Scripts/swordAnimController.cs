using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAnimController : MonoBehaviour
{
    public Animator swordAnim;
    public GameObject playerObject;
    public GameObject swordObject;
    // Start is called before the first frame update
    void Start()
    {
        swordAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if ((playerObject.GetComponent<SwordScript>().getSwordInUse()))
        {
            string swordDirection = (playerObject.GetComponent<SwordScript>().getPublicSwordDirection());

            Debug.Log(swordDirection);

            if (swordDirection == "up")
            {
                swordAnim.Play("Sword Animation(Up)");
            }

            if (swordDirection == "down")
            {
                swordAnim.Play("Sword Animation(Down)");
            }

            if (swordDirection == "right")
            {
                swordAnim.Play("Sword Animation(Right)");
            }

            if (swordDirection == "left")
            {
                swordAnim.Play("Sword Animation(Left)");
            }
        }
        else
        {
            swordAnim.Play("Idling");
        }
    }

    
}
