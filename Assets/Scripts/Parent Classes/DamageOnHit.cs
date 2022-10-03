using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{

    public float damageDone;
    public Pawn owner;

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
