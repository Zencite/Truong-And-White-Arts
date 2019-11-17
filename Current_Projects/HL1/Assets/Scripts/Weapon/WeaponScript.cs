using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    // NON ALT UI
    public static GameObject AmmoObject;
    public Text AmmoClipNumber;
    public Text AmmoTotalNumber;

    // ALT UI
    public static GameObject AmmoAltObject;
    public Text AmmoPrimClipNumber;
    public Text AmmoPrimTotalNumber;
    public Text AmmoAltAmmoNumber;

    // WEAPON UI
    public static Text PickUpText;
    public static Text AmmoTypeIcon;
    public static Text AmmoPrimTypeIcon;
    public static Text AmmoAltTypeIcon;

    // PRIMARY FIRE
    public int weaponBulletShots;
    public static int currentClipAmmo;
    public int MaxClipAmmo;
    public static int currentTotalAmmo;
    public int MaxTotalAmmo;
    public float weaponRange;

    // ALT FIRE
    public int weaponAltBulletShots;
    public static int currentTotalAltAmmo;
    public int MaxTotalAltAmmo;
    public float weaponAltRange;

    // PRIME STATS
    public static float cooldownRef;
    public static float cooldown;
    private float weaponSpread;
    public static float weaponForce;
    public float weaponDamage;

    // ALT STATS
    public static float altCooldownRef;
    public static float altCooldown;
    private float weaponAltSpread;
    public static float weaponAltForce;
    public float weaponAltDamage;
    public static GameObject projectile;

    // RAYCASTING
    private GameObject shotPos;
    private Vector3 randomizedVector;
    private RaycastHit endpointInfo;

    // WEAPON INFO
    private GameObject gunCam;
    private GameObject firstPersonCamera;
    private GameObject gunCamera;
    private int weaponLayerMask;
    public static GameObject activeWeapon;
    public static GameObject muzzleFlash;
    public static bool weaponSwitch;
    public GameObject bulletHole;

    // GUN RECOIL
    public static GameObject playerGunCam;
    public static float gunCamRotateX;
    public static float n_gunCamRotateX;
    static float t = 0.0f;
    public static bool recoil;

    // CROSSBOW
    private GameObject crossA;
    private GameObject crossUA;
    public RawImage crossScope;
    public static bool isScoped;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonCamera = GameObject.Find("FirstPersonCamera");
        gunCamera = GameObject.Find("GunCamera");
        playerGunCam = GameObject.Find("GunCam");
        weaponLayerMask = gunCamera.GetComponent<Camera>().cullingMask;

        shotPos = GameObject.Find("ShotPos");                                               //Where the raycast for weapons shots from
        gunCam = GameObject.Find("GunCam");                                                 //The empty gameobject that holds all the weapons
        PickUpText = GameObject.Find("PickUpText").GetComponent<Text>();                    //UI that updates on new weapon pick ups or ammo

        AmmoTypeIcon = GameObject.Find("AmmoType").GetComponent<Text>();
        // PRIME WEAPON UI
        AmmoObject = GameObject.Find("AmmoObject");                                         //Gameobject that determines visibility of UI
        AmmoObject.SetActive(false);                                                        //Player starts without suit e.g. no UI

        AmmoPrimTypeIcon = GameObject.Find("AmmoPrimType").GetComponent<Text>();
        AmmoAltTypeIcon = GameObject.Find("AmmoAltType").GetComponent<Text>();
        // ALT WEAPON UI
        AmmoAltObject = GameObject.Find("AmmoAltObject");                                   //Gameobject that determines visibility of UI
        AmmoAltObject.SetActive(false);                                                     //Player starts without suit e.g. no UI

        weaponSwitch = false;                                                               //Checks is the weapon has been switched
    }
    void Update()
    {
        // recoil for guns
        if (recoil)
        {
            playerGunCam.transform.localRotation = Quaternion.Euler((Mathf.Lerp(-n_gunCamRotateX, gunCamRotateX, t)), 0.0f, 0.0f);
            playerGunCam.transform.localPosition = new Vector3 (0.0f, (Mathf.Lerp(0.085f, 0.0f, t)), 0.0f);
            t += 3.25f * Time.deltaTime;
            if (t > 1.0f)
            {
                t = 0.0f;
                playerGunCam.transform.localRotation = Quaternion.Euler(gunCamRotateX, 0.0f, 0.0f);
                playerGunCam.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                recoil = false;
            }
        }
    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // IF PLAYER HAS A WEAPON
        if (activeWeapon != null)
        {
            string nameCheck = activeWeapon.GetComponent<WeaponStats>().GetWeaponName();
            if (!(nameCheck.Equals("Crowbar")))
            {           
                if (weaponSwitch)
                {
                    // ACTIVATES PLAYER WEAPON UI IF NOT CROWBAR OR GRAVGUN
                    if (PlayerHealth.hasSuit)
                    {
                        // CHECKS FOR HUD UPDATE
                        CheckHUD(activeWeapon);
                    }

                    // ASSIGN PRIMARY STATS
                    AssignPrimeStats(activeWeapon);

                    // ASSIGN ALTERNATIVE STATS
                    weaponAltBulletShots = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponBulletShots();
                    weaponAltRange = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponRange();

                    altCooldownRef = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponFireCooldown();
                    altCooldown = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponFireCooldown();

                    weaponAltForce = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponForce();
                    weaponAltSpread = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponSpread();
                    weaponAltDamage = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponDamage();

                    // CHECKS IF USING PRIMARY AMMO FOR ALT FIRE
                    if (!activeWeapon.GetComponent<WeaponStats>().IsWeaponUsingPrimeAmmo())
                    {
                        currentTotalAltAmmo = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo();
                        MaxTotalAltAmmo = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponMaxAmmo();
                    }

                    // CHECKS IF ALT INSTANTIATES A PROJECTILE
                    if (activeWeapon.GetComponent<WeaponStats>().IsWeaponAltInstantiate() || nameCheck.Equals("Crossbow"))
                    {
                        projectile = activeWeapon.GetComponent<WeaponStats>().GetProjectile();
                        if (nameCheck.Equals("Crossbow"))
                        {
                            crossA = GameObject.Find("CrossArmed");
                            crossUA = GameObject.Find("CrossUnarmed");
                        }
                    }

                    weaponSwitch = false;
                    print("Weapon Script: weapon switch is " + weaponSwitch);
                }

                if (activeWeapon.GetComponent<WeaponStats>().IsWeaponUsingPrimeAmmo() || !activeWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire())
                {
                    AmmoClipNumber.text = currentClipAmmo.ToString();
                    AmmoTotalNumber.text = currentTotalAmmo.ToString();
                }
                else if (activeWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire())
                {
                    AmmoPrimClipNumber.text = currentClipAmmo.ToString();
                    AmmoPrimTotalNumber.text = currentTotalAmmo.ToString();
                    AmmoAltAmmoNumber.text = currentTotalAltAmmo.ToString();
                }

                if (!PlayerSight.isHolding && !PlayerSight.isZoomed)
                {
                    if (!(nameCheck.Equals("Grenade")))
                    {
                        // PRIMARY FIRE WEAPON (MOUSE 0)
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            if (!nameCheck.Equals("Crossbow"))
                            {
                                if (currentClipAmmo != 0 && !(currentClipAmmo < 0))
                                {
                                    if (Time.time > cooldownRef)
                                    {
                                        cooldownRef = Time.time + cooldown;

                                        currentClipAmmo--;

                                        for (int i = 0; i < weaponBulletShots; i++)
                                        {
                                            // CROUCHING INCREASES PLAYER AIM  
                                            if (PlayerMovement.isCrouching)
                                            {
                                                weaponSpread = Mathf.CeilToInt(weaponSpread / 1.5f);
                                                weaponForce = weaponForce / 1.5f;
                                            }
                                            else
                                            {
                                                weaponForce = activeWeapon.GetComponent<WeaponStats>().GetWeaponForce();
                                                weaponSpread = activeWeapon.GetComponent<WeaponStats>().GetWeaponSpread();
                                            }
                                            // WEAPON FIRE SPREAD AND DEBUG
                                            randomizedVector = RandomInsideCone(weaponSpread) * transform.forward;
                                            Debug.DrawRay(shotPos.transform.position, randomizedVector * weaponRange, Color.red, 1);

                                            if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                                            {
                                                float rayRange = endpointInfo.distance;

                                                if (rayRange <= weaponRange)
                                                {
                                                    // CHECKS WEAPON DAMAGE AND FORCE AT ENDPOINT
                                                    WeaponRayCastHit(endpointInfo, weaponForce, weaponDamage);

                                                }
                                            }
                                        }
                                        AudioClip fireSFX = activeWeapon.GetComponent<WeaponStats>().GetFireSFX();
                                        StartCoroutine(SoundController.gunSounds(fireSFX, cooldown));
                                        WeaponRecoil(weaponForce);
                                    }
                                }
                                else
                                {
                                    if (Time.time > cooldownRef)
                                    {
                                        cooldownRef = Time.time + cooldown;
                                        AudioClip emptySFX = activeWeapon.GetComponent<WeaponStats>().GetEmptySFX();
                                        StartCoroutine(SoundController.gunSounds(emptySFX, cooldown));
                                    }
                                }
                            }
                            else if (nameCheck.Equals("Crossbow"))
                            {
                                // CROSSBOW FIRE, INSTANTIATE BOLT
                                if (currentClipAmmo != 0 && !(currentClipAmmo < 0))
                                {
                                    if (Time.time > cooldownRef)
                                    {
                                        cooldownRef = Time.time + cooldown;

                                        for (int i = 0; i < weaponBulletShots; i++)
                                        {
                                            Quaternion shotPosRotation = shotPos.transform.rotation;
                                            Rigidbody projectileShot = Instantiate(projectile.GetComponent<Rigidbody>(), shotPos.transform.position, shotPosRotation) as Rigidbody;
                                            projectileShot.transform.LookAt(shotPos.transform.position);
                                            projectileShot.AddForce(shotPos.transform.forward * weaponForce);
                                            currentClipAmmo--;
                                        }

                                        AudioClip fireSFX = activeWeapon.GetComponent<WeaponStats>().GetFireSFX();
                                        StartCoroutine(SoundController.gunSounds(fireSFX, cooldown));

                                        if (activeWeapon.GetComponent<WeaponStats>().GetWeaponName().Equals("Crossbow"))
                                        {
                                            crossA.SetActive(false);
                                            crossUA.SetActive(true);
                                        }
                                    }
                                }
                                else
                                {
                                    if (activeWeapon.GetComponent<WeaponStats>().GetWeaponName().Equals("Crossbow"))
                                    {
                                        crossA.SetActive(false);
                                        crossUA.SetActive(true);
                                    }
                                }
                            }
                        }
                    }

                    if (!(nameCheck.Equals("Grenade")))
                    {
                        // ALT FIRE FOR WEAPONS (MOUSE 2)
                        if (Input.GetKey(KeyCode.Mouse1))
                        {
                            // IF CROSSBOW, SCOPE IN
                            if (activeWeapon.GetComponent<WeaponStats>().GetWeaponName().Equals("Crossbow"))
                            {
                                switch(isScoped)
                                {
                                    case true:
                                        gunCamera.GetComponent<Camera>().cullingMask = weaponLayerMask;
                                        crossScope.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                                        firstPersonCamera.GetComponent<Camera>().fieldOfView = 70f;
                                        isScoped = false;
                                        break;
                                    case false:
                                        gunCamera.GetComponent<Camera>().cullingMask = 0;
                                        firstPersonCamera.GetComponent<Camera>().fieldOfView = 10f;
                                        crossScope.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                                        isScoped = true;
                                        break;
                                }
                            }

                            // WEAPONS ALT FIRE
                            if (activeWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire())
                            {
                                if (activeWeapon.GetComponent<WeaponStats>().IsWeaponUsingPrimeAmmo())
                                {
                                    if (currentClipAmmo != 0 && !(currentClipAmmo < 0))
                                    {
                                        if (Time.time > altCooldownRef)
                                        {
                                            altCooldownRef = Time.time + altCooldown;

                                            currentClipAmmo--;

                                            for (int i = 0; i < weaponAltBulletShots; i++)
                                            {
                                                // CROUCHING INCREASES AIM
                                                if (PlayerMovement.isCrouching)
                                                {
                                                    weaponAltSpread = Mathf.CeilToInt(weaponAltSpread / 1.5f);
                                                    weaponAltForce = weaponAltForce / 1.5f;
                                                }
                                                else
                                                {
                                                    weaponAltForce = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponForce();
                                                    weaponAltSpread = activeWeapon.GetComponent<WeaponStats>().GetAltWeaponSpread();
                                                }
                                                // CREATE RANDOM HITSCAN SPREAD & DEBUG RAYCAST RAYS
                                                randomizedVector = RandomInsideCone(weaponAltSpread) * transform.forward;
                                                Debug.DrawRay(shotPos.transform.position, randomizedVector * weaponAltRange, Color.red, 1);

                                                if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                                                {
                                                    float rayRange = endpointInfo.distance;

                                                    if (rayRange <= weaponAltRange)
                                                    {
                                                        WeaponRayCastHit(endpointInfo, weaponAltForce, weaponAltDamage);
                                                    }
                                                }
                                            }
                                            AudioClip projectileSFX = activeWeapon.GetComponent<WeaponStats>().GetProjectileSFX();
                                            StartCoroutine(SoundController.gunSounds(projectileSFX, altCooldown));
                                            WeaponRecoil(weaponAltForce);
                                        }
                                    }
                                    else
                                    {
                                        if (Time.time > altCooldownRef)
                                        {
                                            altCooldownRef = Time.time + altCooldown;
                                            AudioClip emptySFX = activeWeapon.GetComponent<WeaponStats>().GetEmptySFX();
                                            StartCoroutine(SoundController.gunSounds(emptySFX, altCooldown));
                                        }
                                    }
                                }
                                else if (activeWeapon.GetComponent<WeaponStats>().IsWeaponAltInstantiate())
                                {
                                    if (currentTotalAltAmmo != 0 && !(currentTotalAltAmmo < 0))
                                    {
                                        if (Time.time > altCooldownRef)
                                        {
                                            altCooldownRef = Time.time + altCooldown;
                                            for (int i = 0; i < weaponAltBulletShots; i++)
                                            {
                                                Quaternion shotPosRotation = shotPos.transform.rotation;
                                                Rigidbody projectileShot = Instantiate(projectile.GetComponent<Rigidbody>(), shotPos.transform.position, shotPosRotation) as Rigidbody;
                                                projectileShot.transform.LookAt(shotPos.transform.position);
                                                projectileShot.AddForce(shotPos.transform.forward * weaponAltForce);
                                                currentTotalAltAmmo--;

                                                AudioClip projectileSFX = activeWeapon.GetComponent<WeaponStats>().GetProjectileSFX();
                                                StartCoroutine(SoundController.gunSounds(projectileSFX, altCooldown));
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (Time.time > altCooldownRef)
                                        {
                                            altCooldownRef = Time.time + altCooldown;
                                            AudioClip emptySFX = activeWeapon.GetComponent<WeaponStats>().GetEmptySFX();
                                            StartCoroutine(SoundController.gunSounds(emptySFX, altCooldown));
                                        }
                                    }
                                }
                            }
                        }

                        // SAVES AMMO DATA EITHER WHEN SWITCHING WEAPONS
                        activeWeapon.GetComponent<WeaponStats>().weaponCurrentClipSize = currentClipAmmo;
                        activeWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo = currentTotalAltAmmo;

                        // RELOAD AMMO AND UPDATE PLAYER UI HUD
                        if (Input.GetKey("r"))
                        {
                            if (!isScoped)
                            {
                                if (currentClipAmmo != MaxClipAmmo)
                                {
                                    if (currentTotalAmmo != 0)
                                    {
                                        if (activeWeapon.GetComponent<WeaponStats>().GetWeaponName().Equals("Crossbow"))
                                        {
                                            if (crossA != null && crossUA != null)
                                            {
                                                crossA.SetActive(true);
                                                crossUA.SetActive(false);
                                            }
                                        }

                                        int reloadNumber = (MaxClipAmmo - currentClipAmmo);

                                        AudioClip reloadSFX = activeWeapon.GetComponent<WeaponStats>().GetReloadSFX();
                                        StartCoroutine(SoundController.gunSounds(reloadSFX, cooldown));

                                        if ((currentTotalAmmo - reloadNumber) < 0)
                                        {
                                            currentClipAmmo += currentTotalAmmo;

                                            currentTotalAmmo = 0;
                                        }
                                        else if ((currentTotalAmmo - reloadNumber) >= 0)
                                        {
                                            currentTotalAmmo = (currentTotalAmmo - reloadNumber);

                                            currentClipAmmo += reloadNumber;
                                        }
                                    }
                                }
                            }
                        }
                        activeWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = currentTotalAmmo;
                    }
                }
            }
            // IF CROWBAR IS ACTIVE WEAPON
            else if (activeWeapon.GetComponent<WeaponStats>().GetWeaponName().Equals("Crowbar"))
            {
                if (weaponSwitch)
                {
                    AmmoObject.SetActive(false);
                    AmmoAltObject.SetActive(false);

                    AssignPrimeStats(activeWeapon);

                    weaponSwitch = false;
                }

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Time.time > cooldownRef)
                    {
                        cooldownRef = Time.time + cooldown;

                        //Firing SFX is assigned to clip
                        AudioClip fireSFX = activeWeapon.GetComponent<WeaponStats>().GetFireSFX();
                        StartCoroutine(SoundController.gunSounds(fireSFX, cooldown));

                        //visualize the raycast
                        randomizedVector = RandomInsideCone(weaponSpread) * transform.forward;
                        Debug.DrawRay(shotPos.transform.position, randomizedVector * weaponRange, Color.blue, 1);

                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            float rayRange = endpointInfo.distance;

                            if (rayRange <= weaponRange)
                            {
                                WeaponRayCastHit(endpointInfo, weaponForce, weaponDamage);
                            }
                        }
                    }
                }
            }
        }

        if (activeWeapon != null)
        {
            // TODO MAKE MOUSEWHEEL SCROLL ALSO CHANGE WEAPONS
            if (!PlayerSight.isHolding && !PlayerSight.isZoomed && !isScoped)
            {
                if(Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    //print("minus 1");
                }
                if(Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    //print("plus 1");
                }
            }

            // TODO MAKE MORE ROOM FOR WEAPON SELECTION
            if (!PlayerSight.isHolding && !PlayerSight.isZoomed && !isScoped)
            {
                //Switch to Crowbar if player already has it
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    string weaponTag = "PlayerCrowbar";
                    SwitchWeapon(weaponTag);
                }

                //Switch to Pistol if player has it
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    string weaponTag = "PlayerPistol";
                    SwitchWeapon(weaponTag);
                }

                //Switch to Shotgun if player has it
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    string weaponTag = "PlayerShotgun";
                    SwitchWeapon(weaponTag);
                }

                //Switch to SMG if player has it
                if (Input.GetKey(KeyCode.Alpha4))
                {
                    string weaponTag = "PlayerSMG";
                    SwitchWeapon(weaponTag);
                }

                //Switch to CombineRifle if player has it
                if (Input.GetKey(KeyCode.Alpha5))
                {
                    string weaponTag = "PlayerCombineRifle";
                    SwitchWeapon(weaponTag);
                }

                //Switch to Gravity Gun if player has it
                if (Input.GetKey(KeyCode.Alpha6))
                {
                    string weaponTag = "PlayerGravityGun";
                    SwitchWeapon(weaponTag);
                }
                //Switch to 357 if player has it
                if (Input.GetKey(KeyCode.Alpha7))
                {
                    string weaponTag = "Player357";
                    SwitchWeapon(weaponTag);
                }
                //Switch to crossbow if player has it
                if (Input.GetKey(KeyCode.Alpha8))
                {
                    string weaponTag = "PlayerCrossbow";
                    SwitchWeapon(weaponTag);
                }
                //Switch to grenade if player has it
                if (Input.GetKey(KeyCode.Alpha9))
                {
                    string weaponTag = "PlayerGrenade";
                    SwitchWeapon(weaponTag);
                }
            }
        }
    }
    // ASSIGNS THE PRIMARY STATS FOR WEAPONS FROM WEAPON STATS
    public void AssignPrimeStats(GameObject activeWeapon)
    {
        weaponBulletShots = activeWeapon.GetComponent<WeaponStats>().GetWeaponBulletShots();
        weaponRange = activeWeapon.GetComponent<WeaponStats>().GetWeaponRange();

        MaxClipAmmo = activeWeapon.GetComponent<WeaponStats>().GetWeaponMaxClipSize();
        currentClipAmmo = activeWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize();

        MaxTotalAmmo = activeWeapon.GetComponent<WeaponStats>().GetWeaponMaxAmmo();
        currentTotalAmmo = activeWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo();

        cooldownRef = activeWeapon.GetComponent<WeaponStats>().GetWeaponFireCooldown();
        cooldown = activeWeapon.GetComponent<WeaponStats>().GetWeaponFireCooldown();

        weaponForce = activeWeapon.GetComponent<WeaponStats>().GetWeaponForce();
        weaponSpread = activeWeapon.GetComponent<WeaponStats>().GetWeaponSpread();

        weaponDamage = activeWeapon.GetComponent<WeaponStats>().GetWeaponDamage();
    }

    // RANDOMIZES RAYCASTS FOR HITSCAN WEAPONS
    Quaternion RandomInsideCone(float radius)
    {
        Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(-radius, radius), Vector3.up);
        Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(-radius, radius), Vector3.right);
        return (randomSpin * randomTilt);
    }

    // IF NEW WEAPON IS PICKED UP, CHANGE IT TO ACTIVE WEAPON
    public GameObject ChangeActiveWeapon(GameObject tempWeapon)
    {
        foreach (Transform child in gunCam.transform)
        {
            if (child.gameObject.activeInHierarchy == true)
            {
                child.gameObject.GetComponent<WeaponStats>().weaponActive = false;
                child.gameObject.SetActive(false);
            }
        }
        tempWeapon.gameObject.SetActive(true);
        activeWeapon = tempWeapon;
        weaponSwitch = true;
        return activeWeapon;
    }

    // SWITCHES WEAPONS BY WEAPON TAG
    public void SwitchWeapon(string weaponTag)
    {
        GameObject tempWeapon;
        foreach (Transform child in gunCam.transform)
        {
            if (child.gameObject.tag.Equals(weaponTag))
            {
                tempWeapon = child.gameObject;
                if (tempWeapon.GetComponent<WeaponStats>().IsWeaponPickedUp() == true)
                {
                    if (tempWeapon.GetComponent<WeaponStats>().IsWeaponActive() != true)
                    {
                        ChangeActiveWeapon(tempWeapon);
                        string weaponName = tempWeapon.GetComponent<WeaponStats>().GetWeaponName();
                        WeaponStats.FindAmmoType(weaponName);
                    }
                }
            }
        }
    }

    // CHECKS RAYCAST BULLET ON ENDPOINT
    public void WeaponRayCastHit(RaycastHit endpointInfo, float weaponForce, float weaponDamage)
    {
        GameObject myBulletHole = Instantiate(bulletHole, endpointInfo.point, Quaternion.LookRotation(endpointInfo.normal));
        int _weaponDamage = Mathf.CeilToInt(weaponDamage);

        if (endpointInfo.transform.gameObject.GetComponent<EntityHealth>() != null)
        {
            GameObject entity = endpointInfo.transform.gameObject;
            GameObject tagEntity = endpointInfo.transform.gameObject;
            _weaponDamage = CheckHeadshot(tagEntity, _weaponDamage);
            entity.GetComponent<EntityHealth>().entityCurrentHealth -= _weaponDamage;
        }

        if (endpointInfo.transform.parent != null)
        {
            if (endpointInfo.transform.parent.transform.gameObject.GetComponent<EntityHealth>() != null)
            {
                GameObject entity = endpointInfo.transform.parent.transform.gameObject;
                GameObject tagEntity = endpointInfo.transform.gameObject;
                _weaponDamage = CheckHeadshot(tagEntity, _weaponDamage);
                entity.GetComponent<EntityHealth>().entityCurrentHealth -= _weaponDamage;
            }
        }

        myBulletHole.transform.parent = endpointInfo.transform;

        EntityHealth.entityEndpoint = endpointInfo;
        EntityHealth.entityWeaponForce = weaponForce;

        if (endpointInfo.rigidbody != null)
        {
            endpointInfo.rigidbody.AddForce(-endpointInfo.normal * weaponForce);
        }
    }

    // WEAPONS RECOIL "ANIMATION"
    public static void WeaponRecoil(float weaponForce)
    {
        gunCamRotateX = playerGunCam.transform.localRotation.x;
        float weaponRecoil = weaponForce / 200;
        n_gunCamRotateX = playerGunCam.transform.localRotation.x + weaponRecoil;
        playerGunCam.transform.localRotation = Quaternion.Euler(-n_gunCamRotateX, 0.0f, 0.0f);
        playerGunCam.transform.localPosition = new Vector3 (0.0f, 0.085f, 0.0f);
        recoil = true;
    }

    // CHECKS IF HEADSHOT HUMANOID
    private int CheckHeadshot(GameObject tagEntity, int _weaponDamage)
    {
        if (tagEntity.tag == "Head")
        {
            return (_weaponDamage * 2);
        }
        else
        {
            return _weaponDamage;
        }
    }

    private void CheckHUD(GameObject activeWeapon)
    {
        if (activeWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire())
        {
            if (!activeWeapon.GetComponent<WeaponStats>().IsWeaponUsingPrimeAmmo())
            {
                AmmoObject.SetActive(false);
                AmmoAltObject.SetActive(true);
            }
            else if (activeWeapon.GetComponent<WeaponStats>().IsWeaponUsingPrimeAmmo())
            {
                AmmoAltObject.SetActive(false);
                AmmoObject.SetActive(true);
            }
        }
        else
        {
            AmmoAltObject.SetActive(false);
            AmmoObject.SetActive(true);
        }
    }
}