using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    /// <summary>
    /// Value for movement speed.
    /// </summary>
    public float moveSpeed;

    public float maxMoveSpeed;
    /// <summary>
    /// Value for Backward movement speed. 
    /// </summary>
    public float backMoveSpeed;
    public float maxBackwardMoveSpeed;

    /// <summary>
    /// Value for rotation speed
    /// </summary>
    public float turnSpeed;

    /// <summary>
    /// Value for altitude changing speed
    /// </summary>
    public float flySpeed;
    public float maxFlySpeed;
    /// <summary>
    /// The backwards flying speed.
    /// </summary>
    public float backFlySpeed;
    public float maxBackwardFlySpeed;
    /// <summary>
    /// The speed the Helicopter goes up
    /// </summary>
    public float upwardSpeed;
    public float maxUpwardSpeed;
    /// <summary>
    /// The speed the Helicopter flies down
    /// </summary>
    public float downwardSpeed;
    /// <summary>
    /// The speed the Helicopter falls when no button pressed
    /// </summary>
    public float fallingSpeed;


    //Is the Helicopter hovering or not
    public bool isHovering;

    //Max health of the pawn
    protected float maxHealth;

    //The current health of the pawn
    protected float currentHealth;

    //Moves the pawn
    protected Mover mover;

    protected TotallyNotWhatItIs noiseMaker;

    //How the pawn shoots
    protected Shooter shooter;
    public float timeOfLastShot;
    protected bool canShoot = false;

    protected float shotDelay;
    protected float timeSinceLastShot;


    //What bullet shoots
    public GameObject shellPrefab;
    //The force that the bullet fires
    public float fireForce;
    //Damage done by the bullet
    public float damageDone;
    //How long the bullet stays rendered
    public float shellLifespan;

    public HealthComponent healthComponenet;

    // Start is called before the first frame update
    protected virtual void Start()
    {

        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();

        noiseMaker = GetComponent<TotallyNotWhatItIs>();
        
    }




    // Update is called once per frame
    protected virtual void Update()
    {
        maxHealth = healthComponenet.maxHealth;
        currentHealth = healthComponenet.currentHealth;
    }

    //Movement Functions
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void MoveUp();
    public abstract void MoveDown();
    public abstract void rollLeft();
    public abstract void rollRight();
    public abstract void hover();
    public abstract void shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
