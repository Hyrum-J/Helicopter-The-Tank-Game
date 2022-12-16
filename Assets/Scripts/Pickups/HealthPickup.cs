using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{

    public HealthPowerUp powerUp;

    // Update is called once per frame
    protected override void Update()
    {   
        base.Update();
    }

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
