using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 1f;

    public bool facingRight = true;
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool facingLeft = false;
    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        //PlayerFacing();
        Move();
    }
    private void PlayerInput()
    {
        // Get movement input
        movement = playerControls.Movement.Movement.ReadValue<Vector2>();

        // Reset all directions to false
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);

        // Check for movement direction
        if (movement.y > 0) // Walking Up
        {
            animator.SetBool("isWalkingUp", true);
        }
        else if (movement.y < 0) // Walking Down
        {
            animator.SetBool("isWalkingDown", true);
        }
        else if (movement.x < 0) // Walking Left
        {
            animator.SetBool("isWalkingLeft", true);
        }
        else if (movement.x > 0) // Walking Right
        {
            animator.SetBool("isWalkingRight", true);
        }

        // Optionally, set the MoveX and MoveY values for other uses (e.g., if you want to track direction)
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }

    private void Move() 
    {
        rb2d.MovePosition(rb2d.position+movement*(moveSpeed*Time.fixedDeltaTime));
    }
    private void PlayerFacing()
    {
        if (movement.x < 0 && facingRight)
        {
            Flip(false);
        }
        // Check if moving right
        else if (movement.x > 0 && !facingRight)
        {
            Flip(true);
        }
    }

    //// Flips the sprite based on direction
    private void Flip(bool faceRight)
    {
        facingRight = faceRight;
        spriteRenderer.flipX = !faceRight; // flipX flips the sprite horizontally
    }
}
