using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
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
            healthBar.transform.parent.gameObject.SetActive(false);
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
            healthBar.fillAmount = healthAmount / 20f;
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
