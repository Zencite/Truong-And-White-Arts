using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


//public string dialogueBlock;

public class DialogueScript : MonoBehaviour
{
    private bool spoken = false;
    public Flowchart flowchart;
    public string dialogue;
    public string dialogue2;
    public string dialogue3;
    public GameObject playerObject;
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (!spoken)
            {
                flowchart.ExecuteBlock(dialogue);
                spoken = true;
            }
            if (spoken)
            {
                flowchart.ExecuteBlock(dialogue2);
                spoken = false;
            }
            if (!spoken)
            {
                flowchart.ExecuteBlock(dialogue3);
                playerObject.GetComponent<PlayerScript>().setShieldActive();
                spoken = true;
            }
        }
    }
}
