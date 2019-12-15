using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float unitRadius;
    public NavMeshAgent navMeshAgent;
    public bool activeSelected;
    public bool queuedOrders;
    public GameObject targetObject;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(!PlantManager.gameDone)
        { 
        if (queuedOrders)
        {
            if (targetObject != null)
            {
                if ((Vector3.Distance(transform.position, targetObject.transform.position) < unitRadius * 3))
                {
                    if (targetObject.GetComponent<FireSpread>() != null)
                    {
                        if (!(targetObject.GetComponent<FireSpread>().isBurning()) && !(targetObject.GetComponent<FireSpread>().isBurnt()) && !(targetObject.GetComponent<FireSpread>().isChopped()))
                        {
                            targetObject.transform.GetChild(1).gameObject.SetActive(false);
                            targetObject.transform.GetChild(3).gameObject.SetActive(true);
                            targetObject.GetComponent<FireSpread>().chopped = true;
                            queuedOrders = false;
                        }
                    }
                }
            }
        }

            // MOUSE CLICK WHEN UNIT IS SELECTED
            if (activeSelected)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Input.GetKey("left shift"))
                    {
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (!EventSystem.current.IsPointerOverGameObject())
                            {
                                if (hit.transform.gameObject.GetComponent<FireSpread>() != null)
                                {
                                    targetObject = hit.transform.gameObject;

                                    navMeshAgent.SetDestination(targetObject.transform.position);

                                    queuedOrders = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(ray, out hit))
                        {
                            MoveToPosition(hit, ray);
                        }
                    }
                }
            }
        }
    }
    //  MOVE TO UNIT TO POSITION OF MOUSE CLICK
    public void MoveToPosition(RaycastHit hit, Ray ray)
    {
        if (Physics.Raycast(ray, out hit))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
