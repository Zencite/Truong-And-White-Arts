using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private bool touch = false;
    private bool once = false;

    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        clip1 = audioSources[0].clip;
        clip2 = audioSources[1].clip;
        winText.text = "";
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
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
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 54)
        {
            winText.text = "Peaceful Victory!";
        }
    }
}