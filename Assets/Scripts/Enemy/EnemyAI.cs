using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    Entity entity;

    public enum EnemyState { WANDER, CHASE, ATTACK, STUNNED };
    public EnemyState ActiveState = EnemyState.WANDER;
    public IAstarAI ai;

    public GameObject playerRef;
    private Transform startPosition;
    private EnemyStats stats;

    public bool ReachedDestination;
    bool isStunned;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        GetPlayer();
        ai = GetComponent<IAstarAI>();
        stats = GetComponent<EnemyStats>();
        startPosition = transform;
        GameEvents.current.onStunEntity += OnStun;
    }

    private void OnDestroy()
    {
        GameEvents.current.onStunEntity -= OnStun;
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


    private void OnStun(int interactableID, float stunPower)
    {
        if (interactableID != entity.entityID || isStunned)
            return;
        StartCoroutine("SetStunned", stunPower);
    }


    private IEnumerator SetStunned(float stunPower)
    {
        Debug.Log(stunPower);
        ActiveState = EnemyState.STUNNED;
        isStunned = true;
        ai.maxSpeed = 0;
        Debug.Log("Entered stunned state");
        yield return new WaitForSeconds(stunPower);
        isStunned = false;
        ActiveState = EnemyState.WANDER;
    }
    public Vector3 AgentVelocity()
    {
        return ai.desiredVelocity;
    }

    private void FixedUpdate()
    {
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