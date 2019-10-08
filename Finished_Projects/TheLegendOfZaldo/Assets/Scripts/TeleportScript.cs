using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


//public string dialogueBlock;

public class TeleportScript : MonoBehaviour
{
    public Flowchart flowchart;
    public Transform target;
    public Transform playerTransform;
    public Rigidbody playerBody;

    public GameObject Player;
    private bool playerTrigger = false;
    private bool isFading = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTrigger = true;
        }
    }

    void Start()
    {
        playerTrigger = false;
    }

    void Update()
    {
        if (playerTrigger)
        {
            isFading = true;

            StartCoroutine("timeDelay");
            isFading = false;
            playerTrigger = false;
        }
    }

    private IEnumerator timeDelay()
    {
        Player.gameObject.active = false;
        flowchart.ExecuteBlock("Fade Out");
        yield return new WaitForSeconds(2);

        Player.gameObject.active = true;
        playerTransform.transform.position = target.position;
    }

}