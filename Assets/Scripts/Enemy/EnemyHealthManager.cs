using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 20f;
    DamageFlash damageFlash;

    public GameObject enemyObject;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // get the reference to the object to which this script is attached
        enemyObject = gameObject;
        damageFlash = enemyObject.GetComponent<DamageFlash>();
    }

    void Update()
    {
        animator.SetFloat("Health", healthAmount);
        if (healthAmount <= 0)
        {
            enemyObject.GetComponent<EnemyController>().enabled = false;
            StartCoroutine(Death());
        }
    }

    public void TakeDamage(float damage)
    {
        damageFlash.Flash();
        healthAmount -= damage;
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        // destroy the object
        Destroy(enemyObject);
    }
}
