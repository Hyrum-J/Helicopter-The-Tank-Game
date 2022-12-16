using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MisslePowerup : PowerUp
{
    public float damageIncrease;
    private float originalDamage;

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

    public override void Remove(PowerUpManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();
        
        if (tankPawn != null) 
        {
            tankPawn.damageDone = originalDamage;
        }
    }
}
