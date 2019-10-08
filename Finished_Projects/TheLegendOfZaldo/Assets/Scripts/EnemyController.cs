using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform shotPos;
    public Transform turret;
    public GameObject turretObject;
    public GameObject playerObject;
    private bool firing = false;

    public float shotForce = 100f;
    public float moveSpeed = 5f;

    public GameObject target;
    public float minDistance = 5;
    public float speed = 0.5f;

    private Vector3 targetPos;

    void Start()
    {
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < minDistance)
        {
            transform.LookAt(target.transform.position);
            targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            firing = true;
        }
        else
        {
            firing = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if ((other.gameObject.tag == "Friendly Projectile") || (other.gameObject.tag == "Sword"))
        {
            Debug.Log("Collision");
            playerObject.GetComponent<PlayerScript>().setPlayerCount();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Friendly Projectile") || (other.gameObject.tag == "Sword"))
        {
            Debug.Log("Collider");
            playerObject.GetComponent<PlayerScript>().setPlayerCount();
            Destroy(gameObject);
        }
    }

    public bool getFiring()
    {
        return firing;
    }
    
}