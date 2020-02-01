using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineButton : MonoBehaviour
{
    public Transform vendingPos;
    public GameObject vendingCan;
    public GameObject green;
    public GameObject red;
    public bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        green = transform.GetChild(1).gameObject;
        red = transform.GetChild(2).gameObject;
        red.SetActive(false);
    }

    // RETURNS VENDINGCAN PREFAB
    public GameObject GetVendingCan()
    {
        return vendingCan;
    }

    // RETURNS VENDING POSITION
    public Transform GetVendingPos()
    {
        return vendingPos;
    }
}
