using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliPawn : Pawn
{
    /// <summary>
    /// Noting to see here :(
    /// </summary>

    // Start is called before the first frame update
    protected override void Start()
    {
        shotDelay = 0.1f;
        base.Start();
    }

    protected override void Update()
    {
        timeSinceLastShot = Time.time - timeOfLastShot;
        if (timeSinceLastShot > shotDelay)
        {
            canShoot = true;
        }
        if (isHovering == false)
        {
            mover.Fall(-fallingSpeed);
        }
        else
        {
            mover.Hover(-fallingSpeed);
        }
        base.Update();
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, flySpeed, maxFlySpeed, "forward");
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -backFlySpeed, maxBackwardFlySpeed, "backward");
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void MoveUp()
    {
        mover.Move(transform.up, upwardSpeed, maxUpwardSpeed, "up");
    }

    public override void MoveDown()
    {
        mover.Move(transform.up, -downwardSpeed, fallingSpeed, "down");
    }
    public override void rollLeft()
    {
        mover.Roll(turnSpeed);
    }
    public override void rollRight()
    {
        mover.Roll(-turnSpeed);
    }
    public override void hover()
    {
        isHovering = !isHovering;
    }

    public override void shoot()
    {
        if (canShoot == true)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            timeOfLastShot = Time.time;
            canShoot = false;
        }
        
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}

