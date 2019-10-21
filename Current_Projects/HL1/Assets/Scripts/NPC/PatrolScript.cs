using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public NavMeshAgent navMeshAgent;
    public Transform agent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();
        }
        else
        {
            if ((Vector3.Distance(agent.position, point1.transform.position) < 1) && (Vector3.Distance(agent.position, point2.transform.position) > 1))
            {
                navMeshAgent.SetDestination(point2.position);
            }
            else if ((Vector3.Distance(agent.position, point2.transform.position) < 1) && (Vector3.Distance(agent.position, point1.transform.position) > 1))
            {
                navMeshAgent.SetDestination(point1.position);
            }
        }
    }

    private IEnumerator SetDestination(Transform endpoint)
    {
        navMeshAgent.SetDestination(endpoint.position);
        yield return null;
    }

}
