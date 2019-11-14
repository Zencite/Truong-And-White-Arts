using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public bool activeSelected;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (activeSelected)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        print("Not on UI");
                        navMeshAgent.SetDestination(hit.point);
                    }
                }
            }
        }
    }
}
