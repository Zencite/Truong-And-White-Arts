using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endscreen;
    public GameObject unitUI;

    public GameObject abortPrompt;

    public Text gScore;
    public Text cScore;
    public Text bScore;
    public Text tScore;

    private int score;
    private int maxScore;


    // Update is called once per frame
    void Update()
    {
        if (PlantManager.gameDone)
        {
            endscreen.SetActive(true);
            unitUI.SetActive(false);

            gScore.text = PlantManager.greenTreeCount.ToString() + " -> " + (PlantManager.greenTreeCount * 10).ToString();
            cScore.text = PlantManager.choppedTreeCount.ToString() + " -> " + (PlantManager.choppedTreeCount * 5).ToString();
            bScore.text = PlantManager.burntTreeCount.ToString() + " -> -" + (PlantManager.burntTreeCount * 5).ToString();

            score = ((PlantManager.greenTreeCount * 10) + (PlantManager.choppedTreeCount * 5) - (PlantManager.burntTreeCount * 5));
            maxScore = PlantManager.childCount * 10;
            tScore.text = score.ToString() + "/" + maxScore;
        }
    }

    public void PlayAgain()
    {
        // RELOAD SCENE
        SceneManager.LoadScene("MapScene");
    }

    public void MainMenu()
    {
        // LOAD IN MAIN MENU
        IntroScript.playIntro = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AbortPrompt()
    {
        abortPrompt.SetActive(true);
        unitUI.SetActive(false);
    }

    public void Back()
    {
        unitUI.SetActive(true);
        abortPrompt.SetActive(false);
    }
}
