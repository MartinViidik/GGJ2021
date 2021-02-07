using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public EnemyAI Enemy;
    public Animator anim;
    public SpriteRenderer sprite;

    private void Start()
    {
        GameEvents.current.onStunEntity += OnStun;
    }
    private void OnDestroy()
    {
        GameEvents.current.onStunEntity -= OnStun;
    }

    private void OnStun(int interactableID, float stunPower)
    {
        if(Enemy.AgentVelocity().x <= 0)
        {
            anim.Play("StunLeft");
        } else {
            anim.Play("Stun");
        }
    }
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
