using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public GameObject inactiveClaw;
    public GameObject activeClaw;
    public float pullForce;
    public Light gravglow;

    public AudioClip clawsClose;
    public AudioClip clawsOpen;
    public AudioClip gravDrop;
    public AudioClip gravDryFire;
    public AudioClip gravPickUp;
    public AudioClip gravLaunch;
    public AudioClip gravHold;

    public GameObject gg_shotPos;
    public GameObject gravPos;
    private RaycastHit endpointInfo;
    private bool once;
    private float cooldownRef;
    private float cooldown = 1.5f;
    private float dropTimeRef;
    private float dropTime = 0.01f;
    public static string NPC = "NPC";
    public GameObject targetRB;

    void Start()
    {
        inactiveClaw.SetActive(true);
        activeClaw.SetActive(false);
        gg_shotPos = GameObject.Find("ShotPos");
        cooldownRef = cooldown;
        dropTimeRef = dropTime;
        once = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (WeaponScript.activeWeapon != null)
        {
            if (WeaponScript.activeWeapon.GetComponent<WeaponStats>().GetWeaponName().Equals("GravityGun"))
            {
                gravPos = GameObject.Find("GravPos");

                 
                if (Physics.Raycast(gg_shotPos.transform.position, gg_shotPos.transform.TransformDirection(Vector3.forward), out endpointInfo))
                {
                    if (!PlayerSight.isHolding)
                    {
                        if (endpointInfo.collider.gameObject.GetComponent<Rigidbody>() != null && endpointInfo.collider.transform.parent == null)
                        {
                            targetRB = endpointInfo.collider.gameObject;
                        }
                        else
                        {
                            CloseClaws();
                        }

                        if (endpointInfo.collider.transform.parent != null)
                        {
                            if (endpointInfo.collider.transform.parent.gameObject.GetComponent<Rigidbody>() != null)
                            {
                                targetRB = endpointInfo.collider.transform.parent.gameObject;
                            }
                            else
                            {
                                CloseClaws();
                            }
                        }
                    }

                    if (targetRB != null)
                    {
                        if (targetRB.tag != "Player" && targetRB.tag != "NPC" && targetRB.tag != "Head" && targetRB.tag != "Torso")
                        {
                            if (targetRB.GetComponent<Rigidbody>() != null)
                            {
                                activeClaw.SetActive(true);
                                gravglow.range = 10;
                                inactiveClaw.SetActive(false);

                                if (once && !PlayerSight.isHolding)
                                {
                                    if (!SoundController.noiseAudioSource.isPlaying)
                                    {
                                        StartCoroutine(SoundController.noiseSound(clawsOpen, 0f));
                                        once = false;
                                    }
                                }

                                // Drop item in gravgun
                                if (Input.GetKey(KeyCode.Mouse1) && PlayerSight.isHolding)
                                {
                                    if (!SoundController.noiseAudioSource.isPlaying)
                                    {
                                        StartCoroutine(SoundController.noiseSound(gravDrop, 0f));
                                    }

                                    targetRB.transform.parent = null;
                                    targetRB.GetComponent<Rigidbody>().isKinematic = false;
                                    targetRB.GetComponent<Rigidbody>().useGravity = true;
                                    if (Time.time > dropTimeRef)
                                    {
                                        dropTimeRef = Time.time + dropTime;
                                        PlayerSight.isHolding = false;
                                    }
                                    targetRB = null;
                                }

                                // PICK UP OBJECT
                                else if (Input.GetKey(KeyCode.Mouse1) && !PlayerSight.isHolding)
                                {
                                    // PULL ITEM TOWARDS GRAV GUN
                                    if (targetRB.tag != "Player" || targetRB.name != "Player")
                                    {
                                        pullForce = (targetRB.GetComponent<Rigidbody>().mass * 1000);

                                        // PICK UP ITEM IN GRAV GUN
                                        if (Vector3.Distance(gravPos.transform.position, targetRB.transform.position) > 3f)
                                        {
                                            targetRB.GetComponent<Rigidbody>().AddForce(endpointInfo.normal * pullForce * Time.fixedDeltaTime);
                                        }
                                        else
                                        {
                                            if (!SoundController.noiseAudioSource.isPlaying)
                                            {
                                                StartCoroutine(SoundController.noiseSound(gravPickUp, 0f));
                                            }
                                            targetRB.GetComponent<Rigidbody>().isKinematic = true;
                                            targetRB.GetComponent<Rigidbody>().useGravity = false;
                                            Transform gravTrans = gravPos.gameObject.transform;
                                            targetRB.transform.position = gravTrans.position;
                                            targetRB.transform.rotation = gravTrans.rotation;
                                            targetRB.transform.parent = gravTrans;

                                            PlayerSight.isHolding = true;
                                        }
                                    }
                                }

                                // LAUNCH ITEM IN GRAV GUN
                                if (Input.GetKeyDown(KeyCode.Mouse0) && targetRB != null)
                                {
                                    if (PlayerSight.isHolding)
                                    {
                                        targetRB.GetComponent<Rigidbody>().isKinematic = false;
                                        targetRB.GetComponent<Rigidbody>().useGravity = true;
                                        targetRB.transform.parent = null;
                                        endpointInfo.rigidbody.AddForce(-endpointInfo.normal * targetRB.GetComponent<Rigidbody>().mass * 2000);
                                        StartCoroutine(SoundController.gunSounds(gravLaunch, 0f));
                                        WeaponScript.WeaponRecoil(4000f);
                                        targetRB = null;
                                        PlayerSight.isHolding = false;
                                    }

                                    /*if (!PlayerSight.isHolding)
                                    {
                                        //targetRB.GetComponent<Rigidbody>().AddForce(-endpointInfo.normal * targetRB.GetComponent<Rigidbody>().mass * 100);
                                        if (Time.time > cooldownRef)
                                        {
                                            cooldownRef = Time.time + cooldown;
                                            print("I did " + (targetRB.GetComponent<Rigidbody>().mass * 10) + " damage.");
                                            //Vector3.Magnitude(endpointInfo.rigidbody.velocity)
                                            StartCoroutine(SoundController.gunSounds(gravLaunch, 0f));
                                        }
                                    }*/
                                    else
                                    {
                                        StartCoroutine(SoundController.gunSounds(gravDryFire, 0f));
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    targetRB = null;
                }


                if (PlayerSight.isHolding)
                {
                    if (!SoundController.gunCamAudioSource.isPlaying)
                    {
                        StartCoroutine(SoundController.gunSounds(gravHold, 0f));
                    }
                }
            }
            else
            {
                gravPos = null;
            }
        }
    }

    private void CloseClaws()
    {
        activeClaw.SetActive(false);
        gravglow.range = 5;
        inactiveClaw.SetActive(true);
        targetRB = null;

        if (!once && !PlayerSight.isHolding)
        {
            if (!SoundController.noiseAudioSource.isPlaying)
            {
                StartCoroutine(SoundController.noiseSound(clawsClose, 0f));
                once = true;
            }
        }
    }
}