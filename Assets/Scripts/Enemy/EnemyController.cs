using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    private Rigidbody2D rigidBody;

    private GameObject knightObject;
    private KnightHealthManager knightHealth;

    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    private Vector3 spawnPos;
    private int index = 0;

    private void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        // Find the KnightController object and get its transform
        target = FindObjectOfType<KnightController>().transform;
        spawnPos = transform.position;

        // Find the Knight object and get its KnightHealthManager component
        knightObject = GameObject.Find("Knight");
        knightHealth = knightObject.GetComponent<KnightHealthManager>();
    }

    private void FixedUpdate()
    {
        // Check if the player is in range and the knight's health is greater than 0
        if (IsPlayerInRange() && knightHealth.healthAmount > 0)
        {
            FollowPlayer();
        }
        // Check if the player is too far away or the knight's health is 0 or less
        else if (IsPlayerTooFarAway() || knightHealth.healthAmount <= 0)
        {
            GoToSpawn();
        }
    }

    private bool IsPlayerInRange()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= maxRange && distance >= minRange;
    }

    private bool IsPlayerTooFarAway()
    {
        // Calculate the distance between the player and the enemy
        float distance = Vector3.Distance(target.position, transform.position);
        return distance >= maxRange;
    }

    private void FollowPlayer()
    {
        // Set the animator parameters for movement
        animator.SetBool("isMoving", true);
        animator.SetBool("isReturning", false);
        animator.SetFloat("moveX", GetDirection(target.position.x - transform.position.x));
        animator.SetFloat("moveY", GetDirection(target.position.y - transform.position.y));

        // Calculate the direction towards the player
        Vector3 direction = (target.transform.position - rigidBody.transform.position).normalized;
        rigidBody.MovePosition(rigidBody.transform.position + direction * speed * Time.fixedDeltaTime);
    }

    private void GoToSpawn()
    {
        if (animator.GetBool("isMoving"))
        {
            // Set the animator parameters for returning to spawn
            animator.SetBool("isReturning", true);
            animator.SetFloat("moveX", GetDirection(spawnPos.x - transform.position.x));
            animator.SetFloat("moveY", GetDirection(spawnPos.y - transform.position.y));

            // Calculate the direction towards the spawn position
            Vector3 direction = (spawnPos - rigidBody.transform.position).normalized;
            rigidBody.MovePosition(rigidBody.transform.position + direction * speed * Time.fixedDeltaTime);

            // Check if the enemy has reached the spawn position
            if (Vector3.Distance(transform.position, spawnPos) <= 0.5)
            {
                animator.SetBool("isReturning", false);
                index++;
                Debug.Log("Move set to false!" + index);
                animator.SetBool("isMoving", false);
            }
        }
    }

    private int GetDirection(float x)
    {
        // Determine the direction based on the sign of the input value
        if (x < 0)
            return -1;
        else if (x > 0)
            return 1;
        else
            return 0;
    }
}
