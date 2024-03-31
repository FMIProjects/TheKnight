using UnityEngine;

public class AreaMelee : MonoBehaviour
{
    private float power = 5f;
    // reference to the closest enemy
    private GameObject closestEnemy;
    // health manager of the closest enemy
    private EnemyHealthManager healthManager; 

    void Start()
    {
        //find the closest enemy to the player
        FindClosestEnemy();
    }

    void Update()
    {
        // find closest enemy each frame
        FindClosestEnemy();
    }

    void FindClosestEnemy()
    {
        // find all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
   
        // if there are no enemies, return
        if (enemies.Length == 0)
        {
            closestEnemy = null;
            healthManager = null;
            return;
        }

        // initialize variables to store closest distance and closest enemy
        float closestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // loop through each enemy to find the closest one
        foreach (GameObject enemy in enemies)
        {
            // calculate the distance between this enemy and the player
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            // if this enemy is closer than the previous closest one, update closest enemy and closest distance
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // update reference to closest enemy and its health manager
        closestEnemy = nearestEnemy;
        if (closestEnemy != null)
        {
            healthManager = closestEnemy.GetComponent<EnemyHealthManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If an enemy enters the trigger zone and there is a closest enemy, damage it
        if (other.CompareTag("Enemy") && closestEnemy != null)
        {
            healthManager.TakeDamage(power);
        }
    }
}
