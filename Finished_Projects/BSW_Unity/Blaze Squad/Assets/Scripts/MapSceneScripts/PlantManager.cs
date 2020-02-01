using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    public Text fCount;

    public static int greenTreeCount;
    public static int burntTreeCount;
    public static int choppedTreeCount;

    private int fireCount;

    public static int childCount;
    private int checkCount;
    private int randChild;
    private int fireNumStart;
    private List<int> activeFires = new List<int>();
    private bool checkFireNum;

    public static bool gameDone;

    // Start is called before the first frame update
    void Start()
    {
        gameDone = false;
        childCount = transform.childCount;

        // CHECKS IF HARDMODE WAS ACTIVATED
        if (MainMenuButtons.hardMode)
        {
            fireNumStart = 5;
        }
        else
        {
            fireNumStart = 1;
        }

        // RANDOMLY SELECTS PLANTS TO SET ON FIRE
        for (int i = 0; i < fireNumStart; i++)
        {
            randChild = Random.Range(0, childCount);
            checkFireNum = true;

            foreach(int x in activeFires)
            {
                // CHILD HAS ALREADY BEEN SELECTED BEFORE
                if(x == randChild)
                {
                    checkFireNum = false;
                    break;
                }
            }

            if (checkFireNum)
            {
                transform.GetChild(randChild).transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(randChild).GetComponent<FireSpread>().burning = true;
                activeFires.Add(randChild);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // CHECKS IF GAME IS OVER VIA # OF FIRES LEFT

        fireCount = CheckFires();

        fCount.text = fireCount.ToString();

        if(fireCount == 0)
        {
            if (!gameDone)
            {
                CheckTrees();
            }
        }
    }

    // COUNTS ACTIVE FIRES
    public int CheckFires()
    {
        int fires = 0;
        foreach (Transform child in transform)
        {
            if (child.transform.GetChild(0).gameObject.activeSelf)
            {
                fires++;
            }
        }
        return fires;
    }

    // COUNTS TREES FOR SCORING IN GAME MANAGER
    public void CheckTrees()
    {
        burntTreeCount = 0;
        choppedTreeCount = 0;
        greenTreeCount = 0;

        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<FireSpread>().isBurnt())
            {
                burntTreeCount++;
            }
            else if (child.gameObject.GetComponent<FireSpread>().isChopped())
            {
                choppedTreeCount++;
            }
            else
            {
                greenTreeCount++;
            }
        }
        gameDone = true;
    }
}
