using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

public class PlayerScript : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;
    private int count;
    private int life;
    private bool touch = false;
    private bool once = false;
    private bool healthy = true;
    private bool gameOver;
    private bool restart;
    private int playerCount = 0;
    public int goalScore;

    public GameObject player;
    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public Text countText;
    public Text lifeText;
    public Text deathText;
    public Text restartText;

    public Flowchart flowchart;
    public string dialogue;


    public GameObject swordObject;
    public GameObject playerObject;

    private bool swordActive = false;
    private bool shieldActive = false;
    private bool bowActive = false;

    public bool isShieldActive()
    {
        return shieldActive;
    }
    public bool isSwordActive()
    {
        return swordActive;
    }
    public bool isBowActive()
    {
        return bowActive;
    }



    public void setShieldActive()
    {
        shieldActive = true;
    }
    public void setSwordActive()
    {
        swordActive = true;
    }
    public void setBowActive()
    {
        bowActive = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        life = 3;
        gameOver = false;
        restart = false;
        restartText.text = "";
        deathText.text = "";
        SetLifeText();
        SetCountText();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        clip1 = audioSources[1].clip; //rupee
        clip2 = audioSources[2].clip; //link hurt
        clip3 = audioSources[3].clip; //life
        clip4 = audioSources[4].clip; //death
        clip5 = audioSources[5].clip; //low life
        clip6 = audioSources[6].clip; //movement
    }
    void Update()
    {
        if (restart)
        {
            flowchart.ExecuteBlock(dialogue);
        }
        
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 up = new Vector3(0.0f, jump, 0.0f);

        float shieldMovement = 1.0f;

        //If a weapon is being held up
        if ((Input.GetKey("right shift")) || (Input.GetKey("left shift")))
        {
            if (bowActive || shieldActive)
            {
                shieldMovement = 0.5f;
            }
        }
  
        else if ((Input.GetKeyDown("space")) || (playerObject.GetComponent<SwordScript>().getSwordInUse()))
        {
            if (swordActive)
            {
                shieldMovement = 0;
            }
        }

        else
        {
            shieldMovement = 1.0f;
        }


        rb.AddForce((movement * speed) * shieldMovement);

        rb.velocity = new Vector3(0, -5, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (life > 1)
            {
                other.gameObject.SetActive(false);
                source.PlayOneShot(clip2);
                life = life - 1;
                SetLifeText();
            }
            else
            {
                restart = true;
                //player.gameObject.SetActive(false);
                SetDeathText();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (life > 1)
            {
                source.PlayOneShot(clip2);
                life = life - 1;
                SetLifeText();
            }
            else
            {
                restart = true;
                //player.gameObject.SetActive(false);
                SetDeathText();
            }
        }
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            source.PlayOneShot(clip1);
            if (other.gameObject.name == "greenLupee")
            {
                count = count + 1;
            }
            if (other.gameObject.name == "blueLupee")
            {
                count = count + 5;
            }
            if (other.gameObject.name == "redLupee")
            {
                count = count + 25;
            }
            SetCountText();
        }
        if (other.gameObject.CompareTag("Death"))
        {
            restart = true;
            //player.gameObject.SetActive(false);
            SetDeathText();
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            if (life != 3)
            {
                StartCoroutine(Heal());
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Lupees: " + count.ToString();
    }

    void SetLifeText()
    {
        lifeText.text = "Health: " + life.ToString();
    }

    void SetDeathText()
    {
        source.PlayOneShot(clip4);
        life = 0;
        SetLifeText();
        deathText.text = "Gudinsmurf Returns...";
        gameOver = true;

        restartText.text = "";
        restart = true;

    }

    IEnumerator Heal()
    {
        source.PlayOneShot(clip3);
        life = life + 1;
        SetLifeText();
        yield return new WaitForSeconds(3);
    }

    public void setPlayerCount()
    {
        playerCount += 1;
        Debug.Log(playerCount);
        if (playerCount >= goalScore)
        {
            restart = true;
        }
    }

}
