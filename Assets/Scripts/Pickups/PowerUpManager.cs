using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public List<PowerUp> powerUps;
    public List<PowerUp> removedPowerUpQueue;

    void Start()
    {
        powerUps = new List<PowerUp>();    
        removedPowerUpQueue = new List<PowerUp>();
    }

    private void Update()
    {
        if(powerUps.Count > 0)
        {
            DecrementPowerUpTimes();
        }
    }

    private void LateUpdate()
    {
        if(removedPowerUpQueue.Count > 0)
        {
            ApplyRemovedPowerUpsQueue();
        }
    }

    public void Add(PowerUp powerUpToAdd)
    {
        powerUpToAdd.Apply(this);

        powerUps.Add(powerUpToAdd);
    }

    public void Remove(PowerUp powerUpToRemove)
    {
        powerUpToRemove.Remove(this);

        removedPowerUpQueue.Add(powerUpToRemove);

    }

    public void DecrementPowerUpTimes()
    {
        foreach (PowerUp powerUp in powerUps)
        {
            if(!powerUp.isPermanent)
            {
                powerUp.duration -= Time.deltaTime;

                if(powerUp.duration <= 0)
                {
                    Remove(powerUp);
                }
            }
        }
    }

    public void ApplyRemovedPowerUpsQueue()
    {
        foreach (PowerUp powerUp in removedPowerUpQueue)
        {
            powerUps.Remove(powerUp);
        }

        removedPowerUpQueue.Clear();
    }

}
