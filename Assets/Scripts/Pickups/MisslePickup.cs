using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePickup : Pickup
{
    //Connects to the powerup
    public MisslePowerup powerUp;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //Destroys pickup
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
