using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float unitRadius;
    public NavMeshAgent navMeshAgent;
    public bool activeSelected;
    public bool queuedOrders;
    public GameObject targetObject;
    public Text actionText;
    private int randomNum;
    private bool once;

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

                                if (targetObject.GetComponent<CapsuleCollider>() != null)
                                {
                                    targetObject.GetComponent<CapsuleCollider>().enabled = false;
                                }
                                else if (targetObject.GetComponent<SphereCollider>() != null)
                                {
                                    targetObject.GetComponent<SphereCollider>().enabled = false;
                                }

                                targetObject.GetComponent<FireSpread>().chopped = true;

                                randomNum = Random.Range(0, 4);
                                actionText.color = new Color(1.0f, 0.0f, 0.0f);
                                switch (randomNum)
                                {
                                    case 0:
                                        actionText.text = "Finished";
                                        break;
                                    case 1:
                                        actionText.text = "Cleared";
                                        break;
                                    case 2:
                                        actionText.text = "Got it";
                                        break;
                                    case 3:
                                        actionText.text = "Chopped";
                                        break;
                                    default:
                                        break;
                                }
                                
                                queuedOrders = false;
                                once = false; 
                            }
                        }
                    }
                }
            }
            else
            {
                if (!once)
                {
                    randomNum = Random.Range(0, 4);
                    actionText.color = new Color(0.0f, 1.0f, 0.0f);
                    switch (randomNum)
                    {
                        case 0:
                            actionText.text = "on Standby";
                            break;
                        case 1:
                            actionText.text = "Ready";
                            break;
                        case 2:
                            actionText.text = "Idling";
                            break;
                        case 3:
                            actionText.text = "Waiting";
                            break;
                        default:
                            break;
                    }
                    once = true;
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

                                    randomNum = Random.Range(0, 4);
                                    actionText.color = new Color(1.0f, 0.5f, 0.0f);
                                    switch (randomNum)
                                    {
                                        case 0:
                                            actionText.text = "Roger";
                                            break;
                                        case 1:
                                            actionText.text = "Queued up";
                                            break;
                                        case 2:
                                            actionText.text = "Queued!";
                                            break;
                                        case 3:
                                            actionText.text = "Targeted";
                                            break;
                                        default:
                                            break;
                                    }
                                    queuedOrders = true;
                                    once = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(ray, out hit))
                        {
                            MoveToPosition(hit, ray);

                            randomNum = Random.Range(0, 4);
                            actionText.color = new Color(1.0f, 1.0f, 0.0f);
                            switch (randomNum)
                            {
                                case 0:
                                    actionText.text = "Moving";
                                    break;
                                case 1:
                                    actionText.text = "On Route";
                                    break;
                                case 2:
                                    actionText.text = "Going out";
                                    break;
                                case 3:
                                    actionText.text = "En Route";
                                    break;
                                default:
                                    break;
                            }
                            once = false;
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
