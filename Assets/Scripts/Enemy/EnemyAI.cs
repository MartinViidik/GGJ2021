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
    bool playerDead = false;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        GetPlayer();
    }

    void Start()
    {
        GetPlayer();
        ai = GetComponent<IAstarAI>();
        stats = GetComponent<EnemyStats>();
        startPosition = transform;
        GameEvents.current.onStunEntity += OnStun;
        GameEvents.current.onPlayerDead += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        playerDead = true;
        //Increase wander radius to avoid enemy wandering around the player corpse
        stats.WanderRadius = 50;
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
        if (interactableID != entity.entityID || ActiveState == EnemyState.STUNNED)
            return;

        StartCoroutine("SetStunned", stunPower);
    }


    private IEnumerator SetStunned(float stunPower)
    {
        ActiveState = EnemyState.STUNNED;
        ai.maxSpeed = 0;
        yield return new WaitForSeconds(stunPower);
        ActiveState = EnemyState.WANDER;
    }

    public Vector3 AgentVelocity()
    {
        return ai.desiredVelocity;
    }

    private void FixedUpdate()
    {
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
                    if (PlayerDistance() < stats.SightDistance && !playerDead)
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
                    if (PlayerDistance() > stats.SightDistance || !playerDead)
                    {
                        ActiveState = EnemyState.WANDER;
                    }
                    //Debug.Log("Entered chase state");
                }
                break;
            case EnemyState.STUNNED:
                {
                }
                break;
        }
    }
}