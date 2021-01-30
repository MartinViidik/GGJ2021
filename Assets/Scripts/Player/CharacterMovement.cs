using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    public Animator anim;
    public SpriteRenderer sprite;

    private Vector3 movementDirection;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
        MoveCharacter();
        Animate();
    }

    void HandleInput()
    {
        movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 15f);
    }

    void MoveCharacter()
    {
        rb.velocity = (movementDirection * movementSpeed) * 5;
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
}
