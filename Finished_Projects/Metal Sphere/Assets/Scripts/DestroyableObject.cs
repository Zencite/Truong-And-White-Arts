using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public int count;
    public GameObject teleporter;
    public GameObject baseMap;

    public float objectHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        teleporter.gameObject.SetActive(false);
        baseMap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectHealth <= 0)
        {
            count += 1;
            this.gameObject.SetActive(false);
        }

        print(count);

        if (count >= 1)
        {
            teleporter.gameObject.SetActive(true);
            baseMap.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "killBox")
        {
            objectHealth -= 101f;
        }
    }
}
