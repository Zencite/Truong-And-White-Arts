using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPlayer : MonoBehaviour
{
    public int healingRate;
    public int totalHealCharge;
    public float healDelayRef;
    public static float healDelay;
    public Light healthLight;

    public TextMesh blueScreen;
    public bool isStation;
    public Renderer screen;

    public static bool atStation;
    public AudioClip smallMedKit;
    public AudioClip medChargeYes;
    public AudioClip medCharge;
    public AudioClip medChargeNo;

    // Start is called before the first frame update
    void Start()
    {
        healDelay = healDelayRef;
        atStation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStation)
        {
            blueScreen.text = ">TRUONG & WHITE" +
                                "\n\n>HEAL CHARGE: "
                                + totalHealCharge.ToString() + "%" +
                                "\n\n" +
                                "> NOTE: THIS DEVICE IS FOR\n" +
                                "> TEMPORARY MEDICAL\n" +
                                "> ASSISTANCE AND IS NOT A\n" +
                                "> REPLACEMENT TO LIFE\n" +
                                "> THREATING INJURIES.";

            if (totalHealCharge == 0)
            {
                blueScreen.text = "";
                screen.GetComponent<Renderer>().material.color = Color.black;
                healthLight.intensity = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isStation)
            {
                if (PlayerHealth.playerHealth < PlayerHealth.maxHealth)
                {
                    if (Time.time > healDelayRef)
                    {
                        healDelayRef = Time.time + healDelay;

                        if ((PlayerHealth.playerHealth + healingRate) >= PlayerHealth.maxHealth)
                        {
                            PlayerHealth.playerHealth = 100;
                            //print("Player is now at max health: " + PlayerHealth.playerHealth);
                            SoundController.playerHeadAudioSource.PlayOneShot(smallMedKit);
                            WeaponScript.PickUpText.text = "+";
                            Destroy(this.gameObject);
                        }
                        else if ((PlayerHealth.playerHealth + healingRate) < PlayerHealth.maxHealth)
                        {
                            PlayerHealth.playerHealth = PlayerHealth.playerHealth + this.healingRate;
                            //print("Player is now at " + PlayerHealth.playerHealth);
                            SoundController.playerHeadAudioSource.PlayOneShot(smallMedKit);
                            WeaponScript.PickUpText.text = "+";
                            Destroy(this.gameObject);
                        }
                    } 
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        

        if(other.gameObject.tag == "Player")
        {
            //print("Player is in my zone");

            if (isStation)
            {
                atStation = true;
                //print("At station is " + atStation);

                if (Input.GetKey("e") && PlayerSight.lookingAtStation)
                {
                    if (PlayerHealth.playerHealth < PlayerHealth.maxHealth && totalHealCharge != 0)
                    {
                        PlayerHealth.playerHealth = PlayerHealth.playerHealth + healingRate;
                        totalHealCharge = totalHealCharge - healingRate;
                        if (!(SoundController.playerHeadAudioSource.isPlaying))
                        {
                            SoundController.playerHeadAudioSource.PlayOneShot(medChargeYes);
                        }
                    }
                    else if (PlayerHealth.playerHealth == PlayerHealth.maxHealth)
                    {
                        if (!(SoundController.playerHeadAudioSource.isPlaying))
                        {
                            SoundController.playerHeadAudioSource.PlayOneShot(medChargeNo);
                        }
                    }
                    else if (totalHealCharge == 0)
                    {
                        if (!(SoundController.playerHeadAudioSource.isPlaying))
                        {
                            SoundController.playerHeadAudioSource.PlayOneShot(medChargeNo);
                        }
                    }
                }
            }
            else
            {
                atStation = false;
            }
        }
    }
}
