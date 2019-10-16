using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBoltScript : MonoBehaviour
{
    private GameObject entity;
    private GameObject parent;
    private bool isBolted;
    public GameObject coreCylinder;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale;
        coreCylinder.transform.localScale = coreCylinder.transform.localScale;

        if (isBolted)
        {
            // TODO GET THE BOLT TO STICK ONTO PEOPLE AND WALLS
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        // CROSSBOW COLLIDES
        if (col.gameObject.tag != "Player")
        {
            if (col.transform.gameObject.GetComponent<EntityHealth>() != null)
            {
                entity = col.transform.gameObject;
                entity.GetComponent<EntityHealth>().entityCurrentHealth -= 1000;
            }
            if (col.transform.parent)
            {
                if (col.transform.parent.transform.gameObject.GetComponent<EntityHealth>() != null)
                {
                    entity = col.transform.parent.transform.gameObject;
                    entity.GetComponent<EntityHealth>().entityCurrentHealth -= 1000;
                }
            }
        }
        isBolted = true;
    }
}
