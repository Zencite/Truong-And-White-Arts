using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    public static bool changeLevel;
    private void OnTriggerEnter(Collider other)
    {
        print("TRIGGERED");
        changeLevel = true;
    }
}
