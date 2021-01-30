using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public EnemyAI Enemy;
    public Animator anim;
    private void Update()
    {
        if (Enemy.AgentVelocity() != Vector3.zero && Enemy.AgentVelocity().magnitude > 2)
        {
            anim.SetFloat("Horizontal", Enemy.AgentVelocity().x);
            anim.SetFloat("Vertical", Enemy.AgentVelocity().z);
        }
        anim.SetFloat("Speed", Enemy.AgentVelocity().magnitude);
    }
}
