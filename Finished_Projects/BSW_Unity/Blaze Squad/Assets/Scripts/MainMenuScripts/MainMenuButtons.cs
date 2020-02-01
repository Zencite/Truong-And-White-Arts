using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public static bool missionGo;
    public static bool hardMode;

    public GameObject menu;
    public GameObject ssds;

    public GameObject missionGoButton;
    public GameObject quitPromptButton;
    public GameObject creditsButton;
    public GameObject controlButton;
    public GameObject hardModeButton;
    public GameObject xWingButton;

    public GameObject missionGoHighlight;

    public GameObject quitPromptHighlight;
    public GameObject quitPrompt;
    

    public GameObject creditsHighlight;
    public GameObject credits;

    public GameObject controlHighlight;
    public GameObject controls;

    public GameObject pup;
    public GameObject dog;

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
        if (hardMode)
        {
            pup.SetActive(false);
            dog.SetActive(true);
        }
        else
        {
            pup.SetActive(true);
            dog.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // PLAYS MUSIC
        if (!audioSource.isPlaying && !LevelChangeTrigger.changeLevel)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(levelMusic);
        }

        // RANDOMLY SELECTS LEVEL
        if (LevelChangeTrigger.changeLevel)
        {
            int levelNumber = Random.Range(1, 5);
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
                case 3:
                    SceneManager.LoadScene("MapScene2");
                    LevelChangeTrigger.changeLevel = false;
                    break;
                case 4:
                    SceneManager.LoadScene("MapScene3");
                    LevelChangeTrigger.changeLevel = false;
                    break;
                default:
                    break;
            }
        }
    }


    // MISSION GO BUTTONS
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
        hardModeButton.SetActive(false);
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
        hardModeButton.SetActive(false);
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
        hardModeButton.SetActive(false);
        xWingButton.SetActive(false);
    }

    //========================================

    // EASTER EGG BUTTON
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
        controlHighlight.SetActive(false);
        hardModeButton.SetActive(false);
        xWingButton.SetActive(false);
        
    }

    //========================================

    // HARDMODE BUTTON
    public void HoverHardMode()
    {
        if (pup.activeSelf)
        {
            pup.transform.GetChild(pup.transform.childCount - 1).gameObject.SetActive(true);
        }
        else if (dog.activeSelf)
        {
            dog.transform.GetChild(dog.transform.childCount - 1).gameObject.SetActive(true);
        }
    }

    public void NotHoverHardMode()
    {
        if (pup.activeSelf)
        {
            pup.transform.GetChild(pup.transform.childCount - 1).gameObject.SetActive(false);
        }
        else if (dog.activeSelf)
        {
            dog.transform.GetChild(dog.transform.childCount - 1).gameObject.SetActive(false);
        }
    }

    public void HardMode()
    {
        if (pup.activeSelf)
        {
            pup.transform.GetChild(pup.transform.childCount - 1).gameObject.SetActive(false);
            pup.SetActive(false);
            dog.SetActive(true);
            hardMode = true;
            print("Hard mode is " + hardMode);
        }
        else if (dog.activeSelf)
        {
            dog.transform.GetChild(dog.transform.childCount - 1).gameObject.SetActive(false);
            dog.SetActive(false);
            pup.SetActive(true);
            hardMode = false;
            print("Hard mode is " + hardMode);
        }
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
        controlButton.SetActive(true);
        hardModeButton.SetActive(true);
        xWingButton.SetActive(true);     

        credits.SetActive(false);
        controls.SetActive(false);
        quitPrompt.SetActive(false);
        quitPromptHighlight.SetActive(false);
    }
}
