using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public static bool missionGo;

    public GameObject missionGoButton;
    public GameObject quitPromptButton;
    public GameObject creditsButton;

    public GameObject missionGoHighlight;
    public GameObject quitPrompt;
    public GameObject quitPromptHighlight;
    public GameObject creditsHighlight;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        missionGo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelChangeTrigger.changeLevel)
        {
            print("LEVEL CHANGE");
            SceneManager.LoadScene("MapScene");
            LevelChangeTrigger.changeLevel = false;
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
        missionGoButton.SetActive(false);
        quitPromptButton.SetActive(false);
        missionGoHighlight.SetActive(false);
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
        missionGoButton.SetActive(false);
        quitPromptButton.SetActive(false);
        creditsHighlight.SetActive(false);
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

        credits.SetActive(false);
        quitPrompt.SetActive(false);
        quitPromptHighlight.SetActive(false);
    }
}
