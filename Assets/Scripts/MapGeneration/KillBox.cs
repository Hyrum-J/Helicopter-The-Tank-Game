using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public HealthComponent targetHealth;
    public Pawn killbox;
    private void OnTriggerEnter(Collider other)
    {
        //Kills Player if they fall out of the world
        targetHealth = other.GetComponent<HealthComponent>();
        targetHealth.TakeDamage(1000, killbox);
    }
}
