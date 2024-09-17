using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;

    private Transform Parent => transform.parent;

    public void ReadMovement(PlayerController controller)
    {
        moveDirection = controller.Movement.Move.ReadValue<Vector2>().normalized;
    }

    public void Move(Rigidbody2D rb2D)
    {
        rb2D.MovePosition(rb2D.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void AdjustPlayerFollowMouse(SpriteRenderer spritesRenderer)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        spritesRenderer.flipX = mousePos.x > playerScreenPoint.x;
    }

    public Vector2 MoveDirection => moveDirection;
}
