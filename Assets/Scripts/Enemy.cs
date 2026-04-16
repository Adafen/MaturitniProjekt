using UnityEngine;

public class Enemy : Death
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    private float checkTimer;
    private Vector3 destination;

    private bool isAttacking;

    private BoxCollider2D boxCollider;
    private Vector3[] directions = new Vector3[4];

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (isAttacking)
        {
            transform.Translate(destination.normalized * Time.deltaTime * speed);

            CheckForWalls();
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                checkTimer = 0f;
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            // Perform a BoxCast in the current direction to check for the player
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, directions[i], range, playerLayer | wallLayer);

            if (hit.collider != null)
            {
                // Check if the hit object is the player and if the enemy is not already attacking
                if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0 && !isAttacking)
                {
                    isAttacking = true;
                    destination = directions[i];
                    checkTimer = 0f;
                }
                else
                {
                    Debug.DrawRay(transform.position, directions[i] * hit.distance, Color.blue);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, directions[i] * range, Color.red);
            }
        }
    }
    private void CheckForWalls()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, destination, 0.1f, wallLayer);

        if (hit.collider != null)
        {
            isAttacking = false;
        }
    }

    private void CalculateDirections() 
    {
        directions[0] = transform.right * range; // Right
        directions[1] = -transform.up * range;   // Down
        directions[2] = -transform.right * range; // Left
        directions[3] = transform.up * range;   // Up
    }

    private new void OnTriggerEnter2D(Collider2D collision) => base.OnTriggerEnter2D(collision);
}
