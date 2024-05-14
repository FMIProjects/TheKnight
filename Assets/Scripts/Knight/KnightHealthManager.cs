using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightHealthManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject knightObject;
    private GameObject sword;

    public float healthAmount = 100f;

    private Animator animator;
    private DamageFlash damageFlash;

    private int deathCount = 0;
    private bool isRespawning = false;

    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
    }

    private void Reload()
    {
        Heal(100f);
        animator.SetFloat("Health", healthAmount);
        animator.SetBool("isRespawned", true);
        isRespawning = false;
    }

    private void Start()
    {
        sword = GameObject.Find("SwordParent");
        animator = GetComponent<Animator>();
        knightObject = GameObject.Find("Knight");
        damageFlash = knightObject.GetComponent<DamageFlash>();
    }

    private void Update()
    {
        animator.SetFloat("Health", healthAmount);

        if (healthAmount <= 0 && !isRespawning)
        {
            animator.SetBool("isRespawned", false);
            sword.SetActive(false);
            StartCoroutine(Respawn());
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20f);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(20f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (healthAmount > 0)
        {
            damageFlash.Flash();
            healthAmount -= damage;
            healthBar.fillAmount = healthAmount / 100f;
        }
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0f, 100f);

        healthBar.fillAmount = healthAmount / 100f;
    }

    private IEnumerator Respawn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(5f);
        deathCount++;
        transform.position = new Vector3(0, 0, 0);
        Reload();
    }
}
