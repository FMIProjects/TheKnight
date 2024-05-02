using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 20f;
    public GameObject knightObject;
    private Animator animator;
    private DamageFlash damageFlash;
    public UnityEvent<GameObject> OnHitWithReference;

    private void Start()
    {
        animator = GetComponent<Animator>();
        damageFlash = GetComponent<DamageFlash>();
    }

    private void Update()
    {
        animator.SetFloat("Health", healthAmount);
        if (healthAmount <= 0)
        {
            GetComponent<EnemyController>().enabled = false;
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
        Destroy(gameObject);
    }
}
