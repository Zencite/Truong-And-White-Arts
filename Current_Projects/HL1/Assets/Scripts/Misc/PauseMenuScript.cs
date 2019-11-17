using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject MainMenu;
    public GameObject QuitMenu;

    public GameObject buttons;
    public GameObject resumeButton;
    public GameObject askMenuButton;
    public GameObject askQuitButton;

    public GameObject backButton;

    public static bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }



    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PauseMenu.SetActive(false);
        buttons.SetActive(true);
        MainMenu.SetActive(false);
        QuitMenu.SetActive(false);
        backButton.SetActive(false);

        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PauseMenu.SetActive(true);

        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Back()
    {
        QuitMenu.SetActive(false);
        MainMenu.SetActive(false);
        backButton.SetActive(false);

        buttons.SetActive(true);
        
    }

    public void AskMenu()
    {
        QuitMenu.SetActive(false);
        buttons.SetActive(false);

        MainMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void AskQuit()
    {
        MainMenu.SetActive(false);
        buttons.SetActive(false);

        QuitMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
