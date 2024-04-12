using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordParent : MonoBehaviour
{
    private Transform target;
    private Animator enemyAnimator, swordAnimator;
    private bool isAttacking;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float damageAmount = 5f;

    public Transform center;
    public float radius;

    EnemyHealthManager enemyHealth;
    KnightHealthManager knightHealth;

    private void Start()
    {
        enemyAnimator = GetComponentInParent<Animator>();
        swordAnimator = GetComponentInChildren<Animator>();
        target = FindObjectOfType<KnightController>().transform;
        enemyHealth = GetComponentInParent<EnemyHealthManager>();
        knightHealth = GameObject.Find("Knight").GetComponent<KnightHealthManager>();
    }
    void Update()
    {
        if (enemyHealth.healthAmount <= 0)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;

        }
        bool isMoving = enemyAnimator.GetBool("isMoving");
        if (!isAttacking&&isMoving&&enemyHealth.healthAmount>0&&knightHealth.healthAmount>0)
        {
            Vector2 direction = (transform.position -target.position ).normalized;
            transform.right = direction;
            Vector2 scale = transform.localScale;
            
            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else
            {
                if(direction.x>0)
                    scale.y = 1;
                
            }
            transform.localScale = scale;
            if (direction.y > 0)
            {
                if (direction.x < 0.35f && direction.x > -0.35f)
                {
                    enemyAnimator.SetFloat("moveX", 0);
                    enemyAnimator.SetFloat("moveY", -1);
                }
                else
                {
                    enemyAnimator.SetFloat("moveX", scale.y);
                    enemyAnimator.SetFloat("moveY", 0);
                }
            }
            else
            {
                if (direction.x < 0.35f && direction.x > -0.35f)
                {
                    enemyAnimator.SetFloat("moveX", 0);
                    enemyAnimator.SetFloat("moveY", 1);
                }
                else
                {
                    enemyAnimator.SetFloat("moveX", scale.y);
                    enemyAnimator.SetFloat("moveY", 0);
                }
            }

            DetectColliders();

        }
        
    }

    public void Attack()
    {
        if (isAttacking)
        {
            return;
        }
        swordAnimator.SetTrigger("Attack");
        isAttacking = true;
        StartCoroutine(DelayAttack());
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Vector3 position;
        if (center == null)
        {
            position = Vector3.zero;
        }
        else
        {
            position = center.position;
        }
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(center.position, radius))
        {
            KnightHealthManager health;
            if (health = collider.GetComponent<KnightHealthManager>())
            {
                Attack();
                health.TakeDamage(damageAmount);
            }
        }
    }
}
