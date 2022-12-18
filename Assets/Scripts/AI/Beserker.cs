using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beserker : AiController
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

    //Takes out flee for the beserker
    public override void MakeDesicions()
    {
        TargetPlayerOne();

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
                if (CanHearEnemy(target) && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
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
                break;
            case AIState.Patrol:
                Patrol();
                if (IsDistanceLessThan(target, followDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                }
                if (CanHearEnemy(target) && CanSeeEnemy())
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
                break;
            default:
                Debug.Log("Error Picking Between States");
                break;
        }
    }
}
