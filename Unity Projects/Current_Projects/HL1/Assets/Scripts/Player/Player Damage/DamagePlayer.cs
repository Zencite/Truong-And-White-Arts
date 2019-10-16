using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageRate;
    public float damageDelayRef;
    private float damageDelay;

    // Start is called before the first frame update
    void Start()
    {
        damageDelay = damageDelayRef;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (PlayerHealth.playerSuit != 0)
            {
                PlayerHealth.playerSuit -= damageRate;
            }
            else if (PlayerHealth.playerHealth != 0)
            {
                PlayerHealth.playerHealth -= damageRate;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Time.time > damageDelayRef)
            {
                damageDelayRef = Time.time + damageDelay;

                if (PlayerHealth.playerSuit != 0)
                {
                    PlayerHealth.playerSuit -= damageRate;
                }
                else if (PlayerHealth.playerHealth != 0)
                {
                    PlayerHealth.playerHealth -= damageRate;
                }
            }
        }
    }
}
