using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    public GameObject FirstPersonCamera;        //First Person Camera to get the raycast ray
    public GameObject GunCamera;
    float sightDistance;                        //Raycast length
    public static bool isHolding;               //Checks if the player is already holding an object
    public Transform playerHoldingPosition;     //Transform where the object will be held

    public AudioClip denySFX;

    public static bool lookingAtStation;
    public static bool isZoomed;
    public static bool lookingAtVending;
    static float t = 0.0f;
    public int weaponLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        FirstPersonCamera = GameObject.Find("FirstPersonCamera");
        GunCamera = GameObject.Find("GunCamera");
        playerHoldingPosition = GameObject.Find("HoldingPosition").transform;
        sightDistance = 4.5f;
        isHolding = false;
        lookingAtStation = false;
        isZoomed = false;
        weaponLayerMask = GunCamera.GetComponent<Camera>().cullingMask;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = FirstPersonCamera.transform.TransformDirection(Vector3.forward);                  //Uses the FirstPersonCamera as the Ray
        RaycastHit hit;                                                                                     //Raycast hit
        Debug.DrawRay(FirstPersonCamera.transform.position, forward * sightDistance, Color.yellow);           //Debug draws the raycast lines

        if (Physics.Raycast(FirstPersonCamera.transform.position, forward, out hit, sightDistance))
        {
            float rayDistance = hit.distance;
            if (rayDistance <= sightDistance)
            {
                //print("I can detect " + hit.collider.name + " with tag " + hit.collider.tag);
                if (hit.transform.tag != "PlayerHead" || hit.transform.tag != "PlayerHead")
                {
                    //Pick Up physics Object
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        //Physically Carry the Object
                        if (!isHolding)
                        {
                            //If wanting to pick up & move object
                            if (Input.GetKeyDown("e"))
                            {
                                if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                                {
                                    if (hit.transform.gameObject.GetComponent<Rigidbody>().mass < 10)
                                    {
                                        //print("Right Holding");
                                        if (WeaponScript.activeWeapon != null)
                                        {
                                            WeaponScript.activeWeapon.SetActive(false);
                                        }
                                        CarryObject(hit.transform.gameObject);
                                    }
                                    else
                                    {
                                        if (PlayerHealth.hasSuit)
                                        {
                                            StartCoroutine(SoundController.gunSounds(denySFX, 0.0f));
                                        }
                                    }
                                }
                            }
                        }
                        //Physically Drop the Object
                        else if (isHolding && (playerHoldingPosition.childCount > 0))
                        {
                            //if wanting to release object
                            if (Input.GetKeyUp("e"))
                            {
                                //print("Right released");
                                DropObject(hit.transform.gameObject);
                                if (WeaponScript.activeWeapon != null)
                                {
                                    WeaponScript.activeWeapon.SetActive(true);
                                }
                            }

                            if (Input.GetKey(KeyCode.Mouse0))
                            {

                                ThrowObject(hit.transform.gameObject, hit);
                                if (WeaponScript.activeWeapon != null)
                                {
                                    WeaponScript.activeWeapon.SetActive(true);
                                }
                            }
                        }
                    }
                    else if(HealPlayer.atStation || ChargePlayer.atStation)
                    {
                        if (hit.collider.tag.Equals("PlayerStation"))
                        {
                            //print("Player is looking at me");
                            lookingAtStation = true;
                        }
                        else
                        {
                            lookingAtStation = false;
                        }
                    }
                    else if(hit.collider.tag.Equals("Button"))
                    {
                        if(hit.collider.gameObject.GetComponent<VendingMachineButton>() != null)
                        {
                            GameObject vendingButton = hit.collider.gameObject;
                            if(!vendingButton.GetComponent<VendingMachineButton>().pressed)
                            {
                                lookingAtVending = true;
                                GameObject vendingCan = vendingButton.GetComponent<VendingMachineButton>().GetVendingCan();
                                Transform vendingPos = vendingButton.GetComponent<VendingMachineButton>().GetVendingPos();

                                if (Input.GetKey("e") && lookingAtVending)
                                {
                                    vendingButton.GetComponent<VendingMachineButton>().pressed = true;
                                    vendingButton.GetComponent<VendingMachineButton>().green.SetActive(false);
                                    vendingButton.GetComponent<VendingMachineButton>().red.SetActive(true);
                                    Rigidbody can = Instantiate(vendingCan.GetComponent<Rigidbody>(), vendingPos.position, vendingPos.rotation) as Rigidbody;
                                }
                            }
                            else
                            {
                                lookingAtVending = false;
                            }
                        }
                        else
                        {
                            print("other buttons being added in future");
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerHealth.hasSuit)
        {
            if (Input.GetKey("z") && !isZoomed && !WeaponScript.isScoped)
            {
                GunCamera.GetComponent<Camera>().cullingMask = 0;
                FirstPersonCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(70f, 20f, t);
                t += 0.95f * Time.deltaTime;
                if (t > 1.0f)
                {
                    t = 0.0f;
                    FirstPersonCamera.GetComponent<Camera>().fieldOfView = 20f;
                    isZoomed = true;
                }

            }
            else if (!Input.GetKey("z") && !isZoomed && !WeaponScript.isScoped)
            {
                GunCamera.GetComponent<Camera>().cullingMask = weaponLayerMask;
                float currentFOV = FirstPersonCamera.GetComponent<Camera>().fieldOfView;
                FirstPersonCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentFOV, 70f, t);
                t += 0.95f * Time.deltaTime;
                if (t > 1.0f)
                {
                    t = 0.0f;
                    FirstPersonCamera.GetComponent<Camera>().fieldOfView = 70f;
                }
            }
            else if (!Input.GetKey("z") && isZoomed && !WeaponScript.isScoped)
            {
                GunCamera.GetComponent<Camera>().cullingMask = weaponLayerMask;
                FirstPersonCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(20f, 70f, t);
                t += 0.95f * Time.deltaTime;
                if (t > 1.0f)
                {
                    t = 0.0f;
                    FirstPersonCamera.GetComponent<Camera>().fieldOfView = 70f;
                    isZoomed = false;
                }
            }
        }
    }

    //==========================
    //CARRY OBJECT
    //==========================
    private void CarryObject(GameObject hitObject)
    {
        
        if (hitObject.transform.parent != null && !(hitObject.transform.parent.name.Equals(playerHoldingPosition.name)) && !(hitObject.transform.parent.tag.Equals("Untagged")))
        {
            hitObject = hitObject.transform.parent.gameObject;
        }

        hitObject.GetComponent<Rigidbody>().isKinematic = true;
        hitObject.GetComponent<Rigidbody>().useGravity = false;
        hitObject.transform.rotation = playerHoldingPosition.rotation;
        hitObject.transform.position = playerHoldingPosition.position;
        hitObject.transform.parent = GameObject.Find("HoldingPosition").transform;
        hitObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        sightDistance = 3.5f;
        isHolding = true;
    }

    //==========================
    //DROP OBJECT
    //==========================
    private void DropObject(GameObject hitObject)
    {
        if (hitObject.transform.parent != null && !(hitObject.transform.parent.name.Equals(playerHoldingPosition.name)))
        {
            hitObject = hitObject.transform.parent.gameObject;
        }

        hitObject.GetComponent<Rigidbody>().isKinematic = false;
        hitObject.GetComponent<Rigidbody>().useGravity = true;
        hitObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        hitObject.transform.parent = null;
        sightDistance = 7.0f;
        isHolding = false;
    }

    //==========================
    //THROW OBJECT
    //==========================
    private void ThrowObject(GameObject hitObject , RaycastHit hit)
    {
        if (hitObject.transform.parent != null && (!(hitObject.transform.parent.name.Equals(playerHoldingPosition.name)) && !(hitObject.transform.parent.name.Equals("GravPos"))))
        {
            hitObject = hitObject.transform.parent.gameObject;
        }

        //print("Throwing " + hitObject.name);
        hitObject.GetComponent<Rigidbody>().isKinematic = false;
        hitObject.GetComponent<Rigidbody>().useGravity = true;
        hitObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        hitObject.transform.parent = null;
        hit.rigidbody.AddForce(-hit.normal * 600);
        WeaponScript.cooldownRef = Time.time + WeaponScript.cooldown;
        isHolding = false;
    }
}