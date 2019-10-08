using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSequence : MonoBehaviour
{
    public GameObject menuButtons;
    public GameObject backdrop;
    public GameObject opening;
    // Start is called before the first frame update
    void Start()
    {
        menuButtons.gameObject.SetActive(false);
        backdrop.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(title());
    }

    private IEnumerator title()
    {
        yield return new WaitForSeconds(3);
        menuButtons.gameObject.SetActive(true);
        backdrop.gameObject.SetActive(true);
        opening.gameObject.SetActive(false);
    }
}
