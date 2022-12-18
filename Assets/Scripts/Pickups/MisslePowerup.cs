using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MisslePowerup : PowerUp
{
    //How much more damage the missles do
    public float damageIncrease;
    private float originalDamage;

    //Applies the damage increase
    public override void Apply(PowerUpManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();

        if (tankPawn != null)
        {
            originalDamage = tankPawn.damageDone;
            tankPawn.damageDone *= damageIncrease;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns to original damage after a certain amount of time
    public override void Remove(PowerUpManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();
        
        if (tankPawn != null) 
        {
            tankPawn.damageDone = originalDamage;
        }
    }
}
