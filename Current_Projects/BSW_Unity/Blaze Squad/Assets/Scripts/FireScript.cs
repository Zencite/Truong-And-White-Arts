using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public double fireDamage;
    public double FuelReduction;
    public double fireRange;
    FireSpread parentData;
    SphereCollider col;
    void Start()
    {
        parentData = transform.GetComponentInParent<FireSpread>();
        col = this.GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        if(parentData.fuel > 0)
        {
            parentData.fuel = parentData.fuel - FuelReduction;

            if (parentData.fuel >= 51)
            {
                fireRange += 0.05;
            }
            else
            {
                fireRange -= 0.05;
            }
        }
        

        

        //takes fuel, reduces it by FuelReduction and checks amount
        //if fuel is approaching 51, increase damage and range
        //if fuel is approaching 0, decrease its damage and range
        if(fireRange <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            col.radius = (float)fireRange;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FireSpread>() != null)
        {
            if (other.GetComponent<FireSpread>().burning == false)
            {
                other.GetComponent<FireSpread>().applyDamage(calculateDamage());
            }
        }
    }

    double calculateDamage()
    {
        if (parentData.fuel > 0)
        {
            if (parentData.fuel >= 51)
            {
                fireDamage += 0.05;
            }
            else
            {
                fireDamage -= 0.5;
            }   
        }

        return fireDamage;
    }
}
