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

    private float blinkTimer;
    public float blinkTimeMax;

    private float cooldownRef;
    private float cooldown;

    private float maxThrowForce = 1000.0f;
    private float minThrowForce = 250.0f;
    private float currentThrowForce;

    private float gn_CurrentCookTime = 0.0f;
    private float gn_MaxCookTime = 6.0f;
    private float gn_ThrowCharge = 200.0f;

    private bool hasThrown;
    private bool gn_Active;
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
                print(currentThrowForce);
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

                if (WeaponScript.currentClipAmmo != 0 && !(WeaponScript.currentClipAmmo < 0))
                {
                    // MAX THROW ASSIGNED IF CURRENT EXCEEDS MAX
                    if (currentThrowForce >= maxThrowForce && !hasThrown)
                    {
                        currentThrowForce = maxThrowForce;
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
                    else if (Input.GetKey(KeyCode.Mouse0) && !hasThrown)
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
                    }
                }
                else if (WeaponScript.currentTotalAmmo != 0 && !(WeaponScript.currentTotalAmmo < 0))
                {
                    WeaponScript.currentTotalAmmo--;
                    WeaponScript.currentClipAmmo++;
                    pinned.SetActive(true);
                    unpinned.SetActive(false);
                }
                else if (WeaponScript.currentTotalAmmo <= 0)
                {
                    WeaponScript.currentTotalAmmo = 0;
                    pinned.SetActive(true);
                    unpinned.SetActive(false);
                    gn_InHand.SetActive(false);
                }
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
                blinker.range = 10;
                blinkTimer = 0.0f;
            }
            else
            {
                blinker.range = 5;
            }
        }
    }

    // THROW GRENADE
    private void ThrowGrenade()
    {
        hasThrown = true;
        gn_Active = false;
        Quaternion shotPosRotation = gn_shotPos.transform.rotation;
        Rigidbody projectileShot = Instantiate(gn_Projectile.GetComponent<Rigidbody>(), gn_shotPos.transform.position, shotPosRotation) as Rigidbody;
        projectileShot.transform.LookAt(gn_shotPos.transform.position);
        projectileShot.AddForce(gn_shotPos.transform.forward * currentThrowForce);
        gn_CurrentCookTime = 0.0f;
        WeaponScript.currentClipAmmo--;
    }

    /*// IF THE PLAYER COOKS FOR LONG, EXPLODE ON SELF
    private void GrenadeExplosion()
    {
        Vector3 gn_explosionPosition = gn_Projectile.transform.position;
        Collider[] colliders = Physics.OverlapSphere(gn_explosionPosition, gn_explosionRadius);
        StartCoroutine(SoundController.noiseSound(gn_explosionSFX, 0));
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (!hit.transform.tag.Equals("Grenade"))
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(gn_explosionPower, gn_explosionPosition, gn_explosionRadius, gn_explosionRadius);

                    if (hit.transform.gameObject.GetComponent<EntityHealth>() != null)
                    {
                        GameObject entity = hit.transform.gameObject;
                        EntityExplosionDamage(entity);
                    }

                    if (hit.transform.parent != null)
                    {
                        if (hit.transform.parent.transform.gameObject.GetComponent<EntityHealth>() != null)
                        {
                            GameObject entity = hit.transform.parent.transform.gameObject;
                            EntityExplosionDamage(entity);
                        }
                    }

                    if (hit.tag.Equals("Player"))
                    {
                        GameObject player = hit.transform.gameObject;
                        int damage = Mathf.CeilToInt(CalculateDamage(player.transform.position));
                        int damageThreshold = Mathf.CeilToInt(player.GetComponent<PlayerHealth>().damageThreshold);
                        if (damage > damageThreshold)
                        {
                            PlayerHealth.playerHealth = PlayerHealth.playerHealth - (damage - damageThreshold);
                        }
                    }
                }
            }
        }
        gg_explosionPrefab.Play();
    }

    // CHECK IF ANY ENTITY IS NEARBY SELFEXPLOSION
    private void EntityExplosionDamage(GameObject entity)
    {
        int damage = Mathf.CeilToInt(CalculateDamage(entity.transform.position));
        entity.GetComponent<EntityHealth>().entityCurrentHealth -= damage;
    }

    // CALCULATE DAMAGE BY DISTANCE
    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (gn_explosionRadius - explosionDistance) / gn_explosionRadius;
        float damage = relativeDistance * gn_explosionPower;
        damage = Mathf.Max(0f, damage);
        return damage;
    }*/
}