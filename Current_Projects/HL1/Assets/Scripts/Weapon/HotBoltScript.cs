using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBoltScript : MonoBehaviour
{
    private GameObject entity;
    private GameObject parent;
    private bool isBolted;
    public GameObject coreCylinder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale;
        coreCylinder.transform.localScale = coreCylinder.transform.localScale;

        if (isBolted)
        {
            /*this.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.transform.gameObject.GetComponent<Rigidbody>().Sleep();

            coreCylinder.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            coreCylinder.transform.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            coreCylinder.transform.gameObject.GetComponent<Rigidbody>().Sleep();

            Destroy(this.transform.gameObject.GetComponent<Rigidbody>());
            Destroy(this.transform.gameObject.GetComponent<CapsuleCollider>());
            Destroy(coreCylinder.transform.gameObject.GetComponent<CapsuleCollider>());
            isBolted = false;*/

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
        {
            if (col.transform.gameObject.GetComponent<EntityHealth>() != null)
            {
                entity = col.transform.gameObject;
                entity.GetComponent<EntityHealth>().entityCurrentHealth -= 1000;
            }
            else if (col.transform.parent.transform.gameObject.GetComponent<EntityHealth>() != null)
            {
                entity = col.transform.parent.transform.gameObject;
                entity.GetComponent<EntityHealth>().entityCurrentHealth -= 1000;
            }
        }

        //transform.parent = col.transform;
        //parent = col.gameObject;
        isBolted = true;
    }
}
