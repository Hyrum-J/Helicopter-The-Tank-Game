using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    /// <summary>
    /// Value for movement speed.
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Value for rotation speed
    /// </summary>
    public float turnSpeed;

    /// <summary>
    /// Value for altitude changing speed
    /// </summary>
    public float flySpeed;

    public float maxHealth;

    protected float currentHealth;

    protected Mover mover;

    protected Shooter shooter;

    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;

    // Start is called before the first frame update
    protected virtual void Start()
    {

        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        


    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void MoveUp();
    public abstract void MoveDown();
    public abstract void hover();
    public abstract void shoot();

}
