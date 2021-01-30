using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { WANDER, CHASE, ATTACK, STUNNED };
    public EnemyState ActiveState = EnemyState.WANDER;
    public IAstarAI ai;

    public GameObject playerRef;
    private Transform startPosition;
    private EnemyStats stats;

    public bool ReachedDestination;
    bool isStunned;

    void Start()
    {
        GetPlayer();
        ai = GetComponent<IAstarAI>();
        stats = GetComponent<EnemyStats>();
        startPosition = transform;
    }

    Vector3 GetPositionAroundObject(Transform tx)
    {
        Vector3 offset = Random.insideUnitCircle * stats.WanderRadius;
        Vector3 pos = tx.position + offset;
        return pos;
    }

    float PlayerDistance()
    {
        return Vector3.Distance(playerRef.transform.position, transform.position);
    }

    void GetPlayer()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator GetNewPosition()
    {
        ReachedDestination = true;
        yield return new WaitForSeconds(1f);
        ai.destination = GetPositionAroundObject(startPosition);
        ai.SearchPath();
        ReachedDestination = false;
    }
    private IEnumerator SetStunned()
    {
        isStunned = true;
        ai.maxSpeed = 0;
        Debug.Log("Entered stunned state");
        yield return new WaitForSeconds(stats.StunTimer);
        isStunned = false;
        ActiveState = EnemyState.WANDER;

    }
    public Vector3 AgentVelocity()
    {
        return ai.desiredVelocity;
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown("space"))
        {
            ActiveState = EnemyState.STUNNED;
        }
        if (!playerRef)
        {
            GetPlayer();
        }
        switch (ActiveState)
        {
            case EnemyState.WANDER:
                {
                    if (ai.maxSpeed != stats.WanderSpeed)
                        ai.maxSpeed = stats.WanderSpeed;
                    //Debug.Log("Entered wander state");
                    if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
                    {
                        if (!ReachedDestination)
                        {
                            StartCoroutine("GetNewPosition");
                        }
                    }
                    if (PlayerDistance() < stats.SightDistance)
                    {
                        ActiveState = EnemyState.CHASE;
                    }
                }
                break;

            case EnemyState.CHASE:
                {
                    if (ai.maxSpeed != stats.ChaseSpeed)
                        ai.maxSpeed = stats.ChaseSpeed;
                    ai.destination = playerRef.transform.position;
                    //Debug.Log("Entered chase state");
                }
                break;
            case EnemyState.STUNNED:
                {
                    if (!isStunned)
                    {
                        StartCoroutine("SetStunned");
                    }
                }
                break;
        }
    }
}