using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float friction = 0.85f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

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
        if (moveInput.x != 0)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            // Tambah friction saat tidak ada input
            rb.velocity = new Vector2(rb.velocity.x * friction, rb.velocity.y);
        }
    }
}