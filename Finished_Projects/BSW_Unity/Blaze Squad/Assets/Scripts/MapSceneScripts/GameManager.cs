using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip levelMusic;
    public AudioClip endMusic;

    public GameObject endscreen;
    public GameObject unitUI;

    public GameObject abortPrompt;

    public Text gScore;
    public Text cScore;
    public Text bScore;
    public Text tScore;
    public Text starScore;

    private float timer;
    public float timerMax;
    private int starCounter;

    private int score;
    private int maxScore;
    private bool once;
    private bool comp;

    string sceneName;
    public Scene activeScene;

    void Start()
    {
        // GETS ACTIVE SCENE NAME FOR RELOADING SCENE
        activeScene = SceneManager.GetActiveScene();
        sceneName = activeScene.name;
        starCounter = 0;
        once = false;
        comp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // PLAYS MUSIC 
        if (!audioSource.isPlaying && !PlantManager.gameDone)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(levelMusic);
        }

        // WHEN PLANTMANAGER HAS 0 FIRES, GAME IS OVER
        if (PlantManager.gameDone)
        {
            if (!once)
            {
                audioSource.Stop();
                once = true;
            }

            if (!audioSource.isPlaying && once)
            {
                audioSource.loop = false;
                audioSource.PlayOneShot(endMusic);
            }

            endscreen.SetActive(true);
            unitUI.SetActive(false);

            gScore.text = PlantManager.greenTreeCount.ToString() + " -> " + (PlantManager.greenTreeCount * 10).ToString();
            cScore.text = PlantManager.choppedTreeCount.ToString() + " -> " + (PlantManager.choppedTreeCount * 5).ToString();
            bScore.text = PlantManager.burntTreeCount.ToString() + " -> -" + (PlantManager.burntTreeCount * 5).ToString();

            score = ((PlantManager.greenTreeCount * 10) + (PlantManager.choppedTreeCount * 5) - (PlantManager.burntTreeCount * 5));
            maxScore = PlantManager.childCount * 10;
            tScore.text = score.ToString() + "/" + maxScore;

            float percent = (((float) score/ (float) maxScore) * 100);

            print("percentage is " + percent);
            if (!comp)
            {
                if (percent < 50)
                {
                    starScore.text = "-";
                }
                else if (percent <= 60)
                {
                    timer += Time.deltaTime;

                    if (timer > timerMax)
                    {
                        starScore.text = "*";
                        timer = 0;
                        comp = true;
                    }
                }
                else if (percent <= 70)
                {
                    timer += Time.deltaTime;

                    if (timer > timerMax)
                    {
                        if (starCounter == 0)
                        {
                            starScore.text = "* ";
                            starCounter = 1;
                            timer = 0;
                        }
                        else
                        {
                            starScore.text += "*";
                            starCounter = 2;
                            timer = 0;
                            comp = true;
                        }
                    }
                }
                else if (percent <= 80)
                {
                    timer += Time.deltaTime;

                    if (timer > timerMax)
                    {
                        if (starCounter == 0)
                        {
                            starScore.text = "* ";
                            starCounter = 1;
                            timer = 0;
                        }
                        else if (starCounter == 1)
                        {
                            starScore.text += "* ";
                            starCounter = 2;
                            timer = 0;
                        }
                        else if (starCounter == 2)
                        {
                            starScore.text += "*";
                            starCounter = 3;
                            timer = 0;
                            comp = true;
                        }
                    }
                }
                else if (percent <= 90)
                {
                    timer += Time.deltaTime;

                    if (timer > timerMax)
                    {
                        if (starCounter == 0)
                        {
                            starScore.text = "*";
                            starCounter = 1;
                            timer = 0;
                        }
                        else if (starCounter == 1)
                        {
                            starScore.text += " *";
                            starCounter = 2;
                            timer = 0;
                        }
                        else if (starCounter == 2)
                        {
                            starScore.text += " * ";
                            starCounter = 3;
                            timer = 0;
                        }
                        else if (starCounter == 3)
                        {
                            starScore.text += "*";
                            starCounter = 4;
                            timer = 0;
                            comp = true;
                        }
                    }
                }
                else
                {
                    timer += Time.deltaTime;

                    if (timer > timerMax)
                    {
                        if (starCounter == 0)
                        {
                            starScore.text = "*";
                            starCounter = 1;
                            timer = 0;
                        }
                        else if (starCounter == 1)
                        {
                            starScore.text += " *";
                            starCounter = 2;
                            timer = 0;
                        }
                        else if (starCounter == 2)
                        {
                            starScore.text += " *";
                            starCounter = 3;
                            timer = 0;
                        }
                        else if (starCounter == 3)
                        {
                            starScore.text += " * ";
                            starCounter = 4;
                            timer = 0;
                        }
                        else if (starCounter == 4)
                        {
                            starScore.text += "*";
                            starCounter = 5;
                            timer = 0;
                            comp = true;
                        }
                    }
                }
            }
        }
    }

    // RELOAD SCENE
    public void PlayAgain()
    {
        
        SceneManager.LoadScene(sceneName);
    }

    // LOAD IN MAIN MENU
    public void MainMenu()
    {
        
        IntroScript.playIntro = true;
        SceneManager.LoadScene("MainMenu");
    }

    // QUITS TO DESKTOP
    public void Quit()
    {
        Application.Quit();
    }

    // BRINGS UP THE MENU
    public void AbortPrompt()
    {
        abortPrompt.SetActive(true);
        unitUI.SetActive(false);
    }

    // RESUME BACK TO GAME
    public void Back()
    {
        unitUI.SetActive(true);
        abortPrompt.SetActive(false);
    }
}
