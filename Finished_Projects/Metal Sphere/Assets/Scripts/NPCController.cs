using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public GameObject alert;
    public GameObject stun;
    public GameObject normalFace;
    public GameObject deadFace;
    public GameObject sleepFace;
    public GameObject chaseFace;

    public Transform destination;
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public Transform agent;

    public float npcHealth = 100;
    

    public NavMeshAgent navMeshAgent;

    public int spottingRange;
    //public int minDistance;

    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    public bool spotted;
    public bool dead = false;

    Vector3 leftRotation = new Vector3(0.0f, -90.0f, 0.0f);
    Vector3 rightRotation = new Vector3(0.0f, 90.0f, 0.0f);

    private float TargetDistance;
    public float fieldOfViewDegrees = 120f;

    private bool notChase;
    private bool pursuit;
    private bool dying;

    public bool npcBusy;
    public bool woke;
    public bool sleep;

    public float spreadFactor = 0.02f;
    public float npcSpeed;

    public Animator npcAnimation;

    // Start is called before the first frame update
    void awake()
    {
        SetPatrol();
        spotted = false;
        npcAnimation = GetComponent<Animator>();
    }

    void Start()
    {
        alert.gameObject.SetActive(false);

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        npcSpeed = GetComponent<NavMeshAgent>().speed;

        notChase = true;
        spotted = false;
        dying = false;
        dead = false;
        pursuit = false;
        woke = false;
        sleep = false;
        npcBusy = true;

        //Sound/clips
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        clip1 = audioSources[1].clip; //Alert Noise
        clip2 = audioSources[0].clip; //Target Spotted
        clip3 = audioSources[2].clip; //Lost Target heading to last location
        clip4 = audioSources[3].clip; //Target Lost heading back
        clip5 = audioSources[4].clip; //looking around

    }

    void FixedUpdate()
    {
        //This code executes when the npc has no more health
        if(npcHealth <= 0f && !npcBusy)
        {
            npcBusy = true;
            Dying();
        }

        if(woke)
        {
            sleepFace.gameObject.SetActive(false);
            normalFace.gameObject.SetActive(true);
            npcAnimation.Play("Looking Around");
            woke = false;
            
        }

        if (dead == true)
        {
            Dead();
        }

        if (!sleep)
        {
            sleepFace.gameObject.SetActive(false);
            normalFace.gameObject.SetActive(true);
            RaycastHit hit;
            Vector3 rayDirection = destination.transform.position - transform.position;

            if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
            {

                // Detect if player is within the field of view
                if (Physics.Raycast(transform.position, rayDirection, out hit))
                {

                    TargetDistance = hit.distance;

                    if ((TargetDistance <= spottingRange) && (hit.transform.CompareTag("Player")))
                    {
                        StartCoroutine(SetDestination());
                    }
                }
            }

            RaycastHit TheHit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out TheHit))
            {
                if (dying) { }
                else
                {
                    TargetDistance = TheHit.distance;

                    if ((TargetDistance > spottingRange) || (TheHit.transform.tag != "Player"))
                    {
                        SetPatrol();
                    }
                }
            }
        }
    }

    private IEnumerator SetDestination()
    {

        if (!spotted && !npcBusy)
        {
            
            GetComponent<NavMeshAgent>().speed = 8F;
            fieldOfViewDegrees = 270f;
            spottingRange = 25;

            if (pursuit == false)
            {
                locatedPlayer();
                alert.gameObject.SetActive(true);
                source.PlayOneShot(clip1);
                pursuit = true;
            }

            spotted = true;
        }

        Vector3 targetVector = destination.transform.position;
        navMeshAgent.SetDestination(targetVector);

        RaycastHit spot;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out spot) && !npcBusy)
        {
            if (dying) { 
            }
            else if ((TargetDistance < spottingRange) && (spot.transform.tag == "Player"))
            {
                //Target Spotted clip 2
                normalFace.gameObject.SetActive(false);
                chaseFace.gameObject.SetActive(true);
                sleepFace.gameObject.SetActive(false);
                targetVector = destination.transform.position;
                navMeshAgent.SetDestination(targetVector);
            }
            else
            {
                //print("Lost target heading to their last known location..."); clip 3
                normalFace.gameObject.SetActive(true);
                sleepFace.gameObject.SetActive(false);
                chaseFace.gameObject.SetActive(false);
                GetComponent<NavMeshAgent>().speed = 12F;
                spottingRange = 30;
                targetVector = destination.transform.position;
                navMeshAgent.SetDestination(targetVector);

                lookingAround();

                yield return new WaitForSeconds(10);
                alert.gameObject.SetActive(false);

                pursuit = false;
                spottingRange = 8;
                notChase = true;
                //print("Target lost, heading back..."); clip 4
                fieldOfViewDegrees = 270f;
                SetPatrol();
            }
        }

        yield return new WaitForSeconds(2);
        if (!dying && !npcBusy) { pursuit = false; }
        
    }
    //=====================================================
    //This code is called when the npc locates the player.
    //=====================================================
    public void locatedPlayer()
    {
        if (!dying && !npcBusy)
        {
            normalFace.gameObject.SetActive(false);
            sleepFace.gameObject.SetActive(false);
            chaseFace.gameObject.SetActive(true);
            //print("Chasing...");
            spotted = true;
        }
    }

    //=======================================================
    //This code is called when the npc needs to look around.
    //=======================================================
    public void lookingAround()
    {
        //print("Looking Around"); clip 5
        npcAnimation.Play("Looking Around");
    }

    //=======================================================
    //This code is called when the npc is dying
    //=======================================================
    public void Dying()
    {
        Destroy(this.normalFace);
        Destroy(this.sleepFace);
        Destroy(this.chaseFace);
        npcBusy = true;
        deadFace.gameObject.SetActive(true);
        npcAnimation.Play("Dying");
    }


    //=======================================================
    //This code is called when the npc dies
    //=======================================================
    public void Dead()
    {
        this.gameObject.SetActive(false); 
    }


    private void SetPatrol()
    {

        GetComponent<NavMeshAgent>().speed = 3.5F;

        if (notChase == false)
        {
            //code to patrol
            if ((Vector3.Distance(agent.position, patrolPoint1.transform.position) < 0.25) && (Vector3.Distance(agent.position, patrolPoint2.transform.position) > 1))
            {

                navMeshAgent.SetDestination(patrolPoint2.transform.position);

            }
            else if ((Vector3.Distance(agent.position, patrolPoint2.transform.position) < 0.25) && (Vector3.Distance(agent.position, patrolPoint1.transform.position) > 1))
            {

                navMeshAgent.SetDestination(patrolPoint1.transform.position);

            }
        }
        else
        {
            //code to return back to patrolling after chasing
            if (Vector3.Distance(agent.position, patrolPoint1.transform.position) < Vector3.Distance(agent.position, patrolPoint2.transform.position))
            {

                Vector3 patrolVector = patrolPoint1.transform.position;
                navMeshAgent.SetDestination(patrolVector);
                notChase = false;

            }
            else
            {

                Vector3 patrolVector = patrolPoint2.transform.position;
                navMeshAgent.SetDestination(patrolVector);
                notChase = false;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.health -= 10f;
        }

        if(other.gameObject.tag == "killBox")
        {
            npcHealth -= 101f;
        }
        
        if(other.gameObject.tag == "stunBox")
        {
            normalFace.gameObject.SetActive(false);
            deadFace.gameObject.SetActive(false);
            chaseFace.gameObject.SetActive(false);
            sleepFace.gameObject.SetActive(true);
            npcAnimation.Play("stunned");
        }
    }
}

