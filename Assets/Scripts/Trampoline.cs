using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has a Rigidbody2D component
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply an upward force to the Rigidbody2D to create a bouncing effect
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

            // Trigger the bounce animation
            if (animator != null)
            {
                animator.SetTrigger("isBouncing");
            }

        }
    }
}
