using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject logo;
    public Image logoPanelOutline;
    public Text logoText;
    public RawImage logoPicture;
    public bool fadeAway;
    public float fadeTime;

    public AudioSource mainMenuSource;
    public AudioClip valveClip;

    public GameObject sceneList;
    public GameObject sceneChild;
    public int childCount;
    public int sceneNumber;

    public GameObject mainMenu;
    public GameObject levelSelectButton;
    public GameObject creditButton;
    public GameObject askQuitButton;
    public GameObject quitButton;
    public GameObject backButton;

    public GameObject fade;
    public GameObject levels;
    public GameObject credits;
    public GameObject askQuit;

    // Start is called before the first frame update
    void Start()
    {  
        sceneList = GameObject.Find("ScenesList");
        childCount = sceneList.transform.childCount;
        mainMenuSource = this.GetComponent<AudioSource>();
        fadeAway = true;
        sceneNumber = Random.Range(0, childCount);
        sceneChild = sceneList.transform.GetChild(sceneNumber).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeAway && logo.activeSelf)
        {
            if (!mainMenuSource.isPlaying)
            {
                StartCoroutine(MenuSounds(valveClip, 0));
            }
            StartCoroutine(FadeLogo(logoPanelOutline, logoText, logoPicture, fadeAway));
            StartCoroutine(Wait());
        }
        else if (!fadeAway && logo.activeSelf)
        {
            StartCoroutine(FadeLogo(logoPanelOutline, logoText, logoPicture, fadeAway));
            StartCoroutine(Wait());
        }
        StartCoroutine(Title());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        fadeAway = false;
    }

    // ACTIVATES MAIN MENU AFTER LOGO
    private IEnumerator Title()
    {
        yield return new WaitForSeconds(10);
        logo.SetActive(false);
        mainMenu.SetActive(true);
        sceneChild.SetActive(true);
    }

    // PLAYS SOUNDS
    private IEnumerator MenuSounds(AudioClip SFX, float delay)
    {
        mainMenuSource.PlayOneShot(SFX);
        yield return new WaitForSeconds(delay * 5);
    }

    //FADES LOGO
    IEnumerator FadeLogo(Image LPO, Text LT, RawImage LP, bool fadeAway)
    {
        
        // fade from transparent to opaque 
        if (fadeAway)
        {
            // loop over 4 second
            for (float i = 0.0f; i <= 1.0f; i += fadeTime * 0.02f)
            {
                LPO.color = new Color(0f, 0f, 0f, i);
                LT.color = new Color(1f, 1f, 1f, i);
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
                LPO.color = new Color(0f, 0f, 0f, i);
                LT.color = new Color(1f, 1f, 1f, i);
                LP.color = new Color(1f, 1f, 1f, i);
                yield return null;
            }
        }
    }

    // BUTTONS
    public void SelectLevel()
    {
        levelSelectButton.SetActive(false);
        askQuitButton.SetActive(false);
        creditButton.SetActive(false);

        fade.SetActive(true);
        levels.SetActive(true);
        backButton.SetActive(true);
    }

    public void Credits()
    {
        levelSelectButton.SetActive(false);
        askQuitButton.SetActive(false);
        creditButton.SetActive(false);

        fade.SetActive(true);
        credits.SetActive(true);
        backButton.SetActive(true);

    }

    public void Back()
    {
        levelSelectButton.SetActive(true);
        askQuitButton.SetActive(true);
        creditButton.SetActive(true);

        fade.SetActive(false);
        askQuit.SetActive(false);
        credits.SetActive(false);
        levels.SetActive(false);
        backButton.SetActive(false);
    }

    public void PlayTestScene()
    {
        SceneManager.LoadScene("PlayTestScene");
    }

    public void AskQuit()
    {
        levelSelectButton.SetActive(false);
        askQuitButton.SetActive(false);
        creditButton.SetActive(false);

        fade.SetActive(true);
        askQuit.SetActive(true);
        backButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
