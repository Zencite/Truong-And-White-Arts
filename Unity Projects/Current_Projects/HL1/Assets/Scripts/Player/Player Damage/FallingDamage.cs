using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    public Rigidbody playerRB;
    public AudioClip playerFallSFX;

    private bool isFalling;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFalling();
    }

    void CheckIfFalling()
    {
        RaycastHit hit;
        Ray fallingRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * 1000, Color.blue);

        if (Physics.Raycast(fallingRay, out hit, 1000))
        {
            float rayDistance = hit.distance;

            
            if (rayDistance > 8.0f)
            {
                isFalling = true;
                //print("Falling is " + rayDistance);

            }
            if (isFalling && (rayDistance <= 1.0f))
            {
                CheckFallDamage();
                if (!(SoundController.playerHeadAudioSource.isPlaying))
                {
                    StartCoroutine(SoundController.playerSound(playerFallSFX, 5.0f));
                }
            }
        }
    }

    void CheckFallDamage()
    {
        float fallingDamage = Vector3.Magnitude(playerRB.velocity);
        float yVel = transform.InverseTransformDirection(playerRB.velocity).y;
        if(fallingDamage < 0)
        {
            fallingDamage *= -1;
        }
        if(fallingDamage < 0.0000009)
        {
            fallingDamage *= 10000000;
        }

        print("yVel = " + yVel);
        PlayerHealth.playerHealth -= Mathf.CeilToInt(fallingDamage);
        isFalling = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        print("Collision relVelMag is " + col.relativeVelocity.magnitude);
    }
}
