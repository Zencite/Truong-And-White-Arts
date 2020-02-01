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

    public List<GameObject> itemList = new List<GameObject>();

    public bool dropsItems;
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

    public bool isFloating;
    private float timer;
    public float timerMax;
    private float floatTimer;
    public float floatTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        isFloating = false;
        timer = 0.0f;
        floatTimer = 0.0f;
        entityCurrentHealth = entityMaxHealth;
        if (isHumanoid)
        {
            thisEntity = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // INSTANTIATE ITEM POSITIONS & TRANSFORM
        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;

        if (entityCurrentHealth <= 0)
        {
            entityCurrentHealth = 0;

            // IF ENTITY DROPS ITEMS
            if (dropsItems)
            {
                foreach(GameObject item in itemList)
                {
                    if(item.gameObject.GetComponent<WeaponStats>() != null)
                    {
                        item.gameObject.GetComponent<WeaponStats>().isInstantiated = true;
                    }
                    Instantiate(item, pos, rotation);
                    item.transform.parent = null;
                }
                dropsItems = false;
            }

            // IF ENTITY WAS A HUMANOID
            if (isHumanoid)
            {
                // SET TO DEAD FACE
                neutralFace.SetActive(false);
                hostileFace.SetActive(false);
                deadFace.SetActive(true);

                // IF KILLED BY BULLETS OR EXPLOSIVES
                if(thisEntity.GetComponent<NavMeshAgent>() != null && !isFloating)
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
                // IF KILLED BY COMBINE BALL
                else if (isFloating)
                {
                    floatTimer += Time.deltaTime;
                    if (floatTimer >= floatTimeMax)
                    {
                        //floatTimer = 0.0f;
                        Destroy(this.gameObject);
                        isFloating = false;
                    }
                }
            }
            // IF ENTITY WAS CRATE
            if (isCrate)
            {
                // NEEDS TIME TO INSTANTIATE ITEM DROPS BEFORE DESTROY
                activeGameObject.SetActive(false);
                timer += Time.deltaTime;

                if (timer >= timerMax)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        // TODO CHECK FOR PHYSIC'S DAMAGE
        /*if (this.transform.parent != null)
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
        }*/

        print(this.transform.name + " has collided with " + col.transform.name);
        if (col.gameObject.GetComponent<Rigidbody>() != null)
        {
            float kineticColDamage = KineticEnergy(col.gameObject.GetComponent<Rigidbody>());
            print("KineticColDamage is " + kineticColDamage);
            if (kineticColDamage > damageThreshold)
            {
                entityCurrentHealth = entityCurrentHealth - (Mathf.RoundToInt(kineticColDamage - damageThreshold));
                print(this.transform.name + " took physics damage and now is at " + entityCurrentHealth + " hp.");
            }
        }

        float kineticDamage = KineticEnergy(this.transform.GetComponent<Rigidbody>());
        print("KineticDamage is " + kineticDamage);
        if (kineticDamage > damageThreshold)
        {
            entityCurrentHealth = entityCurrentHealth - (Mathf.RoundToInt(kineticDamage - damageThreshold));
            print(this.transform.name + " took physics damage and now is at " + entityCurrentHealth + " hp.");
        }
    }
    // CALCULATE PHYSIC DAMAGE
    public static float KineticEnergy(Rigidbody rb)
    {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }
}