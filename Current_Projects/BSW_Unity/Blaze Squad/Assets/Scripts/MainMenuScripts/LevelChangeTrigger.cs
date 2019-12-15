using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    public static bool changeLevel;

    // CHANGES THE LEVEL
    private void OnTriggerEnter(Collider other)
    {
        changeLevel = true;
    }
}
