using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lifeCounter;

    // Start is called before the first frame update
    void Start()
    {
        lifeCounter = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeCounter <= 0)
        {
            lifeCounter = 0;
            print("I am dead");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            lifeCounter--;
            if (lifeCounter > 0)
            {
                PushBack();
            }
        }
    }

    void PushBack()
    {
        Vector3 pushBackPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(pushBackPosition, 1f);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(10f , pushBackPosition, 1f, 1f);
            }
        }
    }
}
