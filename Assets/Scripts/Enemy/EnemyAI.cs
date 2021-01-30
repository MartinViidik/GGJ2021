using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float radius = 5;
    public enum EnemyState { WANDER, CHASE, ATTACK };
    public EnemyState ActiveState = EnemyState.WANDER;
    public IAstarAI ai;

    public GameObject playerRef;
    private Transform startPosition;

    private bool ReachedDestination;

    void Start()
    {
        GetPlayer();
        ai = GetComponent<IAstarAI>();
        startPosition = transform;
    }

    Vector3 GetPositionAroundObject(Transform tx)
    {
        Vector3 offset = Random.insideUnitCircle * radius;
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
                    //Debug.Log("Entered wander state");
                    if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
                    {
                        if (!ReachedDestination)
                        {
                            StartCoroutine("GetNewPosition");
                        }
                    }
                    if (PlayerDistance() < 20)
                    {
                        ActiveState = EnemyState.CHASE;
                    }
                }
                break;

            case EnemyState.CHASE:
                {
                    ai.destination = playerRef.transform.position;
                    //Debug.Log("Entered chase state");
                }
                break;
        }
    }
}