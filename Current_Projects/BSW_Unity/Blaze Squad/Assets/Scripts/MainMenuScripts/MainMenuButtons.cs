using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public static bool missionGo;

    public GameObject menu;
    public GameObject ssds;

    public GameObject missionGoButton;
    public GameObject quitPromptButton;
    public GameObject creditsButton;
    public GameObject controlButton;
    public GameObject xWingButton;

    public GameObject missionGoHighlight;

    public GameObject quitPromptHighlight;
    public GameObject quitPrompt;
    

    public GameObject creditsHighlight;
    public GameObject credits;

    public GameObject controlHighlight;
    public GameObject controls;

    public GameObject xWingHighlight;

    public AudioSource audioSource;
    public AudioClip levelMusic;
    public AudioClip MissionGo;

    public static bool once;

    // Start is called before the first frame update
    void Start()
    {
        once = false;
        missionGo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !LevelChangeTrigger.changeLevel)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(levelMusic);
        }

        if (LevelChangeTrigger.changeLevel)
        {
            print("LEVEL CHANGE");
            int levelNumber = Random.Range(1, 3);
            switch (levelNumber)
            {
                case 1:
                    SceneManager.LoadScene("MapScene");
                    LevelChangeTrigger.changeLevel = false;
                    break;
                case 2:
                    SceneManager.LoadScene("MapScene1");
                    LevelChangeTrigger.changeLevel = false;
                    break;
                default:
                    break;
            }
        }
    }


    // MISSION GO BUTTON
    public void HoverMissionGo()
    {
        missionGoHighlight.SetActive(true);
    }

    public void NotHoverMissionGo()
    {
        missionGoHighlight.SetActive(false);
    }

    public void MissonGo()
    {
        missionGo = true;

        if (!once)
        {
            audioSource.Stop();
            once = true;
        }

        if (!audioSource.isPlaying && once)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(MissionGo);
        }

        missionGoButton.SetActive(false);
        quitPromptButton.SetActive(false);
        creditsButton.SetActive(false);
        controlButton.SetActive(false);
        missionGoHighlight.SetActive(false);
        xWingButton.SetActive(false);
    }

    //========================================

    // QUIT PROMPT BUTTON
    public void HoverQuitPrompt()
    {
        quitPromptHighlight.SetActive(true);
    }

    public void NotHoverQuitPrompt()
    {
        quitPromptHighlight.SetActive(false);
    }

    public void QuitPrompt()
    {
        missionGoButton.SetActive(false);
        quitPromptButton.SetActive(false);
        creditsButton.SetActive(false);
        controlButton.SetActive(false);
        xWingButton.SetActive(false);

        quitPrompt.SetActive(true);
        quitPromptHighlight.SetActive(false);
    }

    //========================================

    // CREDITS BUTTON
    public void HoverCredits()
    {
        creditsHighlight.SetActive(true);
    }

    public void NotHoverCredits()
    {
        creditsHighlight.SetActive(false);
    }

    public void Credits()
    {
        credits.SetActive(true);
        creditsButton.SetActive(false);
        missionGoButton.SetActive(false);
        quitPromptButton.SetActive(false);
        creditsHighlight.SetActive(false);
        controlButton.SetActive(false);
        xWingButton.SetActive(false);
    }

    //========================================

    // CREDITS BUTTON
    public void HoverXWing()
    {
        xWingHighlight.SetActive(true);
    }

    public void NotHoverXWing()
    {
        xWingHighlight.SetActive(false);
    }

    public void EasterEgg()
    {
        xWingHighlight.SetActive(false);
        menu.SetActive(false);
        NickScript.playTheme = true;
        ssds.SetActive(true);
    }

    //========================================

    // CONTROLS BUTTON
    public void HoverControls()
    {
        controlHighlight.SetActive(true);
    }

    public void NotHoverControls()
    {
        controlHighlight.SetActive(false);
    }

    public void Controls()
    {
        controls.SetActive(true);
        creditsButton.SetActive(false);
        missionGoButton.SetActive(false);
        quitPromptButton.SetActive(false);
        xWingButton.SetActive(false);
        controlHighlight.SetActive(false);
    }

    //========================================

    // QUIT AND BACK BUTTONS
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        missionGoButton.SetActive(true);
        quitPromptButton.SetActive(true);
        creditsButton.SetActive(true);
        xWingButton.SetActive(true);
        controlButton.SetActive(true);

        credits.SetActive(false);
        controls.SetActive(false);
        quitPrompt.SetActive(false);
        quitPromptHighlight.SetActive(false);
    }
}
