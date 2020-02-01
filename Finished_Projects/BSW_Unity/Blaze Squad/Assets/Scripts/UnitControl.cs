using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;
    public GameObject unit4;
    public GameObject unit5;

    //public bool unit1Selected;
    //public bool unit2Selected;
    //public bool unit3Selected;
    //public bool unit4Selected;
    //public bool unit5Selected;

    // Start is called before the first frame update
    void Start()
    {
        unit1 = GameObject.Find("Unit 1");
        unit2 = GameObject.Find("Unit 2");
        unit3 = GameObject.Find("Unit 3");
        unit4 = GameObject.Find("Unit 4");
        unit5 = GameObject.Find("Unit 5");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectUnit1()
    {
        unit1.GetComponent<PlayerMovement>().activeSelected = true;

        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.GetComponent<PlayerMovement>().activeSelected = false;
    }

    public void SelectUnit2()
    {
        unit2.GetComponent<PlayerMovement>().activeSelected = true;

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.GetComponent<PlayerMovement>().activeSelected = false;
    }

    public void SelectUnit3()
    {
        unit3.GetComponent<PlayerMovement>().activeSelected = true;

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.GetComponent<PlayerMovement>().activeSelected = false;
    }

    public void SelectUnit4()
    {
        unit4.GetComponent<PlayerMovement>().activeSelected = true;

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.GetComponent<PlayerMovement>().activeSelected = false;
    }

    public void SelectUnit5()
    {
        unit5.GetComponent<PlayerMovement>().activeSelected = true;

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.GetComponent<PlayerMovement>().activeSelected = false;
    }
}
