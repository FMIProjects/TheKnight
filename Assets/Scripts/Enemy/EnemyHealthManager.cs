using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 20f;
    DamageFlash damageFlash;

    public GameObject enemyObject;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyObject = GameObject.Find("RedEnemy");
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
        Destroy(enemyObject);
    }
}
