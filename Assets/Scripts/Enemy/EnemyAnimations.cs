using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public EnemyAI Enemy;
    public Animator anim;
    private void Update()
    {
        if (Enemy.AgentVelocity() != Vector3.zero)
        {
            anim.SetFloat("Horizontal", Enemy.AgentVelocity().x);
            anim.SetFloat("Vertical", Enemy.AgentVelocity().z);
            anim.SetFloat("Speed", 1);
        } else {
            anim.SetFloat("Speed", 0);
        }
    }
}
