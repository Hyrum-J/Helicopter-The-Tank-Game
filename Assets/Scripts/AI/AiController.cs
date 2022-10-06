using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : Controller
{

    private float lastStateChangeTime;

    public enum AIState {Guard, Idle, Seek, Attack, Chase};

    public AIState currentState;

    public GameObject target;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        ChangeState(AIState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeDesicions();
        base.Update();
    }

    public void MakeDesicions()
    {
        switch (currentState)
        {
            case AIState.Guard:
                break;
            case AIState.Idle:
                DoIdleState();
                if(IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Seek:
                DoSeekState();
                break;
            case AIState.Chase:
                DoChaseState();
                if(!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                break;
            default:
                Debug.Log("Error Picking Between States");
                break;
        }
    }

    public override void ProcessInputs()
    {


    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;

        lastStateChangeTime = Time.time;
    }

    public void DoSeekState()
    {
        Seek(target);
    }

    public void Seek(GameObject target)
    {
        pawn.RotateTowards(target.transform.position);

        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        Seek(targetTransform.gameObject);
    }

    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.gameObject);
    }

    protected void DoChaseState()
    {
        Seek(target);
    }

    protected void DoIdleState()
    {
        Debug.Log("In Idle");
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
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
