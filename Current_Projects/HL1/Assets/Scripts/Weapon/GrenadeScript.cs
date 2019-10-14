using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public GameObject gn_InHand;
    public GameObject gn_Projectile;
    public GameObject pinned;
    public GameObject unpinned;
    public GameObject gn_shotPos;
    public Light blinker;
    public ParticleSystem gg_explosionPrefab;
    public float gn_explosionRadius;
    public float gn_explosionPower;
    public AudioClip gn_explosionSFX;
    public AudioClip grenadeTick;

    private float blinkTimer;
    public float blinkTimeMax;

    private float cooldownRef;
    private float cooldown;

    private float maxThrowForce = 1000.0f;
    private float minThrowForce = 250.0f;
    private float currentThrowForce;

    private float gn_CurrentCookTime = 0.0f;
    public static float _CurrentCookTime;
    public static float gn_MaxCookTime = 6.0f;
    private float gn_ThrowCharge = 295.0f;

    private bool hasThrown;
    private bool gn_Active;
    private bool atMax;
    public static bool gn_explodeDrop;

    // Start is called before the first frame update
    void Start()
    {
        gn_shotPos = GameObject.Find("ShotPos");
        blinkTimer = 0.0f;
        hasThrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponScript.activeWeapon != null)
        {
            if (WeaponScript.activeWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("Grenade"))
            {
                
                if(gn_Active && !hasThrown)
                {
                    gn_CurrentCookTime += Time.deltaTime;

                    // PROJECTILE EXPLODES AFTER 6 SECONDS
                    if (gn_CurrentCookTime >= gn_MaxCookTime)
                    {
                        gn_explodeDrop = true;
                        currentThrowForce = 0.0f;
                        ThrowGrenade();
                    }   
                }

                if (WeaponScript.currentClipAmmo != 0 && !(WeaponScript.currentClipAmmo <= 0))
                {
                    // MAX THROW ASSIGNED IF CURRENT EXCEEDS MAX
                    if (currentThrowForce >= maxThrowForce && !hasThrown && !atMax)
                    {
                        print("At max force");
                        currentThrowForce = maxThrowForce;
                        atMax = true;
                    }
                    // PRIMING GRENADE
                    else if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        hasThrown = false;
                        gn_Active = true;
                        pinned.SetActive(false);
                        unpinned.SetActive(true);
                        currentThrowForce = minThrowForce;
                    }
                    // CHARGING THROW
                    else if (Input.GetKey(KeyCode.Mouse0) && !hasThrown && !atMax)
                    {
                        currentThrowForce += gn_ThrowCharge * Time.deltaTime;
                    }
                    // THROWING GRENADE
                    else if (Input.GetKeyUp(KeyCode.Mouse0) && !hasThrown)
                    {
                        ThrowGrenade();
                    }
                    if (Input.GetKey(KeyCode.Mouse1))
                    {
                        if (PlayerMovement.isCrouching)
                        {
                            print("I'm crounch throwing");

                        }
                        else
                        {
                            print("I'm not crounch throwing");
                        }
                    }
                }
                else if (WeaponScript.currentClipAmmo == 0 && WeaponScript.currentTotalAmmo != 0 && !(WeaponScript.currentTotalAmmo <= 0))
                {
                    WeaponScript.currentTotalAmmo--;
                    WeaponScript.currentClipAmmo++;
                    pinned.SetActive(true);
                    unpinned.SetActive(false);
                }
                else if (WeaponScript.currentTotalAmmo == 0)
                {
                    pinned.SetActive(true);
                    unpinned.SetActive(false);
                }

                // SAVES AMMO DATA EITHER WHEN SWITCHING WEAPONS
                WeaponScript.activeWeapon.GetComponent<WeaponStats>().weaponCurrentClipSize = WeaponScript.currentClipAmmo;
                WeaponScript.activeWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = WeaponScript.currentTotalAmmo;
                //gn_InHand.GetComponent<WeaponStats>().weaponCurrentClipSize = WeaponScript.currentClipAmmo;
                //gn_InHand.GetComponent<WeaponStats>().weaponCurrentAmmo = WeaponScript.currentTotalAmmo;
            }
        }
    }

    private void FixedUpdate()
    {
        if (unpinned.activeSelf)
        {
            blinkTimer += Time.deltaTime;

            // LIGHT BLINKS
            if (blinkTimer >= blinkTimeMax)
            {
                blinker.intensity = 10;
                StartCoroutine(SoundController.gunSounds(grenadeTick, 0));
                blinkTimer = 0.0f;
            }
            else
            {
                blinker.intensity = 1;
            }
        }
    }

    // THROW GRENADE
    private void ThrowGrenade()
    {
        hasThrown = true;
        gn_Active = false;
        atMax = false;
        Quaternion shotPosRotation = gn_shotPos.transform.rotation;
        Rigidbody projectileShot = Instantiate(gn_Projectile.GetComponent<Rigidbody>(), gn_shotPos.transform.position, shotPosRotation) as Rigidbody;
        projectileShot.transform.LookAt(gn_shotPos.transform.position);
        projectileShot.AddForce(gn_shotPos.transform.forward * currentThrowForce);
        _CurrentCookTime = gn_CurrentCookTime;
        gn_CurrentCookTime = 0.0f;
        WeaponScript.currentClipAmmo--;
    }
}