using UnityEngine;

public class CharakterAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement playerMovement;

    public float fallDelay = 5f;
    // private float fallTimer = 0f;

    private float highestY = 0f;
    private bool wasGrounded = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        bool isGrounded = playerMovement.IsGrounded();
        float moveX = playerMovement.GetMoveInput();

        // Tracking posisi Y tertinggi saat di udara
        if (!isGrounded)
            highestY = Mathf.Max(highestY, transform.position.y);

        // Reset saat di tanah
        if (isGrounded && !wasGrounded)
        {
            float fallHeight = highestY - transform.position.y;
            //Debug.Log("Fall height: " + fallHeight);

            if (fallHeight >= fallDelay) // fallDelay sekarang jadi threshold ketinggian
            {
                animator.SetTrigger("fall");
                SoundManager.Instance.PlayMendarat();
            }

            highestY = 0f;
        }

        if (isGrounded)
            highestY = Mathf.Max(highestY, transform.position.y);

        wasGrounded = isGrounded;

        if (Mathf.Abs(moveX) > 0.1f && isGrounded)
            SoundManager.Instance.PlayJalan();
        else
            SoundManager.Instance.StopJalan();

        animator.SetFloat("moveInput", Mathf.Abs(moveX));
        animator.SetBool("isGrounded", isGrounded);
    }
}