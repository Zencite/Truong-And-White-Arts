using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    public GameObject levelSelectButton;
    public GameObject creditButton;
    public GameObject quitButton;
    public GameObject backButton;

    public GameObject levels;
    public GameObject credits;

    public void SelectLevel()
    {
        levelSelectButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        creditButton.gameObject.SetActive(false);

        levels.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void Credits()
    {
        levelSelectButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        creditButton.gameObject.SetActive(false);

        credits.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

    }

    public void Back()
    {
        levelSelectButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        creditButton.gameObject.SetActive(true);

        credits.gameObject.SetActive(false);
        levels.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void SecondLevel()
    {
        SceneManager.LoadScene("secondLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
