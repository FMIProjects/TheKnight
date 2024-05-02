using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParent : MonoBehaviour
{
    private Animator knightAnimator;
    private Animator swordAnimator;
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

    private void Update()
    {
        if (!isAttacking)
        {
            UpdateSwordDirection();
            UpdateKnightAnimation();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void UpdateSwordDirection()
    {
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        Vector2 scale = transform.localScale;
        scale.y = direction.x < 0 ? -1 : 1;
        transform.localScale = scale;
    }

    private void UpdateKnightAnimation()
    {
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        Vector2 scale = transform.localScale;
        if (direction.y > 0)
        {
            knightAnimator.SetFloat("moveX", direction.x < 0.35f && direction.x > -0.35f ? 0 : scale.y);
            knightAnimator.SetFloat("moveY", 1);
        }
        else
        {
            knightAnimator.SetFloat("moveX", direction.x < 0.35f && direction.x > -0.35f ? 0 : scale.y);
            knightAnimator.SetFloat("moveY", -1);
        }
    }

    private void Attack()
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
        Vector3 position = center == null ? Vector3.zero : center.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(center.position, radius))
        {
            EnemyHealthManager health = collider.GetComponent<EnemyHealthManager>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    //Check if player is attacking
    public bool CheckAttacking()
    {
        return isAttacking;
    }
}
