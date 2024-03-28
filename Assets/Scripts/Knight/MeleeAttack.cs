using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private GameObject rangeOfAttackRight = default;
    private GameObject rangeOfAttackLeft = default;
    private GameObject rangeOfAttackUp = default;
    private GameObject rangeOfAttackDown = default;

    private bool isAttacking = false;

    private float attackTime = 0.25f;
    private float timer = 0f;

    private Animator animator;

    void Start()
    {

        animator = GetComponent<Animator>();
        rangeOfAttackRight = transform.GetChild(0).gameObject;
        rangeOfAttackLeft = transform.GetChild(1).gameObject;
        rangeOfAttackUp = transform.GetChild(2).gameObject;
        rangeOfAttackDown = transform.GetChild(3).gameObject;
        rangeOfAttackRight.SetActive(false);
        rangeOfAttackLeft.SetActive(false);
        rangeOfAttackUp.SetActive(false);
        rangeOfAttackDown.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) && isAttacking == false)
        {
            animator.SetFloat("moveX", 1f);
            animator.SetFloat("moveY", 0f);
            AttackRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isAttacking == false)
        {
            animator.SetFloat("moveX", -1f);
            animator.SetFloat("moveY", 0f);
            AttackLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAttacking == false)
        {
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", 1f);
            AttackUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isAttacking == false)
        {
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", -1f);
            AttackDown();
        }

        if (isAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= attackTime)
            {
                timer = 0;
                isAttacking = false;

                rangeOfAttackRight.SetActive(isAttacking);
                rangeOfAttackLeft.SetActive(isAttacking);
                rangeOfAttackUp.SetActive(isAttacking);
                rangeOfAttackDown.SetActive(isAttacking);
            }

        }
    }

    private void AttackRight()
    {
        isAttacking = true;
        rangeOfAttackRight.SetActive(isAttacking);
    }
    private void AttackLeft()
    {
        isAttacking = true;
        rangeOfAttackLeft.SetActive(isAttacking);
    }
    private void AttackUp()
    {
        isAttacking = true;
        rangeOfAttackUp.SetActive(isAttacking);
    }
    private void AttackDown()
    {
        isAttacking = true;
        rangeOfAttackDown.SetActive(isAttacking);
    }
}
