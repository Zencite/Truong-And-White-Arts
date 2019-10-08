using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityHealth : MonoBehaviour
{
    public int entityMaxHealth;
    public int getEntityMaxHealth() { return entityMaxHealth; }

    public int entityCurrentHealth;
    public int getEntityCurrentHealth() { return entityCurrentHealth; }

    public float damageThreshold;

    public bool dropsItems;
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public bool isCrate;
    public GameObject activeGameObject;

    public bool isHumanoid;
    public SphereCollider humanoidHead;
    public CapsuleCollider humanoidTorso;
    public GameObject neutralFace;
    public GameObject hostileFace;
    public GameObject deadFace;
    public GameObject thisEntity;

    public static RaycastHit entityEndpoint;
    public static float entityWeaponForce;

    public static bool isFloating;
    private float timer;
    public float timerMax;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        entityCurrentHealth = entityMaxHealth;
        if (isHumanoid)
        {
            thisEntity = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;

        if (entityCurrentHealth <= 0)
        {
            entityCurrentHealth = 0;
            if (dropsItems)
            {
                if (drop1 != null)
                {
                    Instantiate(drop1, pos, rotation);
                    drop1.transform.parent = null;
                    drop1 = null;
                }
                if (drop2 != null)
                {
                    Instantiate(drop2, pos, rotation);
                    drop2.transform.parent = null;
                    drop2 = null;
                }
                if (drop3 != null)
                {
                    Instantiate(drop3, pos, rotation);
                    drop3.transform.parent = null;
                    drop3 = null;
                }
            }

            if (isHumanoid)
            {
                neutralFace.SetActive(false);
                hostileFace.SetActive(false);
                deadFace.SetActive(true);

                if(this.GetComponent<NavMeshAgent>() != null && !isFloating)
                {
                    Destroy(this.GetComponent<NavMeshAgent>());
                    Destroy(humanoidHead);
                    Destroy(humanoidTorso);
                    thisEntity.AddComponent<Rigidbody>();
                    thisEntity.GetComponent<Rigidbody>().mass = 20;
                    thisEntity.AddComponent<CapsuleCollider>();
                    thisEntity.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    thisEntity.GetComponent<CapsuleCollider>().height = 3;
                    thisEntity.GetComponent<Rigidbody>().AddForce(-entityEndpoint.normal * entityWeaponForce);
                }
            }
            if (isCrate)
            {
                activeGameObject.SetActive(false);
                timer += Time.deltaTime;

                if (timer >= timerMax)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {

        if (this.transform.parent.gameObject.GetComponent<EntityHealth>() != null)
        {
            if (this.transform.parent.gameObject.GetComponent<EntityHealth>().isHumanoid == true)
            {
                GameObject humanoid = this.transform.parent.gameObject;
                if (this.transform.tag == "Head" || this.transform.tag == "Torso")
                {
                    float objectMagVel = Vector3.Magnitude(col.relativeVelocity);
                    print(objectMagVel + " damage to human.");
                    if (objectMagVel > damageThreshold)
                    {
                        humanoid.GetComponent<EntityHealth>().entityCurrentHealth = humanoid.GetComponent<EntityHealth>().entityCurrentHealth - (Mathf.RoundToInt(objectMagVel - damageThreshold));
                    }
                }
            }
        }
        if (col.gameObject.GetComponent<Rigidbody>() != null)
        {
            float kineticDamage = KineticEnergy(col.gameObject.GetComponent<Rigidbody>());
            if (kineticDamage > damageThreshold)
            {
                entityCurrentHealth = entityCurrentHealth - (Mathf.RoundToInt(kineticDamage - damageThreshold));
            }
        }
        if (this.gameObject.GetComponent<Rigidbody>() != null)
        {
            float kineticDamageSelf = KineticEnergy(this.gameObject.GetComponent<Rigidbody>());
            if (kineticDamageSelf > damageThreshold)
            {
                entityCurrentHealth = entityCurrentHealth - (Mathf.RoundToInt(kineticDamageSelf - damageThreshold));
            }
        }
    }

    public static float KineticEnergy(Rigidbody rb)
    {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }
}