using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeliShooter : Shooter
{

    public Transform firepointTransform;
    public Transform firepointTransform2;
    private bool lastShot;

    // Start is called before the first frame update
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        if (lastShot == true)
        {
            GameObject newShell = Instantiate(shellPrefab, firepointTransform2.position, firepointTransform2.rotation);
            afterShotOccurs(newShell, fireForce, damageDone, lifespan);
            lastShot = false;
        }
        else
        {
            GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation);
            afterShotOccurs(newShell, fireForce, damageDone, lifespan);
            lastShot = true;
        }

    }

    private void afterShotOccurs(GameObject newShell, float fireForce, float damageDone, float lifespan)
    {
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        if (doh != null)
        {

            doh.damageDone = damageDone;
            doh.owner = GetComponent<Pawn>();

        }

        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        if (rb != null)
        {

            rb.AddForce(firepointTransform.forward * fireForce);
        }

        Destroy(newShell, lifespan);
    }
}
