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

    public static bool gameDone;

    // Start is called before the first frame update
    void Start()
    {
        gameDone = false;
        childCount = transform.childCount;
        randChild = Random.Range(0, childCount);
        transform.GetChild(randChild).transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(randChild).GetComponent<FireSpread>().burning = true;
        print("Child number " + randChild + " has been selected to burn!");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fireCount = CheckFires();

        fCount.text = fireCount.ToString();

        print("fire count = " + fireCount);

        if(fireCount == 0)
        {
            if (!gameDone)
            {
                CheckTrees();
            }
        }
    }

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
        print("burntTreeCount = " + burntTreeCount + " choppedTreeCount = " + choppedTreeCount + " greenTreeCount = " + greenTreeCount);
        gameDone = true;
    }
}
