using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class AiController : Controller
{

    protected float lastStateChangeTime;

    public float maxIdleTime;

    public enum AIState {Patrol, Idle, Flee, Attack, Chase};

    public AIState currentState;

    public GameObject target;

    public float fleeHealthPercentage;

    public float followDistance;

    public float attackDistance;

    public float fleeDistance;

    public float hearingDistance;

    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;

    public float fov;
    public float viewDistance;
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

    public virtual void MakeDesicions()
    {
        TargetNearestTank();

        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if(IsDistanceLessThan(target, followDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                }
                if(Time.time - lastStateChangeTime > maxIdleTime)
                {
                    ChangeState(AIState.Patrol);
                }
                if(CanHearEnemy(target))
                {
                    ChangeState(AIState.Chase);
                }    
                break;
            case AIState.Chase:
                DoChaseState();
                if(!IsDistanceLessThan(target, followDistance) || !CanSeeEnemy())
                {
                    ChangeState(AIState.Idle);
                }
                else if(IsDistanceLessThan(target, attackDistance) && CanSeeEnemy())
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
                if (CanHearEnemy(target))
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

    public void Seek(Vector3 targetPos)
    {
        pawn.RotateTowards(pawn.transform.position + targetPos);
        pawn.MoveForward();
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

    protected void Flee()
    {
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        Vector3 vectorAwayFromTarget = -vectorToTarget;

        
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        Vector3 fleeVector = vectorAwayFromTarget.normalized * flippedPercentOfFleeDistance;
        Seek(fleeVector);
    }

    protected virtual void Patrol()
    {
        if (waypoints.Length > currentWaypoint)
        {
            Seek(waypoints[currentWaypoint]);
            if(Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        currentWaypoint = 0;
    }

    protected virtual void TargetPlayerOne()
    {
        if(GameManager.instance != null)
        {
            if(GameManager.instance.players != null && GameManager.instance.players.Count > 0)
            {
                target = GameManager.instance.players[0].pawn.gameObject;
            }
        }
    }

    protected virtual void TargetNearestTank()
    {
        Pawn closestTank;
        float closestTankDistance;

        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null && GameManager.instance.players.Count > 0)
            {
                closestTank = GameManager.instance.players[0].pawn;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);


                foreach (PlayerController player in GameManager.instance.players)
                {
                    if (Vector3.Distance(pawn.transform.position, player.pawn.transform.position) <= closestTankDistance)
                    {
                        closestTank = player.pawn;
                        closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                    }
                }

                target = closestTank.gameObject;

            }
        }
    }

    protected void DoChaseState()
    {
        Seek(target);
    }

    protected void DoIdleState()
    {

    }

    protected virtual void DoAttackState()
    {
        Seek(target);

        pawn.shoot();
    }

    protected virtual void DoFleeState()
    {
        Flee();
    }

    protected virtual bool IsDistanceLessThan(GameObject target, float distance)
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

    protected bool CanHearEnemy(GameObject target)
    {
        TotallyNotWhatItIs noiseMaker = target.GetComponent<TotallyNotWhatItIs>();
        if (noiseMaker == null)
        {
            return false;
        }
        if (noiseMaker.currentVolumeDistance <= 0)
        {
            return false;
        }
        
        float totalDistance = noiseMaker.currentVolumeDistance + hearingDistance;
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        }
        else
        {
             Debug.Log("Volume:" + totalDistance);
             return false;
        }
       
    }

    protected bool CanSeeEnemy()
    {
        if(Vector3.Distance(pawn.transform.position, target.transform.position) < viewDistance)
        {
            Vector3 AgentToTargetVector = target.transform.position - transform.position;

            float angleToTarget = Vector3.Angle(AgentToTargetVector, pawn.transform.forward);

            if (angleToTarget < fov)
            {
                //Ray ray = new Ray(transform.position, target.transform.position);
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position);
                //GameObject hitGameObject = hit.transform.gameObject;
                //if (hitGameObject == target)
                //{
                    return true;
                //}
                //else
                //{
                  //  Debug.Log(Physics.Raycast(pawn.transform.position, target.transform.position));
                    //return false;
                //}

            }
            else
            {
                return false;
            }
                
        }   
        else
        {
            return false;
        }    
    }

    protected bool IsHasTarget()
    {
        return target != null;
    }
}
