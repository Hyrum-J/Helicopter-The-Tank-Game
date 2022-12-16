using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmorPowerup : PowerUp
{
    public float armorFactor;

    public override void Apply(PowerUpManager target)
    {
        HealthComponent targetHealth = target.GetComponent<HealthComponent>();

        if (targetHealth != null)
        {
            targetHealth.maxHealth = targetHealth.maxHealth * armorFactor;
            targetHealth.currentHealth = targetHealth.currentHealth * armorFactor;
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
