using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    KnightHealthManager healthManager;
    EnemyHealthManager enemyHealth;
    private float waitToAttack = 10f;
    private bool isTouching;

    [SerializeField] private float power = 10f;

    void Start()
    {
        healthManager = FindObjectOfType<KnightHealthManager>();
        enemyHealth = gameObject.GetComponent<EnemyHealthManager>();
    }

    void Update()
    {
        if(isTouching&&healthManager.healthAmount>0&&enemyHealth.healthAmount>0)
        {
            waitToAttack -= Time.fixedDeltaTime;
            if(waitToAttack < 0f)
            {
                healthManager.TakeDamage(power);
                waitToAttack = 10f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player"&&healthManager.healthAmount>0 && enemyHealth.healthAmount > 0)
        {
            other.gameObject.GetComponent<KnightHealthManager>().TakeDamage(power);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = false;
            waitToAttack = 10f;
        }
    }
}
