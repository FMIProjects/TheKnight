using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float power = 10f;
    private KnightHealthManager healthManager;
    private float waitToAttack = 10f;
    private bool isTouching;

    void Start()
    {
        healthManager = FindObjectOfType<KnightHealthManager>();
    }

    void Update()
    {
        if(isTouching)
        {
            waitToAttack -= Time.fixedDeltaTime;
            if(waitToAttack < 0f)
            {
                healthManager.TakeDamage(power);
                waitToAttack = 10f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            other.gameObject.GetComponent<KnightHealthManager>().TakeDamage(power);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = false;
            waitToAttack = 10f;
        }
    }
}
