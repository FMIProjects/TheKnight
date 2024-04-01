using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    private Rigidbody2D rigidBody;

    [SerializeField] private Transform spawnPos;
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<KnightController>().transform;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) <= maxRange && Vector3.Distance(transform.position, target.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            GoToSpawn();
    }

    public void FollowPlayer()
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", (target.position.x - transform.position.x));
        animator.SetFloat("moveY", (target.position.y - transform.position.y));

        Vector3 direction = (target.transform.position - rigidBody.transform.position).normalized;
        rigidBody.MovePosition(rigidBody.transform.position + direction * speed * Time.fixedDeltaTime);

    }

    public void GoToSpawn()
    {
        animator.SetFloat("moveX", (spawnPos.position.x - transform.position.x));
        animator.SetFloat("moveY", (spawnPos.position.y - transform.position.y));

        Vector3 direction = (spawnPos.transform.position - rigidBody.transform.position).normalized;
        rigidBody.MovePosition(rigidBody.transform.position + direction * speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, spawnPos.position) == 0)
        {
            animator.SetBool("isMoving", false);
        }
    }
}
