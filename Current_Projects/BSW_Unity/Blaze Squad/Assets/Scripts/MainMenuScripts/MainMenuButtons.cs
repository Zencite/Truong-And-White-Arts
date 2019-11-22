using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public static bool missionGo;
    public GameObject missionGoHighlight;

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
            //change scene
            LevelChangeTrigger.changeLevel = false;
        }
    }

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
        missionGoHighlight.SetActive(false);
    }
}
