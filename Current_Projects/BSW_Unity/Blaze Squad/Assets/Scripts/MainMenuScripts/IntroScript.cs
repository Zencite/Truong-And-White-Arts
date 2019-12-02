using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public AudioClip intro1;
    public AudioClip intro2;
    public AudioClip intro3;

    public AudioSource introAudioSource;

    public GameObject menus;
    public GameObject introCam;
    public Light introLight;

    public RawImage logoPicture;
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
        if(isIntro1 && isIntro2 && !isIntro3 && !introAudioSource.isPlaying)
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

    private void FixedUpdate()
    {
        if (cutsceneSeq.activeSelf)
        {
            timer += Time.deltaTime;
            print("timer is " + timer);
            // CS1
            cutsceneSeq.transform.GetChild(0).gameObject.SetActive(true);

            if (cutsceneSeq.transform.GetChild(0).gameObject.activeSelf && timer > 4.5f)
            {
                // CS2
                cutsceneSeq.transform.GetChild(0).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(1).gameObject.activeSelf && timer > 9.0f)
            {
                // CS3
                cutsceneSeq.transform.GetChild(1).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(2).gameObject.activeSelf && timer > 13.5f)
            {
                // CS4
                cutsceneSeq.transform.GetChild(2).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(3).gameObject.activeSelf && timer > 18.0f)
            {
                // CS5
                cutsceneSeq.transform.GetChild(3).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(4).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(4).gameObject.activeSelf && timer > 20.0f)
            {
                // CS6
                cutsceneSeq.transform.GetChild(4).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(5).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(5).gameObject.activeSelf && timer > 25.5f)
            {
                // CS7
                cutsceneSeq.transform.GetChild(5).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(6).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(6).gameObject.activeSelf && timer > 28.5f)
            {
                // CS8
                cutsceneSeq.transform.GetChild(6).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(7).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(7).gameObject.activeSelf && timer > 30.5f)
            {
                // CS9
                cutsceneSeq.transform.GetChild(7).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(8).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(8).gameObject.activeSelf && timer > 32.5f)
            {
                // CS10
                cutsceneSeq.transform.GetChild(8).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(9).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(9).gameObject.activeSelf && timer > 34.5f)
            {
                // CS11
                cutsceneSeq.transform.GetChild(9).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(10).gameObject.SetActive(true);
            }

            if (cutsceneSeq.transform.GetChild(10).gameObject.activeSelf && timer > 36.5f)
            {
                // CS12
                cutsceneSeq.transform.GetChild(10).gameObject.SetActive(false);
                cutsceneSeq.transform.GetChild(11).gameObject.SetActive(true);
            }
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
