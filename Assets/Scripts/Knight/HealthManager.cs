using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image HealthBar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(healthAmount <= 0)
        {
            Respawn();
        }

        if(Input.GetKeyDown("g")) {
            TakeDamage(20f);
        }

        if (Input.GetKeyDown("h"))
        {
            Heal(20f);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        HealthBar.fillAmount = healthAmount/100f;
    }
     
    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0f,100f);

        HealthBar.fillAmount = healthAmount / 100f;
    }

    public void Respawn()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
