using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 20f;
    DamageFlash damageFlash;

    public GameObject knightObject;
    public GameObject enemyObject;
    private Animator animator;

    public UnityEvent<GameObject> OnHitWithReference;

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
        if (healthAmount > 0)
        {
            OnHitWithReference?.Invoke(knightObject);
            damageFlash.Flash();
            healthAmount -= damage;
        }
    }

    private IEnumerator Death()
    {
        
        yield return new WaitForSeconds(5f);
        // destroy the object
        Destroy(enemyObject);
    }
}
