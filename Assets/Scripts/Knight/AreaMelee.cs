using UnityEngine;

public class AreaMelee : MonoBehaviour
{
    private float damage = 5f;

    public GameObject knightObject;
    HealthManager healthManager;

    void Start()
    {
        knightObject = GameObject.Find("RedEnemy");
        healthManager = knightObject.GetComponent<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.GetComponent<HealthManager>() != null)
        {
            healthManager.TakeDamage(damage);
        }
    }
}
