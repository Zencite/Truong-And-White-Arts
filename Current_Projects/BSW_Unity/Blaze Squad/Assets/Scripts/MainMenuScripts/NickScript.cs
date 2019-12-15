using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickScript : MonoBehaviour
{
    // EASTER EGG

    public GameObject fighters;
    public GameObject menu;
    public GameObject ssds;
    public Transform nickStar;

    public Transform fighterStart;
    public static bool playTheme;
    public AudioSource nickSource;
    public AudioClip nickTheme;
    // Start is called before the first frame update
    void Start()
    {
        fighters.transform.position = fighterStart.position;
        nickStar.localRotation = new Quaternion(0.0f, 70.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playTheme && !nickSource.isPlaying)
        {
            nickSource.PlayOneShot(nickTheme);
        }
    }

    // RETURNS BACK TO MENU
    private void OnTriggerEnter(Collider other)
    {
        playTheme = false;
        fighters.transform.localScale = new Vector3(25.0f, 25.0f, 25.0f);
        fighters.transform.position = fighterStart.position;
        nickStar.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        menu.SetActive(true);
        ssds.SetActive(false);
    }
}
