using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    private Rigidbody2D rigidBody;

    GameObject knightObject;
    KnightHealthManager knightHealth;

    private Vector3 spawnPos;
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    private int index = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        // gets the transform of tyhe player
        target = FindObjectOfType<KnightController>().transform;
        // gets the spawn point of the enemy
        spawnPos = transform.position;
        knightObject = GameObject.Find("Knight");
        knightHealth = knightObject.GetComponent<KnightHealthManager>();
    }

    void FixedUpdate()
    {
        // follow the player if it is in the range
        if (Vector3.Distance(transform.position, target.position) <= maxRange && Vector3.Distance(transform.position, target.position) >= minRange&&knightHealth.healthAmount>0)
        {
            FollowPlayer();
        }
        // retreat to spawn poit if gone too far away
        else if (Vector3.Distance(target.position, transform.position) >= maxRange||knightHealth.healthAmount<=0)
            GoToSpawn();
    }

    public void FollowPlayer()
    {
        // set the animator to moving 
        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", (target.position.x - transform.position.x));
        animator.SetFloat("moveY", (target.position.y - transform.position.y));

        Vector3 direction = (target.transform.position - rigidBody.transform.position).normalized;
        rigidBody.MovePosition(rigidBody.transform.position + direction * speed * Time.fixedDeltaTime);

    }

    public void GoToSpawn()
    {

        // check if the character is moving, otherwise there is no use in computing the direction vectors
        if (animator.GetBool("isMoving"))
        {
            // set the animator to moving 
            animator.SetFloat("moveX", (spawnPos.x - transform.position.x));
            animator.SetFloat("moveY", (spawnPos.y - transform.position.y));

            Vector3 direction = (spawnPos - rigidBody.transform.position).normalized;
            rigidBody.MovePosition(rigidBody.transform.position + direction * speed * Time.fixedDeltaTime);

            // if the enemy reached the spawn point , stop moving
            if (Vector3.Distance(transform.position, spawnPos) <= 0.5)
            {
                index++;
                Debug.Log("Move set to false!" + index);
                animator.SetBool("isMoving", false);
            }
        }

        
    }
}
