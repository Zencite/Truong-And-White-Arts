using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSight : MonoBehaviour
{

    public GameObject NPC;
    private GameObject OtherNPC;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //[HideInInspector]
    public List<GameObject> visibleTargets = new List<GameObject>();

    private bool inList;

    void Start()
    {
        inList = false;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        //visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            GameObject target = targetsInViewRadius[i].gameObject;
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {

                    if (visibleTargets.Count == 0)                                          //When list is started
                    {
                        visibleTargets.Add(NPC);                                            //Add the itself to the list
                    }
                    else
                    {
                        for (int k = 0; k < visibleTargets.Count; k++)                      //Goes through list of seen
                        {

                            if (visibleTargets[k] == target)                                //If target matches with target in list
                            {

                                inList = true;                                              //Then bool inList goes true

                                if(visibleTargets[k].tag == "NPC")
                                {
                                    GameObject visibleNPC = visibleTargets[k];
                                }
                                else if(visibleTargets[k].tag == "Player")
                                {
                                    GameObject visiblePlayer = visibleTargets[k];
                                }
                            }
                        }
                        if(inList != true)                                                  //If inList is false
                        {
                            visibleTargets.Add(target);                                     //Add the target as a new target to the List
                        }
                        else
                        {
                            inList = false;                                                 //Else reset inList
                        }
                    }
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
