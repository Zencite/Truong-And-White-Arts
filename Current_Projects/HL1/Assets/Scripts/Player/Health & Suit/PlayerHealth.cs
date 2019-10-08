using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static int playerHealth;
    public static int currentHealth = 100;
    public static int maxHealth = 100;

    public static int playerSuit;
    public static int currentSuit = 0;
    public static int maxSuit = 100;

    public float damageThreshold;

    public GameObject healthObject;
    public GameObject suitObject;
    public static GameObject gravPos;

    public Text healthNumber;
    public Text suitNumber;

    public static bool hasSuit;
    public AudioClip suitPickUp;

    private void Awake()
    {
        healthObject = GameObject.Find("HealthObject");
        suitObject = GameObject.Find("SuitObject");
        gravPos = GameObject.Find("GravPos");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = currentHealth;
        playerSuit = currentSuit;

        healthObject.gameObject.SetActive(false);
        suitObject.gameObject.SetActive(false);
        gravPos.SetActive(false);

        hasSuit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSuit)
        {
            healthObject.gameObject.SetActive(true);
            suitObject.gameObject.SetActive(true);
            healthNumber.text = playerHealth.ToString();
            suitNumber.text = playerSuit.ToString();

            if (WeaponScript.activeWeapon != null)
            {
                if (!(WeaponScript.activeWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("Crowbar")))
                {
                    if (!(WeaponScript.activeWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("GravityGun")))
                    {
                        gravPos.SetActive(false);
                        if (!WeaponScript.activeWeapon.GetComponent<WeaponStats>().isWeaponUsingPrimeAmmo() && WeaponScript.activeWeapon.GetComponent<WeaponStats>().isWeaponHasAltFire())
                        {
                            WeaponScript.AmmoObject.SetActive(false);
                            WeaponScript.AmmoAltObject.SetActive(true);
                        }
                        else if (WeaponScript.activeWeapon.GetComponent<WeaponStats>().isWeaponUsingPrimeAmmo())
                        {
                            WeaponScript.AmmoAltObject.SetActive(false);
                            WeaponScript.AmmoObject.SetActive(true);
                        }
                        else if (!WeaponScript.activeWeapon.GetComponent<WeaponStats>().isWeaponHasAltFire())
                        {
                            WeaponScript.AmmoAltObject.SetActive(false);
                            WeaponScript.AmmoObject.SetActive(true);
                        }
                    }
                    else
                    {
                        WeaponScript.AmmoObject.SetActive(false);
                        WeaponScript.AmmoAltObject.SetActive(false);
                        gravPos.SetActive(true);
                    }
                }
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag.Equals("Suit"))
        {
            hasSuit = true;
            col.gameObject.SetActive(false);
            StartCoroutine(SoundController.suitNoise(suitPickUp, 0f));
        }
        // CHECKS FOR KINETIC DAMAGE ON PLAYER
        if(col.gameObject.GetComponent<Rigidbody>() != null)
        {
            float kineticDamage =  KineticEnergy(col.gameObject.GetComponent<Rigidbody>());
            if(kineticDamage > damageThreshold)
            {
                playerHealth = playerHealth - Mathf.RoundToInt(kineticDamage);
            }
        }
    }
    // CALCULATES THE KINETIC DAMAGE
    public static float KineticEnergy(Rigidbody rb)
    {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }
}
