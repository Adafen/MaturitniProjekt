using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;

    void Start()
    {
        // Get the Rigidbody2D and Animator components attached to the player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Flip the player sprite based on movement direction
        if (horizontalInput > 0.001f)
        {
            transform.localScale = new Vector3(5, 5, 5); // Face right
        }
        else if (horizontalInput < -0.001f)
        {
            transform.localScale = new Vector3(-5, 5, 5); // Face left
        }

        // Update the animator parameters
        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        animator.SetTrigger("jump");
        isGrounded = false; // Set grounded to false when jumping
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded by checking for collisions with the ground layer
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
