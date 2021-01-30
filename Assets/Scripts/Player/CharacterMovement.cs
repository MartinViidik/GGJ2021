using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    public Animator anim;
    public SpriteRenderer sprite;

    private Vector3 movementDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameEvents.current.onPlayerHealthUpdate += onPlayerHealthUpdate;
        GameEvents.current.onPickup += onPickup;
        GameEvents.current.onItemUsage += onItemUsage;
        GameEvents.current.onDealDamage += onDealDamage;
    }

    private void Update()
    {
        HandleInput();
        MoveCharacter();
        Animate();
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerHealthUpdate -= onPlayerHealthUpdate;
        GameEvents.current.onPickup -= onPickup;
        GameEvents.current.onItemUsage -= onItemUsage;
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
        PlayPickupAnimation();
    }

    public void onItemUsage(int interactableID, NamedInt item){
        PlayPickupAnimation();
    }

    void PlayPickupAnimation()
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

    public void onDealDamage(int interactableID, int damageAmount)
    {
        if(damageAmount > 0)
        {
            anim.Play("Damage");
        }
    }
}
