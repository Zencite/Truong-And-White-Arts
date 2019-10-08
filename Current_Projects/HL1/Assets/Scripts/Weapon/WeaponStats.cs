using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    // PRIMARY WEAPON FIRE
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

    // ALT WEAPON FIRE

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

    // CHECK WEAPON

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

    // Update is called once per frame
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
        //Pick up weapon/pickups
        if (other.tag.Equals("Player"))
        {
            string weaponTag = "Player" + this.gameObject.GetComponent<WeaponStats>().getWeaponName();
            Transform gunCam = GameObject.Find("GunCam").transform;

            //search in gameobject gunCam for the weapon with the tag
            foreach (Transform child in gunCam)
            {
                //Find the weapon in the player's list
                if (child.gameObject.tag.Equals(weaponTag))
                {
                    tempWeapon = child.gameObject;
                }
            }

            //If weapon was picked up first time, enable it
            if (tempWeapon.GetComponent<WeaponStats>().isWeaponPickedUp() != true && !isAnAmmoBox && !isAnAmmoCrate)
            {
                //checks if another weapon was active when new weapon was picked up to deactivate
                foreach (Transform child in gunCam)
                {
                    //print(child);
                    if (child.gameObject.activeInHierarchy == true)
                    {
                        child.gameObject.GetComponent<WeaponStats>().weaponActive = false;
                        child.gameObject.SetActive(false);
                    }
                }
                //sets the weapon as the active weapon
                tempWeapon.SetActive(true);
                //assigns activeWeapon as the tempWeapon
                WeaponScript.activeWeapon = tempWeapon;
                bool isCrowbar = tempWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("Crowbar");
                bool isGravGun = tempWeapon.GetComponent<WeaponStats>().getWeaponName().Equals("GravityGun");

                //if Weapon is not the crowbar of Gravity Gun update the ammo UI
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

                //update the weapon pick up UI
                FindWeaponPickUp(tempWeapon.GetComponent<WeaponStats>().getWeaponName());

                //checks that this weapon has now been picked up by the player
                tempWeapon.GetComponent<WeaponStats>().weaponPickedUp = true;

                //indicates items has been picked up
                StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));

                //deletes the gameobject in world
                Destroy(this.gameObject);

            }
            //Weapon is already picked up
            else if (tempWeapon.GetComponent<WeaponStats>().isWeaponPickedUp() == true)
            {
                //If weapon was already picked up, pick up it's ammo stored in weaponStats script if it can
                if (!isAnAmmoCrate && !WeaponScript.weaponSwitch)
                {
                    //Pick ammo for primary ammo
                    if ((tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
                    {
                        WeaponAmmoPickUp(tempWeapon);
                        FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
                        StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                        Destroy(this.gameObject);
                    }
                    //Pick ammo for secondary
                    else if (tempWeapon.GetComponent<WeaponStats>().isWeaponHasAltFire() && (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo()))
                    {
                        WeaponAmmoPickUp(tempWeapon);
                        FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
                        StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                        Destroy(this.gameObject);
                    }
                }

                //Pick up ammo from refillable crates
                else if (isAnAmmoCrate && !WeaponScript.weaponSwitch)
                {
                    //Check if crate is in resupply mode
                    if (!ResupplyCrateScript.isResupplying)
                    {
                        //Pick ammo for primary ammo
                        if ((tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() != tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
                        {
                            WeaponAmmoPickUp(tempWeapon);
                            FindAmmoPickUp(tempWeapon, tempWeapon.GetComponent<WeaponStats>().getWeaponName());
                            //indicates items has been picked up
                            StartCoroutine(SoundController.gunSounds(ammoPickUpSFX, 0));
                            ResupplyCrateScript.isResupplying = true;
                        }
                        //Pick ammo for secondary ammo
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
        if ((this.GetComponent<WeaponStats>().getWeaponCurrentAmmo() != 0 || this.GetComponent<WeaponStats>().getWeaponCurrentClipSize() != 0) && (tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo() < tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo()))
        {
            int thisAmmoTotal = this.GetComponent<WeaponStats>().getWeaponCurrentAmmo() + this.GetComponent<WeaponStats>().getWeaponCurrentClipSize();
            //print("this ammo total is " + thisAmmoTotal);
            int currentAmmoTotal = tempWeapon.GetComponent<WeaponStats>().getWeaponCurrentAmmo();
            //print("current ammo total is " + currentAmmoTotal);

            if ((thisAmmoTotal + currentAmmoTotal) > tempWeapon.GetComponent<WeaponStats>().getWeaponMaxAmmo())
            {
                //print("Picked up max ammo: " + (thisAmmoTotal + currentAmmoTotal));
                tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().weaponMaxAmmo;
                WeaponScript.weaponSwitch = true;
                //print("Ammo is now " + tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo);
            }
            else
            {
                print("picked up min ammo: " + (thisAmmoTotal + currentAmmoTotal));
                tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo += thisAmmoTotal;
                WeaponScript.weaponSwitch = true;
                //print("Ammo is now " + tempWeapon.GetComponent<WeaponStats>().weaponCurrentAmmo);
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
                if ((this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != 0 && this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() != 0) && (tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo() < tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo()))
                {
                    int thisAltAmmoTotal = this.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo();
                    print("this alt ammo total is " + thisAltAmmoTotal);
                    int currentAltAmmoTotal = tempWeapon.GetComponent<WeaponStats>().getAltWeaponCurrentAmmo();
                    print("current alt ammo total is " + currentAltAmmoTotal);

                    if ((thisAltAmmoTotal + currentAltAmmoTotal) > tempWeapon.GetComponent<WeaponStats>().getAltWeaponMaxAmmo())
                    {
                        print("Picked up max alt ammo: " + (thisAltAmmoTotal + currentAltAmmoTotal));
                        tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo = this.gameObject.GetComponent<WeaponStats>().altWeaponMaxAmmo;
                        WeaponScript.weaponSwitch = true;
                        print("Alt Ammo is now " + tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo);
                    }
                    else
                    {
                        print("picked up min alt ammo: " + (thisAltAmmoTotal + currentAltAmmoTotal));
                        tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo += thisAltAmmoTotal;
                        WeaponScript.weaponSwitch = true;
                        print("Alt ammo is now " + tempWeapon.GetComponent<WeaponStats>().altWeaponCurrentAmmo);
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
    //Updates the weapon pick up icon
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
            default:
                break;
        }
    }
    //Updates the pick up icon to show ammo type
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
            default:
                break;
        }
    }

    //Updates the ammotype icon above "AmmoText"
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
            default:
                break;
        }
    }
}