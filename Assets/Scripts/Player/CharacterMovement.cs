using System;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    public Animator anim;
    public SpriteRenderer sprite;

    private Vector3 movementDirection;
    private Rigidbody rb;
    private bool CanMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameEvents.current.onPlayerHealthUpdate += onPlayerHealthUpdate;
        GameEvents.current.onPickup += onPickup;
        GameEvents.current.onStunEntity += StunEntity;
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleInput();
            MoveCharacter();
            Animate();
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerHealthUpdate -= onPlayerHealthUpdate;
        GameEvents.current.onPickup -= onPickup;
        GameEvents.current.onItemUsage -= onItemUsage;
        GameEvents.current.onStunEntity -= StunEntity;
    }

    private void StunEntity(int arg1, float arg2)
    {
        if (sprite.flipX == true)
        {
            anim.Play("Baa");
        }
        else
        {
            anim.Play("BaaLeft");
        }
        StartCoroutine("DisableInput", 0.75f);
    }

    void HandleInput()
    {
        movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1f);
    }

    void MoveCharacter()
    {
        rb.velocity = (movementDirection * movementSpeed) * 8;
    }

    void Animate()
    {
        if(movementDirection != Vector3.zero)
        {
            anim.SetFloat("Horizontal", movementDirection.x);
            anim.SetFloat("Vertical", movementDirection.z);
        }
        anim.SetFloat("Speed", movementSpeed);
    }

    void onPlayerHealthUpdate(int health)
    {
        Debug.Log(health);
    }

    public void onPickup(int interactableID, SOItem item)
    {
        if (sprite.flipX == true)
        {
            anim.Play("Pickup");
        }
        else
        {
            anim.Play("PickupLeft");
        }
    }

    public void onItemUsage(int interactableID, NamedInt item){
        if (sprite.flipX == true)
        {
            anim.Play("Pickup");
        }
        else
        {
            anim.Play("PickupLeft");
        }
    }

    public void onStunEntity(int interactableID, float stunPower)
    {
        Debug.Log("test");
    }

    private IEnumerator DisableInput(float length)
    {
        CanMove = false;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(length);
        CanMove = true;
    }
}
