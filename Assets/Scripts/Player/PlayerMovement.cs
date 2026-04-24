using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        // Get the Rigidbody2D and Animator components attached to the player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (Time.timeScale == 0f) return;
        // Get horizontal input
        float horizontalInput = 0f;

        // Check for left and right movement input
        if (Input.GetKey(InputManager.MoveLeft))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(InputManager.MoveRight))
        {
            horizontalInput = 1f;
        }

        // Apply movement
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Check for jump input
        if (Input.GetKeyDown(InputManager.Jump))
        {
            Jump();
        }

        // Flip the player sprite based on movement direction
        if (horizontalInput > 0.001f)
        {
            transform.localScale = new Vector2(4, 4); // Face right
        }
        else if (horizontalInput < -0.001f)
        {
            transform.localScale = new Vector2(-4, 4); // Face left
        }

        // Update the animator parameters
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", isGrounded());
    }
    private void Jump()
    {
        if (isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
        
    }
    // This method checks if the player is currently grounded by performing a BoxCast downwards from the player's position
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
