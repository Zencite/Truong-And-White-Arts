using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//guard health is a thing, subtract from public health value and check if <= 0
//let william call stun funciton on gaurd
//dont worry about missing textures lewis can replace
//balance weapons/sound radius on your own
//refrence average guard health value is 100
//impliment raytrace radimization generation, weapons have a damage fallof

public class ToolController : MonoBehaviour
{
    //drag bullet prefab into this slot
    private GameObject mainCamera;
    private GameObject firstPersonCamera;

    //player rigidbody
    private Rigidbody playerRigidbody;

    //speed/sensitivity
    public float playerMaxHP = 100;
    private float playerCurrentHP = 100;
    public float aimSpeed = 4f;
    public float moveSpeed = 0.08f;
    public float icePickClimbSpeed = 0.04f;
    public float grapplingHookMoveSpeed = 0.12f;

    //child that renders the halo
    private GameObject playerHalo;

    //strings used to store weapons currently in slot
    private string firstWeaponSlot = "";
    private string secondWeaponSlot = "";
    private bool primaryWeaponEquiped = true;
    private bool currentWeaponAutomatic = false;

    //bools that store if specific tools have been unlocked
    private bool stunMineUnlocked = false;
    private bool grapplingHookUnlocked = false;
    private bool icePickUnlocked = false;
    private bool grenadeUnlocked = false;
    private string currentToolEquiped = "";

    //smoke trail fx
    public float smokeFxSpeed = 0f;
    public GameObject gunSmokeTrailFXPrefab;
    private GameObject gunSmokeTrailFXTempRef;

    //weapon mesh objects to be enabled/disabled to "equip" weapons
    private GameObject silencedPistol;
    private GameObject SMGChild;
    private GameObject CarbineChild;
    private GameObject shotgunChild;

    //weapon mesh objects to be enabled/disabled to "equip" weapons
    private GameObject silencedPistolHolstered;
    private GameObject SMGChildHolstered;
    private GameObject CarbineChildHolstered;
    private GameObject shotgunChildHolstered;

    //weapon mesh objects to be enabled/disabled to "equip" weapons
    private AudioSource silencedPistolAudioSource;
    private AudioSource SMGChildAudioSource;
    private AudioSource CarbineChildAudioSource;
    private AudioSource shotgunChildAudioSource;
    private AudioSource weaponSwapAudioSource;

    //weapon damage values
    public float pistolDamage = 20f;
    public float shotgunPerPelletDamage = 16f;
    public float carbineDamage = 36f;
    public float SMGFireDamage = 10f;

    //weapon fire rate/hitbox duration
    public float pistolFireCooldown = 0.25f;
    public float shotgunFireCooldown = 1f;
    public float carbineFireCooldown = 0.5f;
    public float SMGFireCooldown = 0.1f;

    //weapon fire rate/hitbox duration
    public float pistolFireSpread = 4;
    public float shotgunFireSpread = 18;
    public float shotgunNumPellets = 8;
    public float carbineFireSpread = 1;
    public float SMGFireSpread = 12;

    //raycasting vars
    private GameObject shotPos;
    private Vector3 randomizedVector;
    private RaycastHit endpointInfo;

    //grappling hook public vars
    public float grapplingHookProjectleForce = 12f;
    public float grapplingHookRange = 28f;
    public float grapplingHookDetachRange = 2f;
    public float grapplingHookJumpForce = 12f;
    public float grappingHookIcePickDuration = 3f;
    private float grappingHookIcePickRefrence = 0f;

    //grenade public vars
    public float grenadeThrowForce = 12f;

    //weapon fire rate/hitbox duration
    private float pistolFireCooldownRefrence = 0f;
    private float shotgunFireCooldownRefrence = 0f;
    private float carbineFireCooldownRefrence = 0f;
    private float SMGFireCooldownRefrence = 0f;

    //vars that affect weapon firing sound radius
    //private float silencedPistolRadius = 0f;
    //private float SMGChildRadius = 0f;
    //private float CarbineChildRadius = 0f;
    //private float shotgunChildRadius = 0f;
    private GameObject soundBubble;
    private SphereCollider soundBubbleCollider;

    //cooldown timer vars
    private float soundBubbleDurations = 0.2f;
    private float soundBubbleDurationTimerRefrence = 0f;
    private float weaponSwapCooldown = 0.2f;
    private float weaponSwapCooldownTimerRefrence = 0f;
    private float toolSwapCooldown = 0.2f;
    private float toolSwapCooldownTimerRefrence = 0f;
    public float stunMineCooldown = 8f;
    private float stunMineCooldownRefrence = 0f;
    public float grenadeCooldown = 6f;
    private float grenadeCooldownRefrence = 0f;
    public float grenadeDetonator = 3f;
    private float grenadeDetonatorRefrence = 0f;

    //tool mesh objects to be enabled/disabled to "equip" tools
    private GameObject stunMineChild;
    private GameObject grapplingHookChild;
    private GameObject grapplingHookChildHook;
    private GameObject grapplingHookProjectle;
    private Vector3 grapplingHookDirection;
    private GameObject icePickChild;
    private GameObject grenadeChild;
    private GameObject activeGrenadeRefrence;

    //refrences to prefabs that objects spawn
    public GameObject grapplingHookPrefab;
    private GameObject grapplingHookRefrence;
    public GameObject stunMinePrefab;
    private GameObject stunMineRefrence;
    private AudioSource stunMineDropSound;
    public GameObject thrownGrenadePrefab;
    private GameObject icePickDetectBox;

    //keybind bools used to trigger events based on user input
    private bool stunMineEquip;
    private bool grapplingHookEquip;
    private bool grapplingHookAttached;
    private bool grapplingHookIcePickActive = false;
    private bool icePickEquip;
    private bool grenadeEquip;
    private bool primeToolKeybind;
    private bool activateToolKeybind;
    private bool grapplingHookPrimed = false;
    private bool icePickPrimed = false;
    private bool stunMinePrimed = false;
    private bool grenadePrimed = false;
    private bool aimDownSights;
    private bool primaryFireKey;
    private bool swapWeaponKey;
    private bool pickupWeaponKey;
    private bool icePickClimb;
    private bool icePickDescend;
    private bool icePickActive;
    private bool icePickTempDisable;

    //camera and movement stuff
    private float moveHorizontal;
    private float moveZedAxis;
    private float mouseMoveHorizontal;
    private float mouseMoveVertical;
    private float currentYVelocity;
    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //gets children meshes to be toggled on/off
        //loops though the tranforms child objects
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            //checks the name of the object and assighns it to the correct private variable
            if (gameObject.transform.GetChild(i).gameObject.name == "SilencedGlock")
                silencedPistol = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "MP5")
                SMGChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "Famas")
                CarbineChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "Shotgun")
                shotgunChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "SilencedGlockHolstered")
                silencedPistolHolstered = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "MP5Holstered")
                SMGChildHolstered = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "FamasHolstered")
                CarbineChildHolstered = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "ShotgunHolstered")
                shotgunChildHolstered = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "Clicker")
                stunMineChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "GrappleGun")
                grapplingHookChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "IcePick")
                icePickChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "Grenade")
                grenadeChild = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "FPSCamera")
                firstPersonCamera = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "TPSCamera")
                mainCamera = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "Torus")
                playerHalo = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "SoundBubble")
                soundBubble = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "weaponSwapSound")
                weaponSwapAudioSource = gameObject.transform.GetChild(i).gameObject.GetComponent<AudioSource>();
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "icePickDetectBox")
                icePickDetectBox = gameObject.transform.GetChild(i).gameObject;
            else
            if (gameObject.transform.GetChild(i).gameObject.name == "shotPos")
                shotPos = gameObject.transform.GetChild(i).gameObject;
        }

        //gets the grappling hook model hook child
        for (int i = 0; i < grapplingHookChild.transform.childCount; i++)
        {
            //checks the name of the object and assighns it to the correct private variable
            if (grapplingHookChild.transform.GetChild(i).gameObject.name == "Hook")
                grapplingHookChildHook = grapplingHookChild.transform.GetChild(i).gameObject;
        }

        //gets the player rigidbody
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        soundBubbleCollider = soundBubble.GetComponent<SphereCollider>();

        //gets gun audioSouce Objects
        silencedPistolAudioSource = silencedPistol.GetComponent<AudioSource>();
        SMGChildAudioSource = SMGChild.GetComponent<AudioSource>();
        CarbineChildAudioSource = CarbineChild.GetComponent<AudioSource>();
        shotgunChildAudioSource = shotgunChild.GetComponent<AudioSource>();
        stunMineDropSound = stunMineChild.GetComponent<AudioSource>();

        //toggles on third person camera by default
        firstPersonCamera.SetActive(false);
        mainCamera.SetActive(true);

        //toggles all models off by defualt
        silencedPistol.SetActive(false);
        SMGChild.SetActive(false);
        CarbineChild.SetActive(false);
        shotgunChild.SetActive(false);
        silencedPistolHolstered.SetActive(false);
        SMGChildHolstered.SetActive(false);
        CarbineChildHolstered.SetActive(false);
        shotgunChildHolstered.SetActive(false);
        stunMineChild.SetActive(false);
        grapplingHookChildHook.SetActive(true);
        grapplingHookChild.SetActive(false);
        icePickChild.SetActive(false);
        icePickDetectBox.SetActive(false);
        grenadeChild.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "stunMinePickUp" && !stunMineUnlocked) || (other.tag == "GrapplingHookPickup" && !grapplingHookUnlocked) || (other.tag == "grenadePickup" && !grenadeUnlocked)
            || (other.tag == "IcePickPickup" && !icePickUnlocked))
        {
            toolSwapCooldownTimerRefrence = Time.time + toolSwapCooldown;
            weaponSwapAudioSource.Play();

            if (!grapplingHookPrimed)
            {
                //unequips current tool if applicable
                if (currentToolEquiped == "stunMine")
                {
                    stunMineChild.SetActive(false);
                }
                else
                if (currentToolEquiped == "grapplingHook")
                {
                    grapplingHookChild.SetActive(false);
                }
                else
                if (currentToolEquiped == "icePick")
                {
                    icePickChild.SetActive(false);
                }
                else
                if (currentToolEquiped == "grenade")
                {
                    grenadeChild.SetActive(false);
                }

                //equips the new selected tool
                if (other.tag == "stunMinePickUp")
                {
                    stunMineUnlocked = true;
                    stunMineChild.SetActive(true);
                    currentToolEquiped = "stunMine";
                }
                else
                if (other.tag == "GrapplingHookPickup")
                {
                    grapplingHookUnlocked = true;
                    grapplingHookChild.SetActive(true);
                    currentToolEquiped = "grapplingHook";
                }
                else
                if (other.tag == "grenadePickup")
                {
                    grenadeUnlocked = true;
                    icePickChild.SetActive(true);
                    currentToolEquiped = "icePick";
                }
                else
                if (other.tag == "IcePickPickup")
                {
                    icePickUnlocked = true;
                    grenadeChild.SetActive(true);
                    currentToolEquiped = "grenade";
                }
            }
            else
            {
                if (other.tag == "stunMinePickUp")
                    stunMineUnlocked = true;
                else
                if (other.tag == "GrapplingHookPickup")
                    grapplingHookUnlocked = true;
                else
                if (other.tag == "grenadePickup")
                    grenadeUnlocked = true;
                else
                if (other.tag == "IcePickPickup")
                    icePickUnlocked = true;
            }
        }
        icePickTempDisable = Input.GetKey(KeyCode.LeftControl);
        if ((icePickPrimed || grapplingHookIcePickActive) && (other.tag == "Environment"))
        {
            if (!icePickTempDisable)
                icePickActive = true;
            else
                icePickActive = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        pickupWeaponKey = Input.GetKey("r");
        if (((other.tag == "CarbinePickup") || (other.tag == "ShotgunPickup") || (other.tag == "PistolPickup") || (other.tag == "SMGPickup")) && pickupWeaponKey && (Time.time > weaponSwapCooldownTimerRefrence))
        {
            weaponSwapCooldownTimerRefrence = Time.time + weaponSwapCooldown;
            weaponSwapAudioSource.Play();

            if (primaryWeaponEquiped)
            {
                if (!((secondWeaponSlot == "Carbine") && (other.tag == "CarbinePickup")) && !((secondWeaponSlot == "SMG") && (other.tag == "SMGPickup"))
                && !((secondWeaponSlot == "SilencedPistol") && (other.tag == "PistolPickup")) && !((secondWeaponSlot == "Shotgun") && (other.tag == "ShotgunPickup")))
                {
                    //unequips primary
                    if (firstWeaponSlot == "SMG")
                    {
                        SMGChild.SetActive(false);
                    }
                    else
                    if (firstWeaponSlot == "SilencedPistol")
                    {
                        silencedPistol.SetActive(false);
                    }
                    else
                    if (firstWeaponSlot == "Carbine")
                    {
                        CarbineChild.SetActive(false);
                    }
                    else
                    if (firstWeaponSlot == "Shotgun")
                    {
                        shotgunChild.SetActive(false);
                    }

                    //swaps to and equips secondary
                    if (other.tag == "CarbinePickup")
                    {
                        firstWeaponSlot = "Carbine";
                        CarbineChild.SetActive(true);
                        currentWeaponAutomatic = false;
                    }
                    else
                    if (other.tag == "ShotgunPickup")
                    {
                        firstWeaponSlot = "Shotgun";
                        shotgunChild.SetActive(true);
                        currentWeaponAutomatic = false;
                    }
                    else
                    if (other.tag == "PistolPickup")
                    {
                        firstWeaponSlot = "SilencedPistol";
                        silencedPistol.SetActive(true);
                        currentWeaponAutomatic = false;
                    }
                    else
                    if (other.tag == "SMGPickup")
                    {
                        firstWeaponSlot = "SMG";
                        SMGChild.SetActive(true);
                        currentWeaponAutomatic = true;
                    }
                    primaryWeaponEquiped = true;
                }
            }
            else
            {
                if (!((firstWeaponSlot == "Carbine") && (other.tag == "CarbinePickup")) && !((firstWeaponSlot == "SMG") && (other.tag == "SMGPickup"))
                && !((firstWeaponSlot == "SilencedPistol") && (other.tag == "PistolPickup")) && !((firstWeaponSlot == "Shotgun") && (other.tag == "ShotgunPickup")))
                {
                    //unequips secondary
                    if (secondWeaponSlot == "SMG")
                    {
                        SMGChild.SetActive(false);
                    }
                    else
                    if (secondWeaponSlot == "SilencedPistol")
                    {
                        silencedPistol.SetActive(false);
                    }
                    else
                    if (secondWeaponSlot == "Carbine")
                    {
                        CarbineChild.SetActive(false);
                    }
                    else
                    if (secondWeaponSlot == "Shotgun")
                    {
                        shotgunChild.SetActive(false);
                    }

                    //swaps to and equips primary
                    if (other.tag == "CarbinePickup")
                    {
                        secondWeaponSlot = "Carbine";
                        CarbineChild.SetActive(true);
                        currentWeaponAutomatic = false;
                    }
                    else
                    if (other.tag == "ShotgunPickup")
                    {
                        secondWeaponSlot = "Shotgun";
                        shotgunChild.SetActive(true);
                        currentWeaponAutomatic = false;
                    }
                    else
                    if (other.tag == "PistolPickup")
                    {
                        secondWeaponSlot = "SilencedPistol";
                        silencedPistol.SetActive(true);
                        currentWeaponAutomatic = false;
                    }
                    else
                    if (other.tag == "SMGPickup")
                    {
                        secondWeaponSlot = "SMG";
                        SMGChild.SetActive(true);
                        currentWeaponAutomatic = true;
                    }
                    primaryWeaponEquiped = false;
                }
            }
        }
        icePickTempDisable = Input.GetKey(KeyCode.LeftControl);
        if ((icePickPrimed || grapplingHookIcePickActive) && (other.tag == "Environment"))
        {
            if (!icePickTempDisable)
                icePickActive = true;
            else
                icePickActive = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((icePickPrimed || grapplingHookIcePickActive) && (other.tag == "Environment"))
        {
            icePickActive = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //all movement input below
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveZedAxis = Input.GetAxisRaw("Vertical");
        mouseMoveHorizontal = Input.GetAxis("Mouse X");
        mouseMoveVertical = Input.GetAxis("Mouse Y");
        currentYVelocity = playerRigidbody.velocity.y;
        icePickClimb = Input.GetKey(KeyCode.Space);
        icePickDescend = Input.GetKey(KeyCode.LeftShift);

        //all tools per frame input below
        stunMineEquip = Input.GetKeyDown("3");
        grapplingHookEquip = Input.GetKeyDown("1");
        icePickEquip = Input.GetKeyDown("2");
        grenadeEquip = Input.GetKeyDown("4");
        primeToolKeybind = Input.GetKeyDown("q");
        activateToolKeybind = Input.GetKeyDown("e");
        aimDownSights = Input.GetMouseButton(1);
        swapWeaponKey = Input.GetKeyDown("f");

        //all tools stuff below
        //turns off sound if active
        if (Time.time > soundBubbleDurationTimerRefrence)
        {
            soundBubbleCollider.radius = 0.01f;
        }

        //checks for a singe frame where lmb was pressed or if held dependent on semi or auto mode
        if ((primaryWeaponEquiped && firstWeaponSlot != "") || (!primaryWeaponEquiped && secondWeaponSlot != ""))
        {
            if (currentWeaponAutomatic)
                primaryFireKey = Input.GetMouseButton(0);
            else
                primaryFireKey = Input.GetMouseButtonDown(0);
        }
        else
        {
            primaryFireKey = false;
        }

        //sets toolPrimed based on tool keybinds
        if (primeToolKeybind && (currentToolEquiped != ""))
        {
            if (!stunMinePrimed && currentToolEquiped == "stunMine" && (Time.time > stunMineCooldownRefrence))
            {
                stunMineRefrence = Instantiate(stunMinePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * 1, transform.rotation);
                stunMineDropSound.Play();
                stunMinePrimed = true;
            }
            else
            if (!grapplingHookPrimed && currentToolEquiped == "grapplingHook")
            {
                grapplingHookAttached = false;
                grapplingHookIcePickActive = false;
                grapplingHookChildHook.SetActive(false);
                grapplingHookProjectle = Instantiate(grapplingHookPrefab, transform.position, transform.rotation);
                grapplingHookProjectle.GetComponent<Rigidbody>().AddForce(transform.forward * grapplingHookProjectleForce, ForceMode.Impulse);
                grapplingHookPrimed = true;
            }
            else
            if (!icePickPrimed && currentToolEquiped == "icePick")
            {
                icePickDetectBox.SetActive(true);
                icePickPrimed = true;
            }
            else
            if (!grenadePrimed && currentToolEquiped == "grenade" && (Time.time > grenadeCooldownRefrence))
            {
                grenadeDetonatorRefrence = Time.time + grenadeDetonator;
                grenadeChild.SetActive(false);
                weaponSwapAudioSource.Play();
                activeGrenadeRefrence = Instantiate(thrownGrenadePrefab, gameObject.transform);
                activeGrenadeRefrence.transform.localPosition = new Vector3(0f, 0f, 0.6f);
                grenadePrimed = true;
            }
        }
        else
        if (activateToolKeybind)
        {
            if (stunMinePrimed && currentToolEquiped == "stunMine")
            {
                stunMineCooldownRefrence = Time.time + stunMineCooldown;
                for (int i = 0; i < stunMineRefrence.transform.childCount; i++)
                    if (stunMineRefrence.transform.GetChild(i).gameObject.name == "Collider")
                        stunMineRefrence.transform.GetChild(i).gameObject.SetActive(true);
                stunMineRefrence.GetComponent<AudioSource>().Play();
                Destroy(stunMineRefrence, 1);
                stunMinePrimed = false;
            }
            else
            if (grapplingHookPrimed && currentToolEquiped == "grapplingHook")
            {
                Destroy(grapplingHookProjectle);
                grapplingHookChildHook.SetActive(true);
                grapplingHookAttached = false;
                grapplingHookIcePickActive = true;
                playerRigidbody.useGravity = true;
                transform.eulerAngles = new Vector3(cameraRotationY, 0f, 0f);
                playerRigidbody.AddForce(transform.up * grapplingHookJumpForce, ForceMode.Impulse);
                icePickDetectBox.SetActive(true);
                grappingHookIcePickRefrence = Time.time + grappingHookIcePickDuration;
                grapplingHookPrimed = false;
            }
            else
            if (icePickPrimed && currentToolEquiped == "icePick")
            {
                icePickDetectBox.SetActive(false);
                icePickActive = false;
                icePickPrimed = false;
            }
            else
            if (grenadePrimed && currentToolEquiped == "grenade")
            {
                activeGrenadeRefrence.transform.parent = null;
                activeGrenadeRefrence.GetComponent<Rigidbody>().isKinematic = false;
                activeGrenadeRefrence.GetComponent<Rigidbody>().AddForce(transform.forward * grenadeThrowForce, ForceMode.Impulse);
                grenadeChild.SetActive(true);
                grenadePrimed = false;
                grenadeCooldownRefrence = Time.time + grenadeCooldown;
            }
        }

        //checks for grappling hook collision
        if (((currentToolEquiped == "grapplingHook") && grapplingHookPrimed) && !grapplingHookAttached)
            if (!grapplingHookProjectle.GetComponent<Rigidbody>().useGravity)
            {
                grapplingHookAttached = true;
                playerRigidbody.useGravity = false;
            }

        if (Time.time > grappingHookIcePickRefrence)
        {
            grapplingHookIcePickActive = false;
            if (!icePickPrimed)
            {
                icePickActive = false;
                icePickDetectBox.SetActive(false);
            }
        }

        if ((((currentToolEquiped == "grenade") && grenadePrimed) && (Time.time > grenadeDetonatorRefrence)) || (!(currentToolEquiped == "grenade") && grenadePrimed))
        {
            activeGrenadeRefrence.transform.parent = null;
            activeGrenadeRefrence.GetComponent<Rigidbody>().isKinematic = false;
            if (currentToolEquiped == "grenade")
                grenadeChild.SetActive(true);
            else
                grenadeChild.SetActive(false);
            grenadePrimed = false;
            grenadeCooldownRefrence = Time.time + grenadeCooldown;
        }

        if (grapplingHookAttached)
        {
            if (((transform.position - grapplingHookProjectle.transform.position).magnitude > grapplingHookRange) || ((transform.position - grapplingHookProjectle.transform.position).magnitude < grapplingHookDetachRange))
            {
                Destroy(grapplingHookProjectle);
                grapplingHookChildHook.SetActive(true);
                grapplingHookAttached = false;
                grapplingHookIcePickActive = true;
                playerRigidbody.useGravity = true;
                transform.eulerAngles = new Vector3(cameraRotationY, 0f, 0f);
                icePickDetectBox.SetActive(true);
                grappingHookIcePickRefrence = Time.time + grappingHookIcePickDuration;
                grapplingHookPrimed = false;
            }
            else
            {
                grapplingHookDirection = (grapplingHookProjectle.transform.position - transform.position) / (transform.position - grapplingHookProjectle.transform.position).magnitude;
                transform.rotation = Quaternion.LookRotation(grapplingHookDirection);
                transform.position += grapplingHookDirection * grapplingHookMoveSpeed;
                playerRigidbody.velocity = new Vector3(0f, 0f, 0f);
            }
        }
        else
        {
            if (!aimDownSights)
            {
                cameraRotationX += mouseMoveHorizontal * aimSpeed;
                cameraRotationY = 0f;
                if (icePickActive)
                {
                    playerRigidbody.useGravity = false;
                    playerRigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    playerRigidbody.useGravity = true;
                    playerRigidbody.velocity = new Vector3(0f, currentYVelocity, 0f);
                }
                transform.eulerAngles = new Vector3(cameraRotationY, cameraRotationX, 0f);
                transform.position += gameObject.transform.right * moveSpeed * moveHorizontal;
                transform.position += gameObject.transform.forward * moveSpeed * moveZedAxis;
                if (icePickClimb && icePickActive)
                    transform.position += gameObject.transform.up * icePickClimbSpeed;
                else
                if (icePickDescend && icePickActive)
                    transform.position -= gameObject.transform.up * icePickClimbSpeed;
            }
            else
            {
                cameraRotationX += mouseMoveHorizontal * aimSpeed;

                if (icePickActive)
                    playerRigidbody.useGravity = false;
                else
                    playerRigidbody.useGravity = true;

                if (moveZedAxis == 0)
                {
                    playerRigidbody.velocity = new Vector3(0f, 0f, 0f);
                    cameraRotationY -= mouseMoveVertical * aimSpeed;
                }
                else
                {
                    if (icePickActive)
                        playerRigidbody.velocity = new Vector3(0f, 0f, 0f);
                    else
                        playerRigidbody.velocity = new Vector3(0f, currentYVelocity, 0f);
                    cameraRotationY = 0f;
                }

                transform.eulerAngles = new Vector3(cameraRotationY, cameraRotationX, 0.0f);
                transform.position += gameObject.transform.right * moveSpeed * moveHorizontal;
                transform.position += gameObject.transform.forward * moveSpeed * moveZedAxis;
                if (cameraRotationY == 0)
                {
                    if (icePickClimb && icePickActive)
                        transform.position += gameObject.transform.up * icePickClimbSpeed;
                    else
                    if (icePickDescend && icePickActive)
                        transform.position -= gameObject.transform.up * icePickClimbSpeed;
                }
            }
        }

        if (aimDownSights)
        {
            mainCamera.SetActive(false);
            firstPersonCamera.SetActive(true);
            playerHalo.SetActive(false);
        }
        else
        {
            firstPersonCamera.SetActive(false);
            mainCamera.SetActive(true);
            playerHalo.SetActive(true);
        }

        //weapon firing
        if (primaryFireKey)
        {
            if (primaryWeaponEquiped)
            {
                if (firstWeaponSlot == "SMG")
                {
                    if (Time.time > SMGFireCooldownRefrence)
                    {
                        SMGFireCooldownRefrence = Time.time + SMGFireCooldown;
                        SMGChildAudioSource.Play();
                        randomizedVector = RandomInsideCone(SMGFireSpread) * transform.forward;
                        //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                        gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                        gunSmokeTrailFXTempRef = null;
                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            if (endpointInfo.transform.gameObject.tag == "Enemy")
                            {
                                print("OUCH SMG hurts");
                                NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                theNPC.npcHealth -= SMGFireDamage;
                            }
                        }
                    }
                }
                else
                if (firstWeaponSlot == "SilencedPistol")
                {
                    if (Time.time > pistolFireCooldownRefrence)
                    {
                        pistolFireCooldownRefrence = Time.time + pistolFireCooldown;
                        silencedPistolAudioSource.Play();
                        randomizedVector = RandomInsideCone(pistolFireSpread) * transform.forward;
                        //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                        gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                        //gunSdComponent<Rigidbody>().isKinematic = false;
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                        gunSmokeTrailFXTempRef = null;
                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            if (endpointInfo.transform.gameObject.tag == "Enemy")
                            {
                                print("OUCH pistol hurts");
                                NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                theNPC.npcHealth -= pistolDamage;
                            }
                        }
                    }
                }
                else
                if (firstWeaponSlot == "Carbine")
                {
                    if (Time.time > carbineFireCooldownRefrence)
                    {
                        carbineFireCooldownRefrence = Time.time + carbineFireCooldown;
                        CarbineChildAudioSource.Play();
                        randomizedVector = RandomInsideCone(carbineFireSpread) * transform.forward;
                        //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                        gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                        gunSmokeTrailFXTempRef = null;
                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            if (endpointInfo.transform.gameObject.tag == "Enemy")
                            {
                                print("OUCH carbine hurts");
                                NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                theNPC.npcHealth -= carbineDamage;
                            }
                        }
                    }
                }
                else
                if (firstWeaponSlot == "Shotgun")
                {
                    if (Time.time > shotgunFireCooldownRefrence)
                    {
                        shotgunFireCooldownRefrence = Time.time + shotgunFireCooldown;
                        shotgunChildAudioSource.Play();
                        for (int i = 0; i < shotgunNumPellets; i++)
                        {
                            randomizedVector = RandomInsideCone(shotgunFireSpread) * transform.forward;
                            //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                            gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                            gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                            gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                            gunSmokeTrailFXTempRef = null;
                            if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                            {
                                if (endpointInfo.transform.gameObject.tag == "Enemy")
                                {
                                    print("OUCH shotgun hurts");
                                    NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                    theNPC.npcHealth -= shotgunPerPelletDamage;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (secondWeaponSlot == "SMG")
                {
                    if (Time.time > SMGFireCooldownRefrence)
                    {
                        SMGFireCooldownRefrence = Time.time + SMGFireCooldown;
                        SMGChildAudioSource.Play();
                        randomizedVector = RandomInsideCone(SMGFireSpread) * transform.forward;
                        //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                        gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                        gunSmokeTrailFXTempRef = null;
                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            if (endpointInfo.transform.gameObject.tag == "Enemy")
                            {
                                print("OUCH SMG hurts");
                                NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                theNPC.npcHealth -= SMGFireDamage;
                            }
                        }
                    }
                }
                else
                if (secondWeaponSlot == "SilencedPistol")
                {
                    if (Time.time > pistolFireCooldownRefrence)
                    {
                        pistolFireCooldownRefrence = Time.time + pistolFireCooldown;
                        silencedPistolAudioSource.Play();
                        randomizedVector = RandomInsideCone(pistolFireSpread) * transform.forward;
                        //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                        gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                        gunSmokeTrailFXTempRef = null;
                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            if (endpointInfo.transform.gameObject.tag == "Enemy")
                            {
                                print("OUCH pistol hurts");
                                NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                theNPC.npcHealth -= pistolDamage;
                            }
                        }
                    }
                }
                else
                if (secondWeaponSlot == "Carbine")
                {
                    if (Time.time > carbineFireCooldownRefrence)
                    {
                        carbineFireCooldownRefrence = Time.time + carbineFireCooldown;
                        CarbineChildAudioSource.Play();
                        randomizedVector = RandomInsideCone(carbineFireSpread) * transform.forward;
                        //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                        gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                        gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                        gunSmokeTrailFXTempRef = null;
                        if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                        {
                            if (endpointInfo.transform.gameObject.tag == "Enemy")
                            {
                                print("OUCH carbine hurts");
                                NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                theNPC.npcHealth -= carbineDamage;
                            }
                        }
                    }
                }
                else
                if (secondWeaponSlot == "Shotgun")
                {
                    if (Time.time > shotgunFireCooldownRefrence)
                    {
                        shotgunFireCooldownRefrence = Time.time + shotgunFireCooldown;
                        shotgunChildAudioSource.Play();
                        for (int i = 0; i < shotgunNumPellets; i++)
                        {
                            randomizedVector = RandomInsideCone(shotgunFireSpread) * transform.forward;
                            //Debug.DrawRay(shotPos.transform.position, randomizedVector * 1000, Color.red, 1);
                            gunSmokeTrailFXTempRef = Instantiate(gunSmokeTrailFXPrefab, shotPos.transform.position, gameObject.transform.rotation);
                            gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().isKinematic = false;
                            gunSmokeTrailFXTempRef.GetComponent<Rigidbody>().AddForce(randomizedVector * smokeFxSpeed, ForceMode.Impulse);
                            gunSmokeTrailFXTempRef = null;
                            if (Physics.Raycast(shotPos.transform.position, randomizedVector, out endpointInfo))
                            {
                                if (endpointInfo.transform.gameObject.tag == "Enemy")
                                {
                                    print("OUCH shotgun hurts");
                                    NPCController theNPC = endpointInfo.transform.gameObject.GetComponent<NPCController>();
                                    theNPC.npcHealth -= shotgunPerPelletDamage;
                                }
                            }
                        }
                    }
                }
            }
        }

        //weapon/tool stuff equip below
        if (swapWeaponKey && (Time.time > weaponSwapCooldownTimerRefrence))
        {
            weaponSwapCooldownTimerRefrence = Time.time + weaponSwapCooldown;
            weaponSwapAudioSource.Play();

            if (primaryWeaponEquiped)
            {
                //unequips primary
                if (firstWeaponSlot == "SMG")
                {
                    SMGChild.SetActive(false);
                    SMGChildHolstered.SetActive(true);
                }
                else
                if (firstWeaponSlot == "SilencedPistol")
                {
                    silencedPistol.SetActive(false);
                    silencedPistolHolstered.SetActive(true);
                }
                else
                if (firstWeaponSlot == "Carbine")
                {
                    CarbineChild.SetActive(false);
                    CarbineChildHolstered.SetActive(true);
                }
                else
                if (firstWeaponSlot == "Shotgun")
                {
                    shotgunChild.SetActive(false);
                    shotgunChildHolstered.SetActive(true);
                }

                //equips secondary
                if (secondWeaponSlot == "SMG")
                {
                    SMGChildHolstered.SetActive(false);
                    SMGChild.SetActive(true);
                    currentWeaponAutomatic = true;
                }
                else
                if (secondWeaponSlot == "SilencedPistol")
                {
                    silencedPistolHolstered.SetActive(false);
                    silencedPistol.SetActive(true);
                    currentWeaponAutomatic = false;
                }
                else
                if (secondWeaponSlot == "Carbine")
                {
                    CarbineChildHolstered.SetActive(false);
                    CarbineChild.SetActive(true);
                    currentWeaponAutomatic = false;
                }
                else
                if (secondWeaponSlot == "Shotgun")
                {
                    shotgunChildHolstered.SetActive(false);
                    shotgunChild.SetActive(true);
                    currentWeaponAutomatic = false;
                }
                else
                if (secondWeaponSlot == "")
                {
                    currentWeaponAutomatic = false;
                }
                primaryWeaponEquiped = false;
            }
            else
            {
                //unequips secondary
                if (secondWeaponSlot == "SMG")
                {
                    SMGChild.SetActive(false);
                    SMGChildHolstered.SetActive(true);
                }
                else
                if (secondWeaponSlot == "SilencedPistol")
                {
                    silencedPistol.SetActive(false);
                    silencedPistolHolstered.SetActive(true);
                }
                else
                if (secondWeaponSlot == "Carbine")
                {
                    CarbineChild.SetActive(false);
                    CarbineChildHolstered.SetActive(true);
                }
                else
                if (secondWeaponSlot == "Shotgun")
                {
                    shotgunChild.SetActive(false);
                    shotgunChildHolstered.SetActive(true);
                }

                //equips primary
                if (firstWeaponSlot == "SMG")
                {
                    SMGChildHolstered.SetActive(false);
                    SMGChild.SetActive(true);
                    currentWeaponAutomatic = true;
                }
                else
                if (firstWeaponSlot == "SilencedPistol")
                {
                    silencedPistolHolstered.SetActive(false);
                    silencedPistol.SetActive(true);
                    currentWeaponAutomatic = false;
                }
                else
                if (firstWeaponSlot == "Carbine")
                {
                    CarbineChildHolstered.SetActive(false);
                    CarbineChild.SetActive(true);
                    currentWeaponAutomatic = false;
                }
                else
                if (firstWeaponSlot == "Shotgun")
                {
                    shotgunChildHolstered.SetActive(false);
                    shotgunChild.SetActive(true);
                    currentWeaponAutomatic = false;
                }
                else
                if (firstWeaponSlot == "")
                {
                    currentWeaponAutomatic = false;
                }
                primaryWeaponEquiped = true;
            }
        }

        if (!grapplingHookPrimed && ((stunMineEquip && stunMineUnlocked && (currentToolEquiped != "stunMine")) || (grapplingHookEquip && grapplingHookUnlocked && (currentToolEquiped != "grapplingHook"))
            || (icePickEquip && icePickUnlocked && (currentToolEquiped != "icePick")) || (grenadeEquip && grenadeUnlocked && (currentToolEquiped != "grenade"))))
        {
            toolSwapCooldownTimerRefrence = Time.time + toolSwapCooldown;
            weaponSwapAudioSource.Play();

            //unequips current tool if applicable
            if (currentToolEquiped == "stunMine")
            {
                stunMineChild.SetActive(false);
            }
            else
            if (currentToolEquiped == "grapplingHook")
            {
                grapplingHookChild.SetActive(false);
            }
            else
            if (currentToolEquiped == "icePick")
            {
                icePickChild.SetActive(false);
            }
            else
            if (currentToolEquiped == "grenade")
            {
                grenadeChild.SetActive(false);
            }

            //equips the new selected tool
            if (stunMineEquip)
            {
                stunMineChild.SetActive(true);
                currentToolEquiped = "stunMine";
            }
            else
            if (grapplingHookEquip)
            {
                grapplingHookChild.SetActive(true);
                currentToolEquiped = "grapplingHook";
            }
            else
            if (icePickEquip)
            {
                icePickChild.SetActive(true);
                currentToolEquiped = "icePick";
            }
            else
            if (grenadeEquip)
            {
                grenadeChild.SetActive(true);
                currentToolEquiped = "grenade";
            }
        }
    }

    //randomizes hitscan traces
    Quaternion RandomInsideCone(float radius)
    {
        Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(-radius, radius), Vector3.up);
        Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(-radius, radius), Vector3.right);
        return (randomSpin * randomTilt);
    }

    /*for use when guard object is hit by weapon raytrace
    private void deleteGuard(GameObject guardToDelete)
    {
        print(guardToDelete.name + " Killed");
    }*/
}