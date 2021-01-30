using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { WANDER, CHASE, ATTACK };
    public EnemyState ActiveState = EnemyState.WANDER;
    public IAstarAI ai;

    public GameObject playerRef;
    private Transform startPosition;
    private EnemyStats stats;

    public bool ReachedDestination;

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
        Debug.Log("starting cooldown");
        yield return new WaitForSeconds(1f);
        ai.destination = GetPositionAroundObject(startPosition);
        ai.SearchPath();
        ReachedDestination = false;
    }
    public Vector3 AgentVelocity()
    {
        return ai.desiredVelocity;
    }

    private void FixedUpdate()
    {
        Debug.Log(AgentVelocity());
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
        }
    }
}