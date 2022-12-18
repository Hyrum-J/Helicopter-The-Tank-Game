using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AiController;
using static UnityEngine.GraphicsBuffer;

public class Wimp : AiController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    //Wimp stays away from you
    public override void MakeDesicions()
    {
        TargetNearestTank();

        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if (IsDistanceLessThan(target, fleeDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Flee);
                }
                if (Time.time - lastStateChangeTime > maxIdleTime)
                {
                    ChangeState(AIState.Patrol);
                }
                if (CanHearEnemy(target) && CanSeeEnemy())
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                if (!IsDistanceLessThan(target, fleeDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Idle);
                }
                if (IsDistanceLessThan(target, attackDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Patrol:
                Patrol();
                if (IsDistanceLessThan(target, fleeDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Flee);
                }
                if (CanHearEnemy(target) && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if (!IsDistanceLessThan(target, attackDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Flee);
                }
                if (healthComponent.currentHealth <= fleeHealthPercentage)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            default:
                Debug.Log("Error Picking Between States");
                break;
        }
    }

    //Even attacking he avoids you
    protected override void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);

        pawn.MoveBackward();

        pawn.shoot();
    }
}
