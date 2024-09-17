using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;
    private Movement movement;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        controller = new PlayerController();
        movement = GetComponentInChildren<Movement>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        movement.Move(rb2D);
        movement.AdjustPlayerFollowMouse(spriteRenderer);
    }
    private void Update()
    {
        movement.ReadMovement(controller);
    }

    #region Enable/Disable Controller
    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }
    #endregion
}
