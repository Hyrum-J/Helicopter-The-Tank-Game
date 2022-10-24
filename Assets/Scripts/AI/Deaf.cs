using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AiController;
using static UnityEngine.GraphicsBuffer;

public class Deaf : AiController
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

    public override void MakeDesicions()
    {
        TargetNearestTank();

        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if (IsDistanceLessThan(target, followDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                }
                if (Time.time - lastStateChangeTime > maxIdleTime)
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Chase:
                DoChaseState();
                if (!IsDistanceLessThan(target, followDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Idle);
                }
                else if (IsDistanceLessThan(target, attackDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Attack);
                }
                if (HealthComponent.currentHealth <= fleeHealthPercentage)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                if (HealthComponent.currentHealth > fleeHealthPercentage && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                }
                if (!IsDistanceLessThan(target, fleeDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Patrol:
                Patrol();
                if (IsDistanceLessThan(target, followDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if (!IsDistanceLessThan(target, attackDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                }
                if (HealthComponent.currentHealth <= fleeHealthPercentage)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            default:
                Debug.Log("Error Picking Between States");
                break;
        }
    }
}
