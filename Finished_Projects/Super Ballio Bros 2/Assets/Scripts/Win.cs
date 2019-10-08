using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Win: MonoBehaviour
{
    public Text winText;

    void Start()
    {
        winText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            winText.text = "Violent Victory...";
        }
    }
}
