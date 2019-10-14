using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    // PRIMARY WEAPON FIRE STATS

    public string weaponName;
    public string getWeaponName() { return weaponName; }

    public float weaponDamage;
    public float getWeaponDamage() { return weaponDamage; }

    public float weaponRange;
    public float getWeaponRange() { return weaponRange; }

    public float weaponFireCooldown;
    public float getWeaponFireCooldown() { return weaponFireCooldown; }

    public float weaponSpread;
    public float getWeaponSpread() { return weaponSpread; }

    public int weaponBulletShots;
    public int getWeaponBulletShots() { return weaponBulletShots; }

    public int weaponCurrentClipSize;
    public int getWeaponCurrentClipSize() { return weaponCurrentClipSize; }

    public int weaponMaxClipSize;
    public int getWeaponMaxClipSize() { return weaponMaxClipSize; }

    public int weaponCurrentAmmo;
    public int getWeaponCurrentAmmo() { return weaponCurrentAmmo; }

    public int weaponMaxAmmo;
    public int getWeaponMaxAmmo() { return weaponMaxAmmo; }

    public float weaponForce;
    public float getWeaponForce() { return weaponForce; }

    // ALT WEAPON FIRE STATS

    public bool weaponHasAltFire;
    public bool isWeaponHasAltFire() { return weaponHasAltFire; }

    public bool weaponAltUsesPrimeAmmo;
    public bool isWeaponUsingPrimeAmmo() { return weaponAltUsesPrimeAmmo; }

    public bool weaponAltInstantiates;
    public bool isWeaponAltInstantiate() { return weaponAltInstantiates; }

    public float altWeaponDamage;
    public float getAltWeaponDamage() { return altWeaponDamage; }

    public float altWeaponRange;
    public float getAltWeaponRange() { return altWeaponRange; }

    public float altWeaponFireCooldown;
    public float getAltWeaponFireCooldown() { return altWeaponFireCooldown; }

    public float altWeaponSpread;
    public float getAltWeaponSpread() { return altWeaponSpread; }

    public int altWeaponBulletShots;
    public int getAltWeaponBulletShots() { return altWeaponBulletShots; }

    public int altWeaponCurrentAmmo;
    public int getAltWeaponCurrentAmmo() { return altWeaponCurrentAmmo; }

    public int altWeaponMaxAmmo;
    public int getAltWeaponMaxAmmo() { return altWeaponMaxAmmo; }

    public float altWeaponForce;
    public float getAltWeaponForce() { return altWeaponForce; }

    public GameObject projectile;
    public GameObject getProjectile() { return projectile; }

    // CHECK WEAPON STATUS

    public bool weaponPickedUp;
    public bool isWeaponPickedUp() { return weaponPickedUp; }

    public bool weaponActive;
    public bool isWeaponActive() { return weaponActive; }

    public bool isAnAmmoBox;
    public bool isAmmoBox() { return isAnAmmoBox; }

    public bool isAnAmmoCrate;
    public bool isAmmoCrate() { return isAnAmmoCrate; }



    //AUDIO
    //============================================================
    public AudioClip fireSFX;
    public AudioClip getFireSFX() { return fireSFX; }

    public AudioClip reloadSFX;
    public AudioClip getReloadSFX() { return reloadSFX; }

    public AudioClip emptySFX;
    public AudioClip getEmptySFX() { return emptySFX; }

    public AudioClip ammoPickUpSFX;
    public AudioClip getammoPickUpSFX() { return ammoPickUpSFX; }

    public AudioClip projectileSFX;
    public AudioClip getProjectileSFX() { return projectileSFX; }
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
            string weaponTag = "Player" + this.gameObject.GetComponent<WeaponStats>().getWeaponName();
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
            if (tempWeapon.GetComponent<WeaponStats>().isWeaponPickedUp() != true && !isAnAmmoBox && !isAnAmmoCrate)
            {
                print("First time picked up " + tempWeapon);
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
                bool isCrowbar = tempWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("Crowbar");
                bool isGravGun = tempWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("GravityGun");

                // IF WEAPON IS NOT CROWBAR OR GRAV GUN, UPDATE PLAYER WEAPON UI
                if (!WeaponScript.weaponSwitch && !isAnAmmoBox && !isAnAmmoCrate)
                {
                    if (!isGravGun)
                    {
                        WeaponScript.currentClipAmmo = this.gameObject.GetComponent<WeaponStats>().getWeaponCurrentClipSize();

                        tempWeapon.GetComponent<WeaponStats>().weaponCurrentClipSize = this.gameObject.GetComponent<WeaponStats>().getWeaponCurrentClipSize();

                        WeaponScript.currentTotalAmmo = this.gameObject.GetComponent<WeaponStats>().getWeaponCurrentAmmo();

                        tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().getWeaponCurrentAmmo();

                        FindAmmoType(this.gameObject.GetComponent<WeaponStats>().getWeaponName());

                        WeaponScript.currentTotalAltAmmo = this.gameObject.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo();
                        tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo();

                        WeaponScript.weaponSwitch = true;
                    }
                }

                // UPDATE WEAPON PICK UP UI
                FindWeaponPickUp(tempWeapon.GetComponent<WeaponStats>().getWeaponName());

                // CHECK THAT WEAPON HAS NOW BEEN PICKED UP BY PLAYER
                tempWeapon.GetComponent<WeaponStats>().weaponPickedUp = true;

                // PLAYS PICK UP NOISE
                StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));

                // REMOVES THE WORLDVIEW OBJECT
                Destroy(this.gameObject);

            }
            // IF WEAPON WAS ALREADY PICKED UP
            else if (tempWeapon.GetComponent<WeaponStats>().isWeaponPickedUp() == true)
            {
                print("Already picked up " + tempWeapon);
                // CHECK IF IT CAN PICK UP IT'S AMMO
                if (!isAnAmmoCrate && !WeaponScript.weaponSwitch)
                {
                    // PICK UP PRIMARY AMMO
                    if ((tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
                    {
                        WeaponAmmoPickUp(tempWeapon);
                        FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
                        StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                        Destroy(this.gameObject);
                    }
                    // PICK UP SECONDARY AMMO
                    else if (tempWeapon.GetComponent<WeaponStats>().isWeaponHasAltFire() && (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo()))
                    {
                        WeaponAmmoPickUp(tempWeapon);
                        FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
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
                        if ((tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
                        {
                            WeaponAmmoPickUp(tempWeapon);
                            FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
                            //indicates items has been picked up
                            StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                            ResupplyCrateScript.isResupplying = true;
                        }
                        // PICK UP SECONDARY AMMO
                        else if (tempWeapon.GetComponent<WeaponStats>().isWeaponHasAltFire() && (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo()))
                        {
                            WeaponAmmoPickUp(tempWeapon);
                            FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
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
        if ((this.GetComponent<WeaponStats>().getWeaponCurrentAmmo() != 0 || this.GetComponent<WeaponStats>().getWeaponCurrentClipSize() != 0) && (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() <= tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
        {
            int thisAmmoTotal = this.GetComponent<WeaponStats>().getWeaponCurrentAmmo() + this.GetComponent<WeaponStats>().getWeaponCurrentClipSize();
            print("TAT = " + thisAmmoTotal);
            int currentAmmoTotal = tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo();
            print("CAT = " + currentAmmoTotal);
            if ((thisAmmoTotal + currentAmmoTotal) > tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo())
            {
                print("Filled Max Ammo with " + (thisAmmoTotal + currentAmmoTotal));
                tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().weaponMaxAmmo;
                WeaponScript.weaponSwitch = true;
            }
            else
            {
                print("Got Min Ammo " + (tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo + thisAmmoTotal));
                tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo += thisAmmoTotal;
                WeaponScript.weaponSwitch = true;
            }
        }
        else if ((tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() == tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
        {
            print("Ammo for " + tempWeapon.name + " is already full!");
        }
        else if ((this.GetComponent<WeaponStats>().getWeaponCurrentAmmo() == 0))
        {
            print("No ammo to pick up!");
        }

        // ALT WEAPON AMMO PICK UP 
        if (tempWeapon.GetComponent<WeaponStats>().isWeaponHasAltFire())
        {
            if (!tempWeapon.GetComponent<WeaponStats>().isWeaponUsingPrimeAmmo())
            {
                if ((this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != 0 && this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != 0) && (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() <= tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo()))
                {
                    int thisAltAmmoTotal = this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo();

                    int currentAltAmmoTotal = tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo();


                    if ((thisAltAmmoTotal + currentAltAmmoTotal) > tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo())
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
                else if ((tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() == tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo()))
                {
                    print("Alt Ammo for " + tempWeapon.name + " is already full!");
                }
                else if ((this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() == 0))
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
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            case "Shotgun":
                WeaponScript.PickUpText.text += "sb" + 
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString() 
                + "\n";
                break;
            case "SMG":
                WeaponScript.PickUpText.text += "qa" +
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString()
                + "t" +
                (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo().ToString())
                + "\n";
                break;
            case "CombineRifle":
                WeaponScript.PickUpText.text += "ul" +
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString()
                + "z" +
                (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo().ToString())
                + "\n";
                break;
            case "GravityGun":
                WeaponScript.PickUpText.text += "";
                break;
            case "357":
                WeaponScript.PickUpText.text += "ea" +
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            case "Crossbow":
                WeaponScript.PickUpText.text += "gw" + 
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString()
                + "\n";
                break;
            case "Grenade":
                WeaponScript.PickUpText.text += "kv" +
                (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentClipSize() + tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo()).ToString()
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