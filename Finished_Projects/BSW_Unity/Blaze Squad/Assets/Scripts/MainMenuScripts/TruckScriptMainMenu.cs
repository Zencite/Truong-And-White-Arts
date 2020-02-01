using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScriptMainMenu : MonoBehaviour
{
    public static bool u1Active;
    public static bool u2Active;
    public static bool u3Active;
    public static bool u4Active;
    public static bool u5Active;

    public BoxCollider boxCollider;

    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;

    // Start is called before the first frame update
    void Start()
    {
        u1Active = true;
        u2Active = true;
        u3Active = true;
        u4Active = true;
        u5Active = true;

        light1.enabled = false;
        light2.enabled = false;
        light3.enabled = false;
        light4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ONCE ALL UNITS ARE IN MOVE FIRETRUCK
        if(!u1Active && !u2Active && !u3Active && !u4Active && !u5Active)
        {
            boxCollider.enabled = false;
            light1.enabled = true;
            light2.enabled = true;
            light3.enabled = true;
            light4.enabled = true;
            transform.Translate(Vector3.forward * Time.deltaTime * 10.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // CHECKS EACH UNIT'S LAYER AND "PUTS THEM" IN THE FIRETRUCK
        switch(other.gameObject.layer)
        {
            case 8:
                this.transform.GetChild(0).gameObject.SetActive(true);
                u1Active = false;
                break;
            case 9:
                this.transform.GetChild(1).gameObject.SetActive(true);
                u2Active = false;
                break;
            case 10:
                this.transform.GetChild(2).gameObject.SetActive(true);
                u3Active = false;
                break;
            case 11:
                this.transform.GetChild(3).gameObject.SetActive(true);
                u4Active = false;
                break;
            case 12:
                this.transform.GetChild(4).gameObject.SetActive(true);
                u5Active = false;
                break;
        }
    }
}
