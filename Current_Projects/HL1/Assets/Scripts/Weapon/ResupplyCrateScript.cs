using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResupplyCrateScript : MonoBehaviour
{
    public GameObject lid;
    public GameObject ammoPlaceHolder;

    public static bool isResupplying;
    private bool isOpened;
    private float timer;
    public float timerMax;
    private float l;

    private GameObject Player;
    private float minDistance = 2f;

    private float openAngle = -75.0f;

    private float openYpos = 1.25f;
    private float closeYpos = 0.65f;

    private float openZpos = -0.5f;
    private float closeZpos = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ammoPlaceHolder.SetActive(false);
        isResupplying = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void FixedUpdate()
    {
        // OPENING
        if (Vector3.Distance(transform.position, Player.transform.position) < minDistance && !isOpened)
        {
            lid.transform.localRotation = Quaternion.Euler(Mathf.Lerp(0.0f, openAngle, l), 0.0f, 0.0f);
            lid.transform.localPosition = new Vector3 (0.0f, Mathf.Lerp(0.65f, 1.25f, l), Mathf.Lerp(closeZpos, openZpos, l));
            l += 0.95f * Time.deltaTime;
            if (l > 1.0f)
            {
                l = 0.0f;
                lid.transform.localRotation = Quaternion.Euler(openAngle, 0.0f, 0.0f);
                lid.transform.localPosition = new Vector3(0.0f, openYpos, openZpos);
                isOpened = true;
            }

            if (!isResupplying)
            {
                ammoPlaceHolder.SetActive(true);
            }
            else
            {
                ammoPlaceHolder.SetActive(false);
            }
        }
        // CLOSING
        else if(Vector3.Distance(transform.position, Player.transform.position) > minDistance && !isOpened)
        {

            lid.transform.localRotation = Quaternion.Euler(Mathf.Lerp(lid.transform.localRotation.x, 0.0f, l), 0.0f, 0.0f);
            lid.transform.localPosition = new Vector3(0.0f, Mathf.Lerp(lid.transform.localPosition.y, closeYpos, l), Mathf.Lerp(lid.transform.localPosition.z, closeZpos, l));
            l += 0.95f * Time.deltaTime;
            if (l > 1.0f)
            {
                l = 0.0f;
                lid.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                lid.transform.localPosition = new Vector3(0.0f, closeYpos, 0.0f);
                ammoPlaceHolder.SetActive(false);
                isOpened = false;
            }
        }
        
        else if(Vector3.Distance(transform.position, Player.transform.position) > minDistance && isOpened)
        {
            
            lid.transform.localRotation = Quaternion.Euler(Mathf.Lerp(openAngle, 0.0f, l), 0.0f, 0.0f);
            lid.transform.localPosition = new Vector3(0.0f, Mathf.Lerp(openYpos, closeYpos, l), Mathf.Lerp(openZpos, closeZpos,  l));
            l += 0.95f * Time.deltaTime;
            if (l > 1.0f)
            {
                l = 0.0f;
                lid.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                lid.transform.localPosition = new Vector3(0.0f, closeYpos, 0.0f);
                ammoPlaceHolder.SetActive(false);
                isOpened = false;
            }
        }

        if (isResupplying)
        {
            timer += Time.deltaTime;
            if (timer >= timerMax)
            {
                isResupplying = false;
                timer = 0.0f;
            }
        }
    }
}
