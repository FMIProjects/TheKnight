using UnityEngine;

public class AreaMelee : MonoBehaviour
{
    private float power = 5f;

    public GameObject enemy;
    EnemyHealthManager healthManager;

    void Start()
    {
        enemy = GameObject.Find("RedEnemy");
        healthManager = enemy.GetComponent<EnemyHealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            healthManager.TakeDamage(power);
        }
    }
}
