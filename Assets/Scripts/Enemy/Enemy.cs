using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Transform targetPosition;
    public float speed;
    public float waypointDistance;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public Seeker seeker;
    public Rigidbody rb;
    public Animator anim;

    private float movementSpeed;

    public void Start()
    {
        Seeker seeker = GetComponent<Seeker>();
        Rigidbody rb = GetComponent<Rigidbody>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }
        Vector3 direction = (path.vectorPath[currentWaypoint] - rb.position).normalized;
        movementSpeed = Mathf.Clamp(direction.magnitude, 0.0f, 15f);

        Debug.Log(direction);

        if (direction != Vector3.zero)
        {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.z);
        }
        anim.SetFloat("Speed", movementSpeed);

        float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < waypointDistance) {
            currentWaypoint++;
        }
    }

    void UpdatePath()
    {
        seeker.StartPath(rb.position, targetPosition.position, OnPathComplete);
    }
}