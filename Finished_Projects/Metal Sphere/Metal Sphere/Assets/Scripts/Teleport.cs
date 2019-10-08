using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public string dialogueBlock;

public class Teleport : MonoBehaviour
{
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
        //yield return new WaitForSeconds(2);

        Player.gameObject.active = true;
        playerTransform.transform.position = target.position;
        yield return new WaitForSeconds(1);
    }

}
