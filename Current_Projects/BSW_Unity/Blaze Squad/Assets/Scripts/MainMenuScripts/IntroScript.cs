using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public static bool playIntro;

    public AudioClip intro1;
    public AudioClip intro2;
    public AudioClip intro3;

    public AudioSource introAudioSource;

    public GameObject menus;
    public GameObject introCam;
    public Light introLight;

    public RawImage logoPicture;
    public RawImage introPicture;
    public bool fadeAway;
    public float fadeTime;

    public GameObject LogoSeq;
    public GameObject cutsceneSeq;

    private bool isIntro1;
    private bool isIntro2;
    private bool isIntro3;

    private bool logoDone;
    private int seconds;
    
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        menus.SetActive(false);
        cutsceneSeq.SetActive(false);
        LogoSeq.SetActive(true);
        introCam.SetActive(true);
        fadeAway = true;
        seconds = 3;
        introLight.color = new Color(1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("A key or mouse click has been detected");
            menus.SetActive(true);
            transform.gameObject.SetActive(false);
        }

        if (!playIntro)
        {
            if (fadeAway)
            {
                StartCoroutine(FadeLogo(logoPicture, fadeAway));
                StartCoroutine(Wait());
            }
            else if (!fadeAway)
            {
                StartCoroutine(FadeLogo(logoPicture, fadeAway));
                StartCoroutine(Wait());
                introLight.color = new Color(1.0f, 1.0f, 1.0f);
                introLight.intensity = 0.5f;
            }

            if (!isIntro1 && !introAudioSource.isPlaying)
            {

                introAudioSource.PlayOneShot(intro1);
                if (introAudioSource.isPlaying)
                {
                    isIntro1 = true;

                }
            }
            if (isIntro1 && !isIntro2 && !introAudioSource.isPlaying)
            {
                introAudioSource.PlayOneShot(intro2);
                if (introAudioSource.isPlaying)
                {
                    isIntro2 = true;
                }
            }
            if (isIntro1 && isIntro2 && !isIntro3 && !introAudioSource.isPlaying)
            {
                introAudioSource.PlayOneShot(intro3);
                if (introAudioSource.isPlaying)
                {
                    isIntro3 = true;
                }
            }

            if (logoDone)
            {
                StartCoroutine(Wait());
                LogoSeq.SetActive(false);
                cutsceneSeq.SetActive(true);
            }
        }
        else
        {
            menus.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if(!playIntro)
        {
            if (cutsceneSeq.activeSelf)
            {
                timer += Time.deltaTime;
                print("timer is " + timer);
                // CS1
                if (cutsceneSeq.activeSelf && timer > 0.0f)
                {
                    cutsceneSeq.transform.GetChild(0).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(0).gameObject.activeSelf && timer > 4.5f)
                {
                    // CS2
                    cutsceneSeq.transform.GetChild(0).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(1).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(1).gameObject.activeSelf && timer > 7.5f)
                {
                    // CS3
                    cutsceneSeq.transform.GetChild(1).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(2).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(2).gameObject.activeSelf && timer > 10.5f)
                {
                    // CS4
                    cutsceneSeq.transform.GetChild(2).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(3).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(3).gameObject.activeSelf && timer > 15.0f)
                {
                    // CS5
                    cutsceneSeq.transform.GetChild(3).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(4).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(4).gameObject.activeSelf && timer > 18.0f)
                {
                    // CS6
                    cutsceneSeq.transform.GetChild(4).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(5).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(5).gameObject.activeSelf && timer > 25.25f)
                {
                    // CS7
                    cutsceneSeq.transform.GetChild(5).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(6).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(6).gameObject.activeSelf && timer > 26.75f)
                {
                    // CS8
                    cutsceneSeq.transform.GetChild(6).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(7).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(7).gameObject.activeSelf && timer > 29.5f)
                {
                    // CS9
                    cutsceneSeq.transform.GetChild(7).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(8).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(8).gameObject.activeSelf && timer > 33.5f)
                {
                    // CS10
                    cutsceneSeq.transform.GetChild(8).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(9).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(9).gameObject.activeSelf && timer > 35.5f)
                {
                    // CS11
                    cutsceneSeq.transform.GetChild(9).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(10).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(10).gameObject.activeSelf && timer > 37.5f)
                {
                    // CS12
                    cutsceneSeq.transform.GetChild(10).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(11).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(11).gameObject.activeSelf && timer > 39f)
                {
                    // CS13
                    cutsceneSeq.transform.GetChild(11).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(12).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(12).gameObject.activeSelf && timer > 40f)
                {
                    // CS14
                    cutsceneSeq.transform.GetChild(12).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(13).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(13).gameObject.activeSelf && timer > 41f)
                {
                    // CS15
                    cutsceneSeq.transform.GetChild(13).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(14).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(14).gameObject.activeSelf && timer > 41.5f)
                {
                    // CS16
                    cutsceneSeq.transform.GetChild(14).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(15).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(15).gameObject.activeSelf && timer > 42f)
                {
                    // CS17
                    cutsceneSeq.transform.GetChild(15).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(16).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(16).gameObject.activeSelf && timer > 43f)
                {
                    // CS18
                    cutsceneSeq.transform.GetChild(16).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(17).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(17).gameObject.activeSelf && timer > 44f)
                {
                    // CS19
                    cutsceneSeq.transform.GetChild(17).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(18).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(18).gameObject.activeSelf && timer > 45f)
                {
                    // CS20
                    cutsceneSeq.transform.GetChild(18).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(19).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(19).gameObject.activeSelf && timer > 46f)
                {
                    // CS21
                    cutsceneSeq.transform.GetChild(19).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(20).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(20).gameObject.activeSelf && timer > 47f)
                {
                    // CS22
                    cutsceneSeq.transform.GetChild(20).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(21).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(21).gameObject.activeSelf && timer > 48.5f)
                {
                    // CS23
                    cutsceneSeq.transform.GetChild(21).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(22).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(22).gameObject.activeSelf && timer > 49.5f)
                {
                    // CS24
                    cutsceneSeq.transform.GetChild(22).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(23).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(23).gameObject.activeSelf && timer > 51.5f)
                {
                    // CS25
                    cutsceneSeq.transform.GetChild(23).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(24).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(24).gameObject.activeSelf && timer > 53f)
                {
                    // CS26
                    cutsceneSeq.transform.GetChild(24).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(25).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(25).gameObject.activeSelf && timer > 54f)
                {
                    // CS27
                    cutsceneSeq.transform.GetChild(25).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(26).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(26).gameObject.activeSelf && timer > 55f)
                {
                    // CS28
                    cutsceneSeq.transform.GetChild(26).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(27).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(27).gameObject.activeSelf && timer > 56f)
                {
                    // CS29
                    cutsceneSeq.transform.GetChild(27).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(28).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(28).gameObject.activeSelf && timer > 57f)
                {
                    // CS30
                    cutsceneSeq.transform.GetChild(28).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(29).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(29).gameObject.activeSelf && timer > 58f)
                {
                    // CS31
                    cutsceneSeq.transform.GetChild(29).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(30).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(30).gameObject.activeSelf && timer > 59f)
                {
                    // CS32
                    cutsceneSeq.transform.GetChild(30).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(31).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(31).gameObject.activeSelf && timer > 60f)
                {
                    // CS33
                    cutsceneSeq.transform.GetChild(31).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(32).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(32).gameObject.activeSelf && timer > 61f)
                {
                    // CS34
                    cutsceneSeq.transform.GetChild(32).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(33).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(33).gameObject.activeSelf && timer > 62f)
                {
                    // CS35
                    cutsceneSeq.transform.GetChild(33).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(34).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(34).gameObject.activeSelf && timer > 63f)
                {
                    // CS36
                    cutsceneSeq.transform.GetChild(34).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(35).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(35).gameObject.activeSelf && timer > 64f)
                {
                    // CS37
                    cutsceneSeq.transform.GetChild(35).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(36).gameObject.SetActive(true);
                }

                if (cutsceneSeq.transform.GetChild(36).gameObject.activeSelf && timer > 65f)
                {
                    // CS38
                    cutsceneSeq.transform.GetChild(36).gameObject.SetActive(false);
                    cutsceneSeq.transform.GetChild(37).gameObject.SetActive(true);
                }

                if (timer > 72f)
                {
                    menus.SetActive(true);
                    transform.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            menus.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(seconds);
        fadeAway = false;
    }

    //FADES LOGO
    IEnumerator FadeLogo(RawImage LP, bool fadeAway)
    {

        // fade from transparent to opaque 
        if (fadeAway)
        {
            // loop over 4 second
            for (float i = 0.0f; i <= 1.0f; i += fadeTime * 0.02f)
            {
                LP.color = new Color(1f, 1f, 1f, i);
                yield return null;
            }
        }
        // fade from opaque to transparent
        else
        {
            // loop over 4 second backwards
            for (float i = 1.0f; i >= 0.0f; i -= fadeTime * 0.0125f)
            {
                LP.color = new Color(1f, 1f, 1f, i);
                yield return null;
            }
            logoDone = true;
        }
    }
}
