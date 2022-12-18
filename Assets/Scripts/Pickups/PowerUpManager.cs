using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    
    //Powerup Sound
    public AudioSource powerUpSound;

    //List of powerups and those that need to be removed
    public List<PowerUp> powerUps;
    public List<PowerUp> removedPowerUpQueue;

    //Before the first frame
    void Start()
    {
        powerUps = new List<PowerUp>();    
        removedPowerUpQueue = new List<PowerUp>();
    }

    //Once per frame
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

    //Adds the powerup effect to the player
    public void Add(PowerUp powerUpToAdd)
    {
        powerUpSound.Play();

        powerUpToAdd.Apply(this);

        powerUps.Add(powerUpToAdd);
    }

    //Removes the powerup from player
    public void Remove(PowerUp powerUpToRemove)
    {
        powerUpToRemove.Remove(this);

        removedPowerUpQueue.Add(powerUpToRemove);

    }

    //Removes time from the powerup timer
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

    //Removes powerups that are ready to be removed
    public void ApplyRemovedPowerUpsQueue()
    {
        foreach (PowerUp powerUp in removedPowerUpQueue)
        {
            powerUps.Remove(powerUp);
        }

        removedPowerUpQueue.Clear();
    }

}
