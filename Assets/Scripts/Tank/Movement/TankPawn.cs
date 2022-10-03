using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
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
        Debug.Log("Not for tank");
    }

    public override void MoveDown()
    {
        Debug.Log("Not for tank");
    }

    public override void hover()
    {
        Debug.Log("Not for tank");
    }

    public override void shoot()
    {
        shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
    }
}
