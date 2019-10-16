using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePlayer : MonoBehaviour
{
    public int chargingRate;
    public int totalSuitCharge;
    public float chargeDelayRef;
    public static float chargeDelay;
    public Light chargeLightTank1;
    public Light chargeLightTank2;
    public Light chargeLightTank3;

    public GameObject chargeReady;
    public GameObject chargeEmpty;
    public bool isStation;

    public static bool atStation;
    public AudioClip batteryPack;
    public AudioClip suitChargeYes;
    public AudioClip suitCharge;
    public AudioClip suitChargeNo;

    // Start is called before the first frame update
    void Start()
    {
        if (isStation)
        {
            chargeReady.gameObject.SetActive(true);
            chargeEmpty.gameObject.SetActive(false);
        }
        chargeDelay = chargeDelayRef;
        atStation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStation)
        {
            if (totalSuitCharge == 0)
            {
                chargeReady.gameObject.SetActive(false);
                chargeEmpty.gameObject.SetActive(true);
                chargeLightTank1.intensity = 0;
                chargeLightTank2.intensity = 0;
                chargeLightTank3.intensity = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isStation)
            {
                if (PlayerHealth.playerSuit < PlayerHealth.maxSuit)
                {
                    if (Time.time > chargeDelayRef)
                    {
                        chargeDelayRef = Time.time + chargeDelay;

                        if ((PlayerHealth.playerSuit + chargingRate) >= PlayerHealth.maxSuit)
                        {
                            PlayerHealth.playerSuit = 100;
                            //print("Player suit is now at max: " + PlayerHealth.playerSuit);
                            SoundController.playerHeadAudioSource.PlayOneShot(batteryPack);
                            WeaponScript.PickUpText.text = "*";
                            Destroy(this.gameObject);
                        }
                        else if ((PlayerHealth.playerSuit + chargingRate) < PlayerHealth.maxSuit)
                        {
                            PlayerHealth.playerSuit = PlayerHealth.playerSuit + this.chargingRate;
                            //print("Player suit is now at " + PlayerHealth.playerSuit);
                            SoundController.playerHeadAudioSource.PlayOneShot(batteryPack);
                            WeaponScript.PickUpText.text = "*";
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            //print("Player is in my zone");

            if (isStation)
            {
                atStation = true;
                //print("At station is " + atStation);

                if (Input.GetKey("e") && PlayerSight.lookingAtStation)
                {
                    if (PlayerHealth.playerSuit < PlayerHealth.maxSuit && totalSuitCharge != 0)
                    {
                        PlayerHealth.playerSuit = PlayerHealth.playerSuit + chargingRate;
                        totalSuitCharge = totalSuitCharge - chargingRate;
                        if (!(SoundController.playerHeadAudioSource.isPlaying))
                        {
                            SoundController.playerHeadAudioSource.PlayOneShot(suitChargeYes);
                        }
                    }
                    else if (PlayerHealth.playerSuit == PlayerHealth.maxSuit)
                    {
                        if (!(SoundController.playerHeadAudioSource.isPlaying))
                        {
                            SoundController.playerHeadAudioSource.PlayOneShot(suitChargeNo);
                        }
                    }
                    else if (totalSuitCharge == 0)
                    {
                        if (!(SoundController.playerHeadAudioSource.isPlaying))
                        {
                            SoundController.playerHeadAudioSource.PlayOneShot(suitChargeNo);
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
