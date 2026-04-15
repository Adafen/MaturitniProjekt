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

    private Vector3[] directions = new Vector3[4];
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
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !isAttacking)
            {
                isAttacking = true;
                destination = directions[i];
                checkTimer = 0f;
            }
        }
    }
    private void CheckForWalls()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, destination, 0.5f, wallLayer);

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

    private void OnTriggerEnter2D(Collider2D collision) => base.OnTriggerEnter2D(collision);

}
