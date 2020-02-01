using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBoltScript : MonoBehaviour
{
    private GameObject entity;
    private GameObject parent;
    private bool isBolted;
    public GameObject coreCylinder;
    private Transform colParent;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale;
        coreCylinder.transform.localScale = coreCylinder.transform.localScale;
    }

    private void OnCollisionEnter(Collision col)
    {
        // CROSSBOW COLLIDES
        print(col.transform.name);
        if (col.gameObject.tag != "Player")
        {
            if (col.transform.gameObject.GetComponent<EntityHealth>() != null)
            {
                print("1");
                CrossbowKill(col.transform.gameObject);

            }
            if (col.transform.parent)
            {
                if (col.transform.parent.transform.gameObject.GetComponent<EntityHealth>() != null)
                {
                    print("2");
                    CrossbowKill(col.transform.parent.transform.gameObject);
                }
            }
        }
    }

    private void CrossbowKill(GameObject entity)
    {
        print(entity);
        entity.GetComponent<EntityHealth>().entityCurrentHealth -= 1000;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = entity.transform;
        Destroy(transform.GetComponent<Rigidbody>());
    }

}
