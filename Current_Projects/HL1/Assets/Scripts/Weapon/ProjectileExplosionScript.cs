using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileExplosionScript : MonoBehaviour
{
    public float explosionRadius;
    public float explosionPower;
    public AudioClip explosionSFX;
    public AudioClip combineBallLaunch;
    public AudioClip combineBallBounce;
    public AudioClip grenadeTick;
    public AudioSource projectileSource;

    public GameObject entity;
    public bool isCombineBall;
    public bool isGrenade;
    public bool isRocket;
    public bool isBarrel;
    private bool once;
    private float currentTime;

    public Light gn_blinker;
    private float gn_blinkTimer;
    public float gn_blinkTimeMax;

    private float timer;
    public float timerMax;
    public ParticleSystem explosionPrefab;

    void Start()
    {
        timer = 0.0f;
        // IF COMBINE BALL PLAY LAUNCH SFX INSIDE EXPLOSIONSFX

        //projectileSource = this.GetComponent<AudioSource>();

        if (isCombineBall)
        {
            StartCoroutine(SoundController.gunSounds(explosionSFX, 0));
        }
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<EntityHealth>() != null && isBarrel)
        {
            int maxHealth = this.gameObject.GetComponent<EntityHealth>().getEntityMaxHealth();
            int health = this.gameObject.GetComponent<EntityHealth>().getEntityCurrentHealth();

            if(health < (maxHealth/2))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                timer += Time.deltaTime;
                if (timer >= timerMax)
                {
                    this.gameObject.GetComponent<EntityHealth>().entityCurrentHealth--;
                    timer = 0.0f;
                }

                if(health <= 0)
                {
                    Explosion();
                    Destroy(this.gameObject);
                }
            }
        }


        // IF GRENADE IS HELD ON FOR TOO LONG
        if (GrenadeScript.gn_explodeDrop)
        {
            Explosion();
            Destroy(this.gameObject);
            GrenadeScript.gn_explodeDrop = false;
        }

        // BLOWS UP PROJECTILE AFTER X SECONDS
        if (!isGrenade && !isBarrel)
        {
            timer += Time.deltaTime;

            if (timer >= timerMax)
            {
                Explosion();
                Destroy(this.gameObject);
            }
        }
        // GRENADE  TICK
        else if (isGrenade)
        {
            if (!once)
            {
                currentTime = GrenadeScript._CurrentCookTime;
                once = true;
            }
            currentTime += Time.deltaTime;

            if (currentTime >= GrenadeScript.gn_MaxCookTime)
            {
                Explosion();
                Destroy(this.gameObject);
            }
        }

        // HAS ROCKET MOVE TOWARDS POSITION
        if (isRocket)
        {
            if (WeaponScript.targetInRange)
            {
                transform.LookAt(WeaponScript.laserPoint);
                float step = 10.0f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, WeaponScript.laserPoint, step);
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.gameObject.tag.Equals("PlayerGrenade"))
        {
            gn_blinkTimer += Time.deltaTime;

            // LIGHT BLINKS
            if (gn_blinkTimer >= gn_blinkTimeMax)
            {
                gn_blinker.intensity = 10;
                StartCoroutine(ProjectileSound(grenadeTick, 0));
                gn_blinkTimer = 0.0f;
            }
            else
            {
                gn_blinker.intensity = 5;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (isCombineBall)
        {
            StartCoroutine(ProjectileSound(combineBallBounce, 0));

            Rigidbody rb = col.transform.GetComponent<Rigidbody>();

            if (col.transform.tag != "CombineBall")
            {
                if (rb != null)
                {
                    if (col.transform.gameObject.GetComponent<EntityHealth>() != null)
                    {
                        entity = col.transform.gameObject;
                        if (entity.GetComponent<EntityHealth>().isHumanoid)
                        {
                            StartCoroutine(Floating(entity));   //float up
                        }
                    }
                    if (col.transform.parent != null)
                    {
                        if (col.transform.parent.gameObject.GetComponent<EntityHealth>() != null)
                        {
                            entity = col.transform.parent.gameObject;
                            if (entity.GetComponent<EntityHealth>().isHumanoid)
                            {
                                StartCoroutine(Floating(entity));   //float up
                            }
                        }
                    }
                }
            }
        }
    }
    // IF SMG GRENADE OR RPG ROCKET
    private void OnTriggerEnter()
    {
        if (!isGrenade && !isCombineBall && !isBarrel)
        {
            Explosion();
            Destroy(this.gameObject);
        }
    }

    // PLAY PROJECTILE SFX
    public IEnumerator ProjectileSound(AudioClip SFX, float delay)
    {
        projectileSource.PlayOneShot(SFX);
        yield return new WaitForSeconds(delay * 5);
    }

    private void Explosion()
    {
        StartCoroutine(ProjectileSound(explosionSFX, 0));
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (!hit.transform.tag.Equals("SMGGrenade") && !hit.transform.tag.Equals("CombineBall") && !hit.transform.tag.Equals("Grenade"))
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionPower, explosionPosition, explosionRadius, explosionRadius);

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
                            if (PlayerHealth.playerSuit != 0)
                            {
                                if (damage - damageThreshold <= PlayerHealth.playerSuit)
                                {
                                    PlayerHealth.playerSuit = PlayerHealth.playerSuit - (damage - damageThreshold);
                                }
                                else if (damage - damageThreshold > PlayerHealth.playerSuit)
                                {
                                    int remainder = ((damage - damageThreshold) - PlayerHealth.playerSuit);
                                    PlayerHealth.playerSuit = 0;
                                    PlayerHealth.playerHealth = PlayerHealth.playerHealth - remainder;
                                }
                            }
                            else if (PlayerHealth.playerHealth != 0)
                            {
                                if (damage - damageThreshold <= PlayerHealth.playerHealth)
                                {
                                    PlayerHealth.playerHealth = PlayerHealth.playerHealth - (damage - damageThreshold);
                                }
                                else if (damage - damageThreshold > PlayerHealth.playerHealth)
                                {
                                    print("Death");
                                }
                            }
                        }
                    }
                }
            }
        }
        explosionPrefab.transform.parent = null;
        explosionPrefab.Play();
        if (!isCombineBall)
        {
            Destroy(explosionPrefab.gameObject, explosionPrefab.main.duration);
        }
        WeaponScript.rocketFired = false;
    }

    private void EntityExplosionDamage(GameObject entity)
    {
        int damage = Mathf.CeilToInt(CalculateDamage(entity.transform.position));
        entity.GetComponent<EntityHealth>().entityCurrentHealth -= damage;
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * explosionPower;
        damage = Mathf.Max(0f, damage);
        return damage;
    }

    // COMBINE BALL MAKES HUMANOID TARGETS FLOAT UP
    // TODO MAKE HUMANOID TARGETS DISSOLVE
    IEnumerator Floating(GameObject entity)
    {
        if (entity.GetComponent<Rigidbody>() == null)
        {
            entity.GetComponent<EntityHealth>().isFloating = true;
            Destroy(entity.GetComponent<NavMeshAgent>());
            Destroy(entity.GetComponent<EntityHealth>().humanoidHead);
            Destroy(entity.GetComponent<EntityHealth>().humanoidTorso);
            entity.AddComponent<Rigidbody>();
            entity.GetComponent<Rigidbody>().mass = 1;
            entity.GetComponent<Rigidbody>().useGravity = false;
            entity.AddComponent<CapsuleCollider>();
            entity.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
            entity.GetComponent<CapsuleCollider>().height = 3;
        }
        float entityMass = entity.GetComponent<Rigidbody>().mass;
        entity.GetComponent<Rigidbody>().AddForce(0, entityMass * 20, 0);
        entity.GetComponent<EntityHealth>().entityCurrentHealth -= 1000;
        yield return null;
    }
}