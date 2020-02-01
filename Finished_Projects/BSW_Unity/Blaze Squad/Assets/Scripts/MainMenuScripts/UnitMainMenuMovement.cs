using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMainMenuMovement : MonoBehaviour
{
    public GameObject u1;
    public GameObject u2;
    public GameObject u3;
    public GameObject u4;
    public GameObject u5;

    public GameObject u3S;
    public GameObject u4T;

    public Transform truckPoint;

    // Start is called before the first frame update
    void Start()
    {
        u3S.SetActive(true);
        u4T.SetActive(true);

        u3.SetActive(false);
        u4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenuButtons.missionGo)
        {
            u3S.SetActive(false);
            u4T.SetActive(false);

            u3.SetActive(true);
            u4.SetActive(true);

            if (TruckScriptMainMenu.u1Active)
            {
                u1.GetComponent<NavMeshAgent>().SetDestination(truckPoint.position);
            }
            else
            {
                u1.SetActive(false);
            }

            if (TruckScriptMainMenu.u2Active)
            {
                u2.GetComponent<NavMeshAgent>().SetDestination(truckPoint.position);
            }
            else
            {
                u2.SetActive(false);
            }

            if (TruckScriptMainMenu.u3Active)
            {
                u3.GetComponent<NavMeshAgent>().SetDestination(truckPoint.position);
            }
            else
            {
                u3.SetActive(false);
            }

            if (TruckScriptMainMenu.u4Active)
            {
                u4.GetComponent<NavMeshAgent>().SetDestination(truckPoint.position);
            }
            else
            {
                u4.SetActive(false);
            }

            if (TruckScriptMainMenu.u5Active)
            {
                u5.GetComponent<NavMeshAgent>().SetDestination(truckPoint.position);
            }
            else
            {
                u5.SetActive(false);
            }
        }
    }
}
