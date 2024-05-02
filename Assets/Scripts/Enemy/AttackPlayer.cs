using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float power = 10f;

    private KnightHealthManager healthManager;
    private EnemyHealthManager enemyHealth;
    private float waitToAttack = 10f;
    private bool isTouching;

    private void Start()
    {
        // Find the KnightHealthManager component in the scene
        healthManager = FindObjectOfType<KnightHealthManager>();
        // Get the EnemyHealthManager component attached to this game object
        enemyHealth = GetComponent<EnemyHealthManager>();
    }

    private void Update()
    {
        if (isTouching && healthManager.healthAmount > 0 && enemyHealth.healthAmount > 0)
        {
            // Decrease the wait time to attack
            waitToAttack -= Time.fixedDeltaTime;
            if (waitToAttack < 0f)
            {
                // Attack the player by reducing their health
                healthManager.TakeDamage(power);
                // Reset the wait time to attack
                waitToAttack = 10f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && healthManager.healthAmount > 0 && enemyHealth.healthAmount > 0)
        {
            // Attack the player by reducing their health
            other.gameObject.GetComponent<KnightHealthManager>().TakeDamage(power);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            // Set the flag to indicate that the enemy is touching the player
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            // Reset the flag and wait time to attack when the enemy stops touching the player
            isTouching = false;
            waitToAttack = 10f;
        }
    }
}
