using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
<<<<<<< Updated upstream
=======
using UnityEngine.UIElements;
>>>>>>> Stashed changes

public class PlayerController : MonoBehaviour
{
    //public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 1f;

    public bool facingRight = true;
    private PlayerControls playerControls;

    private Vector2 movementDirection;
    private Vector2 lastDirection;
<<<<<<< Updated upstream
=======
    private Vector2 currentDirection;
>>>>>>> Stashed changes

    private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

<<<<<<< Updated upstream
=======
    [SerializeField] private Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private float angle;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        movementDirection = new Vector2(moveX, moveY).normalized;
=======

        movementDirection = new Vector2(moveX, moveY).normalized;
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(this.transform.position);

        // Calculate the angle between the mouse position and player position
        angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg;

        // Determine the direction based on the angle
        if (angle >= -45 && angle <= 45)
        {
            // Facing Right
            currentDirection = Vector2.right; // (1, 0)
        }
        else if (angle > 45 && angle < 135)
        {
            // Facing Up
            currentDirection = Vector2.up; // (0, 1)
        }
        else if (angle >= 135 || angle <= -135)
        {
            // Facing Left
            currentDirection = Vector2.left; // (-1, 0)
        }
        else if (angle < -45 && angle > -135)
        {
            // Facing Down
            currentDirection = Vector2.down; // (0, -1)
        }

>>>>>>> Stashed changes
    }
    
    private void Move() 
    {
<<<<<<< Updated upstream
        rb2d.velocity = new Vector2(movementDirection.x * moveSpeed, movementDirection.y * moveSpeed);
=======
        rb2d.MovePosition(rb2d.position + movementDirection * (moveSpeed * Time.fixedDeltaTime));
>>>>>>> Stashed changes
    }
    private void Animated()
    {
        animator.SetFloat("MoveX", movementDirection.x);
        animator.SetFloat("MoveY", movementDirection.y);
        //calculate the length of the moving vector, showing the the player moving or not (magnitude >0 || magnitude <0)
        animator.SetFloat("MoveMagnitude", movementDirection.magnitude);
        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);
<<<<<<< Updated upstream
=======
        animator.SetFloat("CurrentDirectionX", currentDirection.x);
        animator.SetFloat("CurrentDirectionY", currentDirection.y);
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);           
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
>>>>>>> Stashed changes
    }
}
