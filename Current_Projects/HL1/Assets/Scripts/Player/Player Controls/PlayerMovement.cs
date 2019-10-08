using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject FPCam;
    public Transform playerHeadTransform;
    public SphereCollider sphereCollider;
    public CapsuleCollider capsuleCollider;
    public float playerBaseSpeed;
    public float playerBaseJump;
    public float raycastJumpRange;
    public AudioClip playerRunSFX;

    private bool canJump;
    private bool once = false;
    public static bool isCrouching;
    public static bool inVent;
    private Vector3 angleCompensation;
    private Rigidbody rb;
    private float angle;
    private float playerHalfSpeed;
    private float playerOldSpeed;
    private float playerMaxSpeed;
    static float t = 0.0f;
    private Vector3 standingUp = new Vector3(0f, 0.5f, 0f);
    private Vector3 crounchingDown = new Vector3(0f, 1.5f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerOldSpeed = playerBaseSpeed;
        playerMaxSpeed = (playerBaseSpeed * 2);
        playerHalfSpeed = (playerBaseSpeed / 2);
    }

    void Update()
    {

        //PLAYER STAMINA BAR
        //if you're moving and pressing shift then speed up take from stamina bar
        if (PlayerHealth.hasSuit)
        {
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) && canJump == true && !isCrouching && !inVent)
            {
                if (!once && !(SoundController.playerHeadAudioSource.isPlaying))
                {
                    StartCoroutine(SoundController.playerSound(playerRunSFX, 0f));
                    once = true;
                }
                //PlayerStaminaBar.stamina -= Time.deltaTime * 10;
                playerBaseSpeed = playerMaxSpeed;
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                //print("Vertical = " + vertical);

                Vector3 movement = Camera.main.transform.forward * vertical * playerBaseSpeed * Time.deltaTime;
                Vector3 sidestep = Camera.main.transform.right * horizontal * playerBaseSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + movement + sidestep);

            }
            //if not pressing anything, then normal movement speed
            else if (!isCrouching || !inVent)
            {
                playerBaseSpeed = playerOldSpeed;
                once = false;
            }

            //Stamina Regeneration
            /*if ((PlayerStaminaBar.stamina >= 0) && (playerBaseSpeed == playerOldSpeed))
            {
                PlayerStaminaBar.stamina += Time.deltaTime * 5;
            }

            //if moving, pressing shift and stamina is 0 then you cannot sprint
            if (rb.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift) && (PlayerStaminaBar.stamina <= 0))
            {
                playerBaseSpeed = playerOldSpeed;
            }

            //if stamina regeneration set to 100, never go past
            if (PlayerStaminaBar.stamina >= 100.0f)
            {
                PlayerStaminaBar.stamina = 100.0f;
            }

            //if Stamina bar goes to 0, then set to 0
            if (PlayerStaminaBar.stamina <= 0.0f)
            {
                PlayerStaminaBar.stamina = 0.0f;
            }*/
        }

        //print("Player's stamina " + PlayerStaminaBar.stamina);
        //===================================================================================================

        // CROUCHING
        if (Input.GetKey(KeyCode.LeftControl) && !isCrouching)
        {
            sphereCollider.center = new Vector3 (0f, Mathf.Lerp(0.5f, 1.5f, t), 0f);
            capsuleCollider.center = new Vector3(0f, Mathf.Lerp(1.5f, 2f, t), 0f);
            capsuleCollider.height = Mathf.Lerp(3f, 2f, t);
            t += 1f * Time.deltaTime;
            if (t > 1.0f)
            {
                t = 0.0f;
                sphereCollider.center = new Vector3(0f, 1.5f, 0f);
                capsuleCollider.center = new Vector3(0f, 2f, 0f);
                capsuleCollider.height = 2f;
                playerBaseSpeed = playerHalfSpeed;
                isCrouching = true;
            }    
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && !isCrouching && !inVent)
        {
            float currentSphereCenter = sphereCollider.center.y;
            float currentCapsuleCenter = capsuleCollider.center.y;
            float currentCapsuleHeight = capsuleCollider.height;

            sphereCollider.center = new Vector3(0f, Mathf.Lerp(currentSphereCenter, 0.5f, t), 0f);
            capsuleCollider.center = new Vector3(0f, Mathf.Lerp(currentCapsuleCenter, 1.5f, t), 0f);
            capsuleCollider.height = Mathf.Lerp(currentCapsuleHeight, 3f, t);
            t += 1f * Time.deltaTime;
            if (t > 1.0f)
            {
                t = 0.0f;
                sphereCollider.center = new Vector3(0f, 0.5f, 0f);
                capsuleCollider.center = new Vector3(0f, 1.5f, 0f);
                capsuleCollider.height = 3f;
                playerBaseSpeed = playerOldSpeed;
            }
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && isCrouching && !inVent)
        {
            sphereCollider.center = new Vector3(0f, Mathf.Lerp(1.5f, 0.5f, t), 0f);
            capsuleCollider.center = new Vector3(0f, Mathf.Lerp(2f, 1.5f, t), 0f);
            capsuleCollider.height = Mathf.Lerp(2f, 3f, t);
            t += 1f * Time.deltaTime;
            if (t > 1.0f)
            {
                t = 0.0f;
                sphereCollider.center = new Vector3(0f, 0.5f, 0f);
                capsuleCollider.center = new Vector3(0f, 1.5f, 0f);
                capsuleCollider.height = 3f;
                playerBaseSpeed = playerOldSpeed;
                isCrouching = false;
            }
        }
    }

    void FixedUpdate()
    {
        //Checks if player can jump
        CheckGroundStatus();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = Camera.main.transform.forward * vertical * playerBaseSpeed * Time.deltaTime;
        Vector3 sidestep = Camera.main.transform.right * horizontal * playerBaseSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement + sidestep);

        if (Input.GetKeyDown("space") && canJump == true)
        {
            canJump = false;
            rb.velocity = new Vector3(0, (playerBaseJump * 2) * playerBaseJump * Time.deltaTime, 0);
        }    
    }

    void CheckGroundStatus()
    {

        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * raycastJumpRange, Color.green);

        if (Physics.Raycast(landingRay, out hit, raycastJumpRange))
        {
            float rayDistance = hit.distance;

            
            if (hit.collider == null)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Vent"))
        {
            print("In vents");
            sphereCollider.center = new Vector3(0f, 1.5f, 0f);
            capsuleCollider.center = new Vector3(0f, 2f, 0f);
            capsuleCollider.height = 2f;
            playerBaseSpeed = playerHalfSpeed;
            inVent = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("Vent"))
        {
            print("Out vents");
            inVent = false;
        }
    }
}
