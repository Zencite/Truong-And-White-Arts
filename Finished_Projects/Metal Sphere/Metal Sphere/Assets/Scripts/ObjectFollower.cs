using UnityEngine;
using System.Collections;

public class ObjectFollower : MonoBehaviour
{

    public GameObject player;
    public Transform target;
    public float distance = 3f;
    public float distanceOffset;

    private Vector3 offset;
    private Vector3 relativePos;
   

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        relativePos = transform.position - (target.position);
        RaycastHit hit;
        if (Physics.Raycast(target.position, relativePos, out hit, distance + 0.5f))
        {
            Debug.DrawLine(target.position, hit.point);
            distanceOffset = distance - hit.distance + 0.8f;
            distanceOffset = Mathf.Clamp(distanceOffset, 0, distance);
        }
        else
        {
            distanceOffset = 0;
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}