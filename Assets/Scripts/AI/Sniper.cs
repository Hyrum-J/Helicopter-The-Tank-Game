using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : AiController
{
    public float minAttackDistance;

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

    //Sniper stays back and fires from afar. Does not like to get close.
    public override void MakeDesicions()
    {
        TargetNearestTank();

        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if (IsDistanceLessThan(target, attackDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                if (Time.time - lastStateChangeTime > maxIdleTime)
                {
                    ChangeState(AIState.Patrol);
                }
                if (CanHearEnemy(target) && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                if (healthComponent.currentHealth > fleeHealthPercentage && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                if (!IsDistanceLessThan(target, fleeDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Patrol:
                Patrol();
                if (IsDistanceLessThan(target, attackDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
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
                    ChangeState(AIState.Patrol);
                }
                else if (IsDistanceLessThan(target, minAttackDistance) && CanSeeEnemy())
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

    protected override void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);

        pawn.shoot();
    }

    protected override bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
