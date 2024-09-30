using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController3 : Singleton<PlayerController3>
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    [SerializeField] public float moveSpeed = 5f;

    [SerializeField] private float slipperyZoneMultiplier = 3f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private Knockback knockback;

    private bool facingLeft = false;

    private bool isInSlipperyZone = false;
    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();        
    }
    private void Start()
    {
        EnablePlayerMovement();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        PlayerHealth.OnPlayerDeath += DisablePlayerMovement;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= DisablePlayerMovement;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Movement.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
            {                
                if (isInSlipperyZone)
                {
                    float currentSpeedd = moveSpeed * slipperyZoneMultiplier;
                    rb.MovePosition(rb.position + movement * (currentSpeedd * Time.fixedDeltaTime));
                }
                return;
            }


            float currentSpeed = moveSpeed;

            if (isInSlipperyZone)
            {
                currentSpeed *= slipperyZoneMultiplier;

            }

            rb.MovePosition(rb.position + movement * (currentSpeed * Time.fixedDeltaTime));
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRender.flipX = false;
            FacingLeft = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlipperyZone"))
        {
            isInSlipperyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SlipperyZone"))
        {
            isInSlipperyZone = false; 
        }      
    }
    private void DisablePlayerMovement()
    {
        myAnimator.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
    private void EnablePlayerMovement()
    {
        myAnimator.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}