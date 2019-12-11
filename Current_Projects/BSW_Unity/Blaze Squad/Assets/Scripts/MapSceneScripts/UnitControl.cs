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

    public GameObject controlsPrompt;
    public GameObject backButton;

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
        if(Input.GetKey(KeyCode.Alpha1))
        {
            SelectUnit1();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SelectUnit2();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            SelectUnit3();
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            SelectUnit4();
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            SelectUnit5();
        }
    }

    public void SelectUnit1()
    {
        unit1.GetComponent<PlayerMovement>().activeSelected = true;
        unit1.transform.GetChild(5).gameObject.SetActive(true);

        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.transform.GetChild(5).gameObject.SetActive(false);

        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.transform.GetChild(5).gameObject.SetActive(false);

        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.transform.GetChild(5).gameObject.SetActive(false);

        unit5.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void SelectUnit2()
    {
        unit2.GetComponent<PlayerMovement>().activeSelected = true;
        unit2.transform.GetChild(5).gameObject.SetActive(true);

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit1.transform.GetChild(5).gameObject.SetActive(false);

        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.transform.GetChild(5).gameObject.SetActive(false);

        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.transform.GetChild(5).gameObject.SetActive(false);

        unit5.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void SelectUnit3()
    {
        unit3.GetComponent<PlayerMovement>().activeSelected = true;
        unit3.transform.GetChild(5).gameObject.SetActive(true);

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit1.transform.GetChild(5).gameObject.SetActive(false);

        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.transform.GetChild(5).gameObject.SetActive(false);

        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.transform.GetChild(5).gameObject.SetActive(false);

        unit5.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void SelectUnit4()
    {
        unit4.GetComponent<PlayerMovement>().activeSelected = true;
        unit4.transform.GetChild(5).gameObject.SetActive(true);

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit1.transform.GetChild(5).gameObject.SetActive(false);

        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.transform.GetChild(5).gameObject.SetActive(false);

        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.transform.GetChild(5).gameObject.SetActive(false);

        unit5.GetComponent<PlayerMovement>().activeSelected = false;
        unit5.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void SelectUnit5()
    {
        unit5.GetComponent<PlayerMovement>().activeSelected = true;
        unit5.transform.GetChild(5).gameObject.SetActive(true);

        unit1.GetComponent<PlayerMovement>().activeSelected = false;
        unit1.transform.GetChild(5).gameObject.SetActive(false);

        unit2.GetComponent<PlayerMovement>().activeSelected = false;
        unit2.transform.GetChild(5).gameObject.SetActive(false);

        unit3.GetComponent<PlayerMovement>().activeSelected = false;
        unit3.transform.GetChild(5).gameObject.SetActive(false);

        unit4.GetComponent<PlayerMovement>().activeSelected = false;
        unit4.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void ControlPrompt()
    {
        controlsPrompt.SetActive(true);
    }

    public void Back()
    {
        controlsPrompt.SetActive(false);
    }
}
