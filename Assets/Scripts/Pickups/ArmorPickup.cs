using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : Pickup
{
    //Connects to the powerup
    public ArmorPowerup powerUp;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //Destroys actual pickup
    public void OnTriggerEnter(Collider other)
    {
        PowerUpManager PowerUpManager = other.GetComponent<PowerUpManager>();

        if (PowerUpManager != null)
        {
            PowerUpManager.Add(powerUp);

            Destroy(gameObject);
        }
    }
}
