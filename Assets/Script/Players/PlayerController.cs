using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 1f;

    public bool facingRight = true;
    private PlayerControls playerControls;

    private Vector2 movementDirection;
    private Vector2 lastDirection;

    private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    //private SpriteRenderer spriteRenderer;

    //private bool facingLeft = false;


    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Update()
    {
        PlayerInput();
        Animated();
    }
    private void FixedUpdate()
    {        
        Move();
    }
    private void PlayerInput()
    {
        //get the movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        // store the last direction of the player when it stops
        if ((moveX == 0 && moveY == 0) && (movementDirection.x != 0 || movementDirection.y != 0))
        {
            lastDirection = movementDirection;
        }
        movementDirection = new Vector2(moveX, moveY).normalized;
    }
    
    private void Move() 
    {
        rb2d.velocity = new Vector2(movementDirection.x * moveSpeed, movementDirection.y * moveSpeed);
    }
    private void Animated()
    {
        animator.SetFloat("MoveX", movementDirection.x);
        animator.SetFloat("MoveY", movementDirection.y);
        //calculate the length of the moving vector, showing the the player moving or not (magnitude >0 || magnitude <0)
        animator.SetFloat("MoveMagnitude", movementDirection.magnitude);
        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);
    }
}
