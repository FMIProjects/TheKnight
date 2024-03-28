using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image HealthBar;
    public float healthAmount = 100f;

    public GameObject knightObject;
    DamageFlash damageFlash;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        knightObject = GameObject.Find("Knight");
        damageFlash = knightObject.GetComponent<DamageFlash>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Health", healthAmount);
        if (healthAmount <= 0)
        {

            StartCoroutine(Respawn());
        }

        if (Input.GetKeyDown("g"))
        {
            TakeDamage(20f);

        }

        if (Input.GetKeyDown("h"))
        {
            Heal(20f);
        }
    }

    public void TakeDamage(float damage)
    {
        damageFlash.Flash();
        healthAmount -= damage;
        HealthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0f, 100f);

        HealthBar.fillAmount = healthAmount / 100f;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainTown");
    }
}