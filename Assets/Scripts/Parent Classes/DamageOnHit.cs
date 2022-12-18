using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{

    //How much damage
    public float damageDone;
    //Who owns the shot
    public Pawn owner;

    //Gets health component and applies damage equal to the amount that should be done
    public void OnTriggerEnter(Collider other)
    {
        HealthComponent otherHealth = other.gameObject.GetComponent<HealthComponent>();
        
        if(otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone, owner);
        }

        Destroy(gameObject);
    }
}
