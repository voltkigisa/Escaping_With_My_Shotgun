using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float friction = 0.85f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isFacingRight = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveLeft() { moveInput = Vector2.left; }
    public void MoveRight() { moveInput = Vector2.right; }
    public void StopMove() { moveInput = Vector2.zero; }

    void FixedUpdate()
    {
        if (moveInput.x != 0 && IsGrounded())
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        else if (IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x * friction, rb.velocity.y);

        if (moveInput.x > 0 && !isFacingRight) Flip();
        else if (moveInput.x < 0 && isFacingRight) Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsFacingRight() { return isFacingRight; }
    public float GetMoveInput() { return moveInput.x; }
}