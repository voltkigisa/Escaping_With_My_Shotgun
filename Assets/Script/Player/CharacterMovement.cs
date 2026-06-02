using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private CharacterMovement movement;
    private bool wasGrounded;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float friction = 0.85f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Dipanggil oleh button Kiri
    public void MoveLeft()
    {
        moveInput = Vector2.left;
    }

    // Dipanggil oleh button Kanan
    public void MoveRight()
    {
        moveInput = Vector2.right;
    }

    // Dipanggil saat button dilepas
    public void StopMove()
    {
        moveInput = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (moveInput.x != 0 && IsGrounded())
        {
            rb.velocity = new Vector2(
                moveInput.x * moveSpeed,
                rb.velocity.y
            );
        }
        else if (IsGrounded())
        {
            rb.velocity = new Vector2(
                rb.velocity.x * friction,
                rb.velocity.y
            );
        }
    }
}