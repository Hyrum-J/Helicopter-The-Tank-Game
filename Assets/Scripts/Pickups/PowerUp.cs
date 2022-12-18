using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PowerUp 
{
    //How long it lasts
    public float duration;
    public bool isPermanent;

    //Applies the power
    public abstract void Apply(PowerUpManager target);

    //Removes the power
    public abstract void Remove(PowerUpManager target);
}
