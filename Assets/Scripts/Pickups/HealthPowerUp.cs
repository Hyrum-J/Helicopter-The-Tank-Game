using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerUp : PowerUp
{

    public float healthToAdd;

    public override void Apply(PowerUpManager target)
    {
        HealthComponent targetHealth = target.GetComponent<HealthComponent>();

        if(targetHealth != null)
        {
            targetHealth.Heal(healthToAdd);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Remove(PowerUpManager target)
    {

    }
}
