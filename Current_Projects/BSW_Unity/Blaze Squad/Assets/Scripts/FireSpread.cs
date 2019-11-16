using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{

    //=============================================
    //Object Variables
    //=============================================
    public double health = 100;
    public double waterShield = 100;
    public double flammability = 0.75;
    public double fuel = 100;
    public bool burning = false;
    public bool burned = false;

    void Start() { }

    void FixedUpdate()
    {
        if(fuel <= 0)
        {
            burning = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
            burned = true;
            this.transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    //====================================================================================
    //heatDamage - this script takes a damage value, and applies a series of calculations
    //before subtracting the result from the objects health.
    //====================================================================================
    public void applyDamage(double damage)
    {
        if (!burned)
        {
            //1) Damage is compared to waterShield
            //2) If waterShield is higher, reduce waterShield by damage and set damage to 0
            //3) If damage is higher, then reduce damage by waterShield and set waterShield to 0
            //--------------------------------------------------------------------------------
            if (waterShield > damage)            //1)
            {
                waterShield = waterShield - damage;                 //2)
                damage = 0;
            }
            else
            {
                damage = damage - waterShield;                      //3)
                waterShield = 0;
            }

            //----------------------------------------------------------------------------------------
            //1) Damage is calculated by multiplying the given value with the flammability of the object.
            //2) The product is then subtracted from the health of the object.
            //3) Object is set to burning if it's health has reached 0;
            //-----------------------------------------------------------------------------------------
            health = health - (damage * flammability);      //1) and 2)

            if (health <= 0)                                //3)
            {
                burning = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
