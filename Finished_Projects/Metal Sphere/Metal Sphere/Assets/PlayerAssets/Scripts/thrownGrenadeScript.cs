using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownGrenadeScript : MonoBehaviour
{
    public float detonationTimer = 3f;
    private float detonationTimerRefrence = 0f;
    private bool grenadeDetonated = false;

    void Start()
    {
        detonationTimerRefrence = Time.time + detonationTimer;
    }

    void FixedUpdate()
    {
        if ((Time.time > detonationTimerRefrence) && !grenadeDetonated)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
                if (gameObject.transform.GetChild(i).gameObject.name == "Collider")
                    gameObject.transform.GetChild(i).gameObject.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1f);
            grenadeDetonated = true;
        }
    }
}
