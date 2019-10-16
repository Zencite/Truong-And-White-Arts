using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDeleteScript : MonoBehaviour
{
    private float timer;
    public float timerMax;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // PROJECTILE EXPLODES ANYWAY AFTER 5 SECONDS
        if (timer >= timerMax)
        {
            Destroy(this.gameObject);
        }
    }
}
