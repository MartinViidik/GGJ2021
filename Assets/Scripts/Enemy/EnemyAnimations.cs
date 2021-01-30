using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 previousPosition;
    private Vector3 velocity;

    public Vector3 CurrentMovementDirection
    {
        get
        {
            return velocity.normalized;
        }
    }

    private void Update()
    {
        if (previousPosition != transform.position)
        {
            velocity = previousPosition - transform.position;
            previousPosition = transform.position;
            Debug.Log(previousPosition);
        }
        else
        {
            velocity = Vector3.zero;
        }
    }
}
