using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
    }

    public void GoToSpawn()
    {
        animator.SetFloat("moveX", (spawnPos.position.x - transform.position.x));
        animator.SetFloat("moveY", (spawnPos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, spawnPos.position, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, spawnPos.position) == 0)
        {
            animator.SetBool("isMoving", false);
        }
    }
}
