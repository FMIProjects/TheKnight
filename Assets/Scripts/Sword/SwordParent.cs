using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParent : MonoBehaviour
{

    private Animator knightAnimator,swordAnimator;
    private bool isAttacking;
    [SerializeField] private float delay = 0.25f;
    [SerializeField] private float damageAmount = 5f;
    public Vector2 mousePosition { get; set; }

    public Transform center;
    public float radius;

    private void Start()
    {
        knightAnimator = GetComponentInParent<Animator>();
        swordAnimator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (!isAttacking)
        {
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            transform.right = direction;
            Vector2 scale = transform.localScale;
            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else
            {
                if (direction.x > 0)
                {
                    scale.y = 1;
                }
            }
            transform.localScale = scale;
            if (direction.y > 0)
            {
                if (direction.x < 0.35f && direction.x > -0.35f)
                {
                    knightAnimator.SetFloat("moveX", 0);
                    knightAnimator.SetFloat("moveY", 1);
                }
                else
                {
                    knightAnimator.SetFloat("moveX", scale.y);
                    knightAnimator.SetFloat("moveY", 0);
                }
            }
            else
            {
                if (direction.x < 0.35f && direction.x > -0.35f)
                {
                    knightAnimator.SetFloat("moveX", 0);
                    knightAnimator.SetFloat("moveY", -1);
                }
                else
                {
                    knightAnimator.SetFloat("moveX", scale.y);
                    knightAnimator.SetFloat("moveY", 0);
                }
            }

            
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
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
            EnemyHealthManager health;
            if (health = collider.GetComponent<EnemyHealthManager>())
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    public bool checkAttacking()
    {
        return isAttacking;
    }
}
