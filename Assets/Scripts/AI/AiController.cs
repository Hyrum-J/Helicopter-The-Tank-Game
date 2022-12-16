using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class AiController : Controller
{

    protected float lastStateChangeTime;

    public float maxIdleTime;

    public HealthComponent healthComponent;

    public enum AIState { Patrol, Idle, Flee, Attack, Chase };

    public AIState currentState;

    public GameObject target;

    public float fleeHealthPercentage;

    public float followDistance;

    public float attackDistance;

    public float fleeDistance;

    public float hearingDistance;
    public float timeSinceLastHeard;
    public float aiMemory;

    private Vector3 lastSeenPosition;

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
        if(target == null)
        {
            TargetPlayerOne();
        }
        base.Update();
    }

    public virtual void MakeDesicions()
    {
        TargetNearestTank();

        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if (IsDistanceLessThan(target, followDistance) && CanSeeEnemy())
                {
                    ChangeState(AIState.Chase);
                    lastSeenPosition = target.transform.position;
                }
                if (Time.time - lastStateChangeTime > maxIdleTime)
                {
                    ChangeState(AIState.Patrol);
                }
                if (CanHearEnemy(target))
                {
                    ChangeState(AIState.Chase);
                    lastSeenPosition = target.transform.position;
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
                if (healthComponent.currentHealth <= fleeHealthPercentage)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                if (healthComponent.currentHealth > fleeHealthPercentage && CanSeeEnemy())
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
                if (!IsDistanceLessThan(target, attackDistance) || CanSeeEnemy())
                {
                    lastSeenPosition = target.transform.position;
                    ChangeState(AIState.Chase);
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
        if (canMoveForward())
        {
            Debug.Log("Forward Clear Vector3");
            pawn.RotateTowards(targetPos);
            pawn.MoveForward();
        }
        else if (canMoveRight())
        {
            Debug.Log("Right Clear");
            pawn.RotateTowards(pawn.transform.position + pawn.transform.right);
        }
        else if (canMoveLeft())
        {
            Debug.Log("Left Clear");
            pawn.RotateTowards(pawn.transform.position - pawn.transform.right);
        }
        else
        {
            pawn.RotateTowards(pawn.transform.position + pawn.transform.right);
        }
    }
    public void Seek(GameObject target)
    {
        Debug.Log(target);
        if (canMoveForward())
        {
            Debug.Log("Forward Clear");
            pawn.RotateTowards(target.transform.position);
            pawn.MoveForward();
        }
        else if (canMoveRight())
        {
            Debug.Log("Right Clear");
            pawn.RotateTowards(pawn.transform.position + pawn.transform.right);
        }
        else if (canMoveLeft())
        {
            Debug.Log("Left Clear");
            pawn.RotateTowards(pawn.transform.position - pawn.transform.right);
        }
        else
        {
            pawn.RotateTowards(pawn.transform.position + pawn.transform.right);
        }
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
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
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
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null && GameManager.instance.players.Count > 0)
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
        //if (CanSeeEnemy())
        //{
        //  lastSeenPosition = target.transform.position;
        //}
        //else if (CanRemeberTarget(aiMemory, timeSinceLastHeard))
        //{
        //  Seek(lastSeenPosition);
        //}
        Seek(target);
    }

    protected void DoIdleState()
    {

    }

    protected virtual void DoAttackState()
    {
        lastSeenPosition = target.transform.position;

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
            timeSinceLastHeard = Time.time;
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
        Vector3 AgentToTargetVector = target.transform.position - transform.position;
        Vector3 HeightOffsetPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z);
        float angleToTarget = Vector3.Angle(AgentToTargetVector, pawn.transform.forward);

        if (Vector3.Distance(pawn.transform.position, target.transform.position) < viewDistance)
        {

            if (angleToTarget < fov)
            {
                RaycastHit hit;

                if (Physics.Raycast(HeightOffsetPosition, AgentToTargetVector.normalized, out hit, Vector3.Distance(pawn.transform.position, target.transform.position)))
                {
                    if (hit.transform.gameObject == target)
                    {
                        Debug.DrawRay(HeightOffsetPosition, AgentToTargetVector.normalized * viewDistance, Color.green, 20);
                        Debug.Log("Raycast hit " + hit.transform.name);
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(HeightOffsetPosition, AgentToTargetVector.normalized * viewDistance, Color.blue, 20);
                        Debug.Log("Raycast hit but also didn't hit.  Object: " + hit.transform.name);
                        return false;
                    }
                }
                else
                {
                    Debug.DrawRay(HeightOffsetPosition, AgentToTargetVector.normalized * viewDistance, Color.red, 20);
                    Debug.LogError("Raycast didn't hit anything.");
                    return false;
                }
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

    protected bool canMoveForward()
    {
        Vector3 HeightOffsetPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z);
        RaycastHit hit;
        Debug.DrawRay(HeightOffsetPosition, transform.forward.normalized * 10, Color.blue, 20);
        Physics.Raycast(HeightOffsetPosition, transform.forward.normalized, out hit, 10);
        Debug.Log(hit.collider);
        if (CanSeeEnemy())
        {
            return true;
        }
        else if (hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool canMoveRight()
    {
        Vector3 HeightOffsetPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z);
        RaycastHit hit;
        Debug.DrawRay(HeightOffsetPosition, transform.right.normalized * 10, Color.blue, 20);
        Physics.Raycast(HeightOffsetPosition, transform.right.normalized, out hit, 10);
        Debug.Log(hit.collider);
        if (hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool canMoveLeft()
    {
        Vector3 HeightOffsetPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z);
        RaycastHit hit;
        Debug.DrawRay(HeightOffsetPosition,Vector3.left.normalized* 10, Color.blue, 20);
        Physics.Raycast(HeightOffsetPosition, Vector3.left.normalized, out hit, 10);
        Debug.Log(hit.collider);
        if(hit.collider == null)
        {
            return true;
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

    protected bool CanRemeberTarget(float memoryTime, float timeSinceLastInteraction)
    {
        if(Time.time - memoryTime <= timeSinceLastInteraction)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
