using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretScript : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform shotPos;
    public Transform turret;
    public GameObject turretObject;
    public float delayTime;

    public float shotForce = 1000f;
    public float moveSpeed = 10f;
    private bool waiting = false;

    void Update()
    {
        if (turretObject.GetComponent<EnemyController>().getFiring())
        {
            if (!waiting)
            {
                waiting = true;
                Quaternion turretRotation = turret.transform.rotation;

                Rigidbody shot = Instantiate(projectile, shotPos.position, turretRotation) as Rigidbody;

                shot.transform.LookAt(turret);

                shot.AddForce(shotPos.forward * shotForce);
                StartCoroutine(turretDelay());
            }
        }
    }

    IEnumerator turretDelay()
    {
        yield return new WaitForSeconds(delayTime);
        waiting = false;
    }
}