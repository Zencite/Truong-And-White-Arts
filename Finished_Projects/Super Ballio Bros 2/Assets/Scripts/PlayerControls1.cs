using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControls1 : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject target;

    private Rigidbody rb;
    private int count;
    private bool touch = false;
    private bool once = false;

    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;

    public Transform player;
    public GameObject camera;
    public GameObject cameraDoor;
    public float turnSpeed = 4.0f;
    private Vector3 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        offset = new Vector3(player.position.x, player.position.y + 3.0f, player.position.z - 8.0f);
        camera.gameObject.SetActive(true);
        cameraDoor.gameObject.SetActive(false);

        count = 0;
        SetCountText();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        clip1 = audioSources[1].clip;
        clip2 = audioSources[0].clip;
        winText.text = "";
    }

    void FixedUpdate()
    {
        Vector3 cameraPlayerDifference = transform.position - camera.transform.position;
        cameraPlayerDifference.y = 0;
        cameraPlayerDifference.Normalize();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 movement = (cameraPlayerDifference * moveVertical + camera.transform.right * moveHorizontal) * speed;
        Vector3 up = new Vector3(0.0f, jump, 0.0f);

        rb.AddForce(movement * speed);
        if ((Input.GetKeyDown("space")) && (touch == true))
        {
            rb.AddForce(up * 600);
            source.PlayOneShot(clip2);
            touch = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Ground" || col.gameObject.tag == "Ground")
        {
            touch = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            source.PlayOneShot(clip1);
            count = count + 1;
            if (count >= 60)
            {
                if (once == false)
                {
                    StartCoroutine(door());
                }
            }
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }

    IEnumerator door()
    {
        Debug.Log("Going to look at Door");
        camera.gameObject.SetActive(false);
        cameraDoor.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(target.gameObject);
        yield return new WaitForSeconds(1);
        camera.gameObject.SetActive(true);
        cameraDoor.gameObject.SetActive(false);
        once = true;
    }
}