using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    // PRIMARY WEAPON FIRE STATS

    public string weaponName;
    public string GetWeaponName() { return weaponName; }

    public float weaponDamage;
    public float GetWeaponDamage() { return weaponDamage; }

    public float weaponRange;
    public float GetWeaponRange() { return weaponRange; }

    public float weaponFireCooldown;
    public float GetWeaponFireCooldown() { return weaponFireCooldown; }

    public float weaponSpread;
    public float GetWeaponSpread() { return weaponSpread; }

    public int weaponBulletShots;
    public int GetWeaponBulletShots() { return weaponBulletShots; }

    public int weaponCurrentClipSize;
    public int GetWeaponCurrentClipSize() { return weaponCurrentClipSize; }

    public int weaponMaxClipSize;
    public int GetWeaponMaxClipSize() { return weaponMaxClipSize; }

    public int weaponCurrentAmmo;
    public int GetWeaponCurrentAmmo() { return weaponCurrentAmmo; }

    public int weaponMaxAmmo;
    public int GetWeaponMaxAmmo() { return weaponMaxAmmo; }

    public float weaponForce;
    public float GetWeaponForce() { return weaponForce; }

    // ALT WEAPON FIRE STATS

    public bool weaponHasAltFire;
    public bool IsWeaponHasAltFire() { return weaponHasAltFire; }

    public bool weaponAltUsesPrimeAmmo;
    public bool IsWeaponUsingPrimeAmmo() { return weaponAltUsesPrimeAmmo; }

    public bool weaponAltInstantiates;
    public bool IsWeaponAltInstantiate() { return weaponAltInstantiates; }

    public float altWeaponDamage;
    public float GetAltWeaponDamage() { return altWeaponDamage; }

    public float altWeaponRange;
    public float GetAltWeaponRange() { return altWeaponRange; }

    public float altWeaponFireCooldown;
    public float GetAltWeaponFireCooldown() { return altWeaponFireCooldown; }

    public float altWeaponSpread;
    public float GetAltWeaponSpread() { return altWeaponSpread; }

    public int altWeaponBulletShots;
    public int GetAltWeaponBulletShots() { return altWeaponBulletShots; }

    public int altWeaponCurrentAmmo;
    public int GetAltWeaponCurrentAmmo() { return altWeaponCurrentAmmo; }

    public int altWeaponMaxAmmo;
    public int GetAltWeaponMaxAmmo() { return altWeaponMaxAmmo; }

    public float altWeaponForce;
    public float GetAltWeaponForce() { return altWeaponForce; }

    public GameObject projectile;
    public GameObject GetProjectile() { return projectile; }

    // CHECK WEAPON STATUS

    public bool weaponPickedUp;
    public bool IsWeaponPickedUp() { return weaponPickedUp; }

    public bool weaponActive;
    public bool IsWeaponActive() { return weaponActive; }

    public bool isAnAmmoBox;
    public bool IsAmmoBox() { return isAnAmmoBox; }

    public bool isAnAmmoCrate;
    public bool IsAmmoCrate() { return isAnAmmoCrate; }



    //AUDIO
    //============================================================
    public AudioClip fireSFX;
    public AudioClip GetFireSFX() { return fireSFX; }

    public AudioClip reloadSFX;
    public AudioClip GetReloadSFX() { return reloadSFX; }

    public AudioClip emptySFX;
    public AudioClip GetEmptySFX() { return emptySFX; }

    public AudioClip ammoPickUpSFX;
    public AudioClip GetAmmoPickUpSFX() { return ammoPickUpSFX; }

    public AudioClip projectileSFX;
    public AudioClip GetProjectileSFX() { return projectileSFX; }
    //============================================================

    private GameObject tempWeapon;

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            weaponActive = true;
        }
        else
        {
            weaponActive = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // PICK UP WEAPONS/PICKUPS
        if (other.tag.Equals("Player"))
        {
            string weaponTag = "Player" + this.gameObject.GetComponent<WeaponStats>().GetWeaponName();
            Transform gunCam = GameObject.Find("GunCam").transform;

            // SEARCH FOR WEAPON WITH SAME TAG IN THE CHILDREN POOL OF GUNCAM
            foreach (Transform child in gunCam)
            {
                // IF FOUND, ASSIGN IT TO TEMPWEAPON
                if (child.gameObject.tag.Equals(weaponTag))
                {
                    tempWeapon = child.gameObject;
                }
            }

            // IF PICKED UP FIRST TIME, MAKE ACTIVE WEAPON
            if (tempWeapon.GetComponent<WeaponStats>().IsWeaponPickedUp() != true && !isAnAmmoBox && !isAnAmmoCrate)
            {
                // DEACTIVES ANY OTHER WEAPON IN CHILDREN POOL OF GUNCAM
                foreach (Transform child in gunCam)
                {
                    if (child.gameObject.activeInHierarchy == true)
                    {
                        child.gameObject.GetComponent<WeaponStats>().weaponActive = false;
                        child.gameObject.SetActive(false);
                    }
                }
                // SETS NEW GUN AS ACTIVE WEAPON
                tempWeapon.SetActive(true);
                WeaponScript.activeWeapon = tempWeapon;

                // IF NEW WEAPON PICKED UP, UPDATE PLAYER WEAPON UI
                if (!WeaponScript.weaponSwitch && !isAnAmmoBox && !isAnAmmoCrate)
                {
                    UpdateCurrentUI(tempWeapon);
                    WeaponScript.weaponSwitch = true;
                }

                // UPDATE WEAPON PICK UP UI
                FindWeaponPickUp(tempWeapon.GetComponent<WeaponStats>().GetWeaponName());

                // CHECK THAT WEAPON HAS NOW BEEN PICKED UP BY PLAYER
                tempWeapon.GetComponent<WeaponStats>().weaponPickedUp = true;

                // PLAYS PICK UP NOISE
                StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));

                // REMOVES THE WORLDVIEW OBJECT
                Destroy(this.gameObject);

            }
            // IF WEAPON WAS ALREADY PICKED UP
            else if (tempWeapon.GetComponent<WeaponStats>().IsWeaponPickedUp() == true)
            {
                // CHECK IF IT CAN PICK UP IT'S AMMO
                if (!isAnAmmoCrate && !WeaponScript.weaponSwitch)
                {
                    // PICK UP PRIMARY AMMO
                    if ((tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().GetWeaponMaxAmmo()))
                    {
                        WeaponAmmoPickUp(tempWeapon);
                        FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().GetWeaponName());
                        StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                        Destroy(this.gameObject);
                    }
                    // PICK UP SECONDARY AMMO
                    else if (tempWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire() && (tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().GetAltWeaponMaxAmmo()))
                    {
                        WeaponAmmoPickUp(tempWeapon);
                        FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().GetWeaponName());
                        StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                        Destroy(this.gameObject);
                    }
                }

                // PICK UP AMMO FROM SUPPLY CRATES
                else if (isAnAmmoCrate && !WeaponScript.weaponSwitch)
                {
                    // CHECK IF CRATE IS NOT RESUPPLYING
                    if (!ResupplyCrateScript.isResupplying)
                    {
                        // PICK UP PRIMARY AMMO
                        if ((tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().GetWeaponMaxAmmo()))
                        {
                            WeaponAmmoPickUp(tempWeapon);
                            FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().GetWeaponName());
                            //indicates items has been picked up
                            StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                            ResupplyCrateScript.isResupplying = true;
                        }
                        // PICK UP SECONDARY AMMO
                        else if (tempWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire() && (tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().GetAltWeaponMaxAmmo()))
                        {
                            WeaponAmmoPickUp(tempWeapon);
                            FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().GetWeaponName());
                            //indicates items has been picked up
                            StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                            ResupplyCrateScript.isResupplying = true;
                        }
                    }
                }
            }
        }
    }
    public void WeaponAmmoPickUp(GameObject tempWeapon)
    {
        // PRIME WEAPON AMMO PICK UP
        if ((this.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() != 0 || this.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() != 0) && (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() <= tempWeapon.GetComponent<WeaponStats>().GetWeaponMaxAmmo()))
        {
            int thisAmmoTotal = this.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() + this.GetComponent<WeaponStats>().GetWeaponCurrentClipSize();
            int currentAmmoTotal = tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo();
            if ((thisAmmoTotal + currentAmmoTotal) > tempWeapon.GetComponent<WeaponStats>().GetWeaponMaxAmmo())
            {
                tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().weaponMaxAmmo;
                WeaponScript.weaponSwitch = true;
            }
            else
            {
                tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo += thisAmmoTotal;
                WeaponScript.weaponSwitch = true;
            }
        }
        else if ((tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() == tempWeapon.GetComponent<WeaponStats>().GetWeaponMaxAmmo()))
        {
            print("Ammo for " + tempWeapon.name + " is already full!");
        }
        else if ((this.GetComponent<WeaponStats>().GetWeaponCurrentAmmo() == 0))
        {
            print("No ammo to pick up!");
        }

        // ALT WEAPON AMMO PICK UP 
        if (tempWeapon.GetComponent<WeaponStats>().IsWeaponHasAltFire())
        {
            if (!tempWeapon.GetComponent<WeaponStats>().IsWeaponUsingPrimeAmmo())
            {
                if ((this.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() != 0 && this.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() != 0) && (tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() <= tempWeapon.GetComponent<WeaponStats>().GetAltWeaponMaxAmmo()))
                {
                    int thisAltAmmoTotal = this.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo();

                    int currentAltAmmoTotal = tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo();


                    if ((thisAltAmmoTotal + currentAltAmmoTotal) > tempWeapon.GetComponent<WeaponStats>().GetAltWeaponMaxAmmo())
                    {
                        tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().altWeaponMaxAmmo;
                        WeaponScript.weaponSwitch = true;
                    }
                    else
                    {
                        tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo += thisAltAmmoTotal;
                        WeaponScript.weaponSwitch = true;
                    }
                }
                else if ((tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() == tempWeapon.GetComponent<WeaponStats>().GetAltWeaponMaxAmmo()))
                {
                    print("Alt Ammo for " + tempWeapon.name + " is already full!");
                }
                else if ((this.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo() == 0))
                {
                    print("No alt ammo to pick up!");
                }
            }
        }
    }

    // UPDATE PICK UP UI
    public void FindWeaponPickUp(string weaponName)
    {
        switch (weaponName)
        {
            case "Crowbar":
                WeaponScript.PickUpText.text += "c\n";
                break;
            case "Pistol":
                WeaponScript.PickUpText.text += "d\n";
                break;
            case "Shotgun":
                WeaponScript.PickUpText.text += "b\n";
                break;
            case "SMG":
                WeaponScript.PickUpText.text += "a\n";
                break;
            case "CombineRifle":
                WeaponScript.PickUpText.text += "l\n";
                break;
            case "GravityGun":
                WeaponScript.PickUpText.text += "m\n";
                break;
            case "357":
                WeaponScript.PickUpText.text += "e\n";
                break;
            case "Crossbow":
                WeaponScript.PickUpText.text += "g\n";
                break;
            case "Grenade":
                WeaponScript.PickUpText.text += "k\n";
                break;
            default:
                break;
        }
    }

    void UpdateCurrentUI(GameObject tempWeapon)
    {
        WeaponScript.currentClipAmmo = this.gameObject.GetComponent<WeaponStats>().GetWeaponCurrentClipSize();
        tempWeapon.GetComponent<WeaponStats>().weaponCurrentClipSize = this.gameObject.GetComponent<WeaponStats>().GetWeaponCurrentClipSize();

        WeaponScript.currentTotalAmmo = this.gameObject.GetComponent<WeaponStats>().GetWeaponCurrentAmmo();

        tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().GetWeaponCurrentAmmo();
        FindAmmoType(this.gameObject.GetComponent<WeaponStats>().GetWeaponName());

        WeaponScript.currentTotalAltAmmo = this.gameObject.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo();
        tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo();
    }

    // UPDATE AMMO PICK UP UI
    public void FindAmmoPickUp(GameObject tempWeapon, string weaponName)
    {
        switch (weaponName)
        {
            case "Crowbar":
                WeaponScript.PickUpText.text = "";
                break;
            case "Pistol":
                WeaponScript.PickUpText.text += "pd" + 
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            case "Shotgun":
                WeaponScript.PickUpText.text += "sb" + 
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString() 
                + "\n";
                break;
            case "SMG":
                WeaponScript.PickUpText.text += "qa" +
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString()
                + "t" +
                (tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo().ToString())
                + "\n";
                break;
            case "CombineRifle":
                WeaponScript.PickUpText.text += "ul" +
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString()
                + "z" +
                (tempWeapon.GetComponent<WeaponStats>().GetAltWeaponCurrentAmmo().ToString())
                + "\n";
                break;
            case "GravityGun":
                WeaponScript.PickUpText.text += "";
                break;
            case "357":
                WeaponScript.PickUpText.text += "ea" +
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            case "Crossbow":
                WeaponScript.PickUpText.text += "gw" + 
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            case "Grenade":
                WeaponScript.PickUpText.text += "kv" +
                (tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().GetWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            default:
                break;
        }
    }

    // UPDATE AMMO TYPE UI
    public static void FindAmmoType(string weaponName)
    { 
        switch (weaponName)
        {
            case "Crowbar":
                WeaponScript.AmmoTypeIcon.text = "";
                break;
            case "Pistol":
                WeaponScript.AmmoTypeIcon.text = "p";
                break;
            case "Shotgun":
                WeaponScript.AmmoTypeIcon.text = "s";
                break;
            case "SMG":
                WeaponScript.AmmoPrimTypeIcon.text = "q";
                WeaponScript.AmmoAltTypeIcon.text = "t";
                break;
            case "CombineRifle":
                WeaponScript.AmmoPrimTypeIcon.text = "u";
                WeaponScript.AmmoAltTypeIcon.text = "z";
                break;
            case "GravityGun":
                WeaponScript.AmmoTypeIcon.text = "";
                break;
            case "357":
                WeaponScript.AmmoTypeIcon.text = "q";
                break;
            case "Crossbow":
                WeaponScript.AmmoTypeIcon.text = "w";
                break;
            case "Grenade":
                WeaponScript.AmmoTypeIcon.text += "v";
                break;
            default:
                break;
        }
    }
}