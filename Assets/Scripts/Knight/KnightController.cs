using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 5;
    [SerializeField] private float CurrentMovementSpeed;

    private Rigidbody2D Rigidbody;
    private Vector3 PositionUpdate;

    private Animator animator;

    public GameObject knightObject;
    KnightHealthManager healthManager;

    [SerializeField] private Image StaminaBar;
    [SerializeField] private float Stamina = 100;
    [SerializeField] private float MaxStamina = 100;
    [SerializeField] private float RunningCost = 35;

    //MaxCharge -> 3 seconds
    [SerializeField] private float ChargingRate = 33;

    private Coroutine Recharge;

    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        knightObject = GameObject.Find("Knight");
        healthManager = knightObject.GetComponent<KnightHealthManager>();
    }

    void FixedUpdate()
    {
        UpdatePosition();
        UpdateAnimation();
    }

    private void UpdatePosition()
    {
        //The position is updated to zero at every frame
        PositionUpdate = Vector3.zero;
        if (healthManager.healthAmount > 0f)
        {
            PositionUpdate.x = Input.GetAxisRaw("Horizontal");
            PositionUpdate.y = Input.GetAxisRaw("Vertical");
        }
        //Check if shift is pressed 
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        //The character is moving only when there is a key pressed
        if (PositionUpdate != Vector3.zero)
        {
            MoveCharacter(isShiftPressed);
        }
    }

    private void UpdateAnimation()
    {
        if (PositionUpdate != Vector3.zero)
        {
            animator.SetFloat("moveX", PositionUpdate.x);
            animator.SetFloat("moveY", PositionUpdate.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", false);
        }
    }

    private void MoveCharacter(bool isShiftPressed)
    {
        //If shift is pressed sprint
        if (isShiftPressed)
        {
            if (Stamina > 0)
            {
                Rigidbody.MovePosition(
                    transform.position + PositionUpdate * MovementSpeed * Time.fixedDeltaTime * 1.5f
                );
                CurrentMovementSpeed = MovementSpeed * 1.5f;
                animator.SetBool("isMoving", false);
                animator.SetBool("isSprinting", true);
            }
            else
            {
                Rigidbody.MovePosition(
                   transform.position + PositionUpdate * MovementSpeed * Time.fixedDeltaTime
                );
                CurrentMovementSpeed = MovementSpeed;
                animator.SetBool("isSprinting", false);
                animator.SetBool("isMoving", true);
            }


            Stamina -= RunningCost * Time.fixedDeltaTime;

            if (Stamina < 0)
            {
                Stamina = 0;
            }
            StaminaBar.fillAmount = Stamina / MaxStamina;

            //Stop recharging if sprinting again and start again when stoping
            if (Recharge != null)
            {
                StopCoroutine(Recharge);

            }
            Recharge = StartCoroutine(RechargeStamina());

        }
        else
        {
            Rigidbody.MovePosition(
                    transform.position + PositionUpdate * MovementSpeed * Time.fixedDeltaTime
            );
            CurrentMovementSpeed = MovementSpeed;
            animator.SetBool("isMoving", true);
            animator.SetBool("isSprinting", false);
        }
    }

    private IEnumerator RechargeStamina()
    {
        //Wait for a second before recharging
        yield return new WaitForSeconds(1f);

        while (Stamina < MaxStamina)
        {
            //Gradually recharge stamina
            Stamina += ChargingRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            float fillRatio = Stamina / MaxStamina;

            //Start recharging the stamina bar from current amount of stamina to the next "milestone", but do it gradually 
            StartCoroutine(UpdateStaminaBar(StaminaBar.fillAmount, fillRatio, 0.1f));

            //Make sure the stamina is not filled all at once
            yield return new WaitForSeconds(.1f);

        }
    }


    private IEnumerator UpdateStaminaBar(float startAmount, float targetAmount, float duration)
    {
        //This method recharges the stamina bar over a duration of time, assuring a smoother experience
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float percentageComplete = elapsedTime / duration;
            StaminaBar.fillAmount = Mathf.Lerp(startAmount, targetAmount, percentageComplete);
            yield return null;
        }

        //Make sure that at the end of the interpolation the Stamina Bar reached the target amount
        StaminaBar.fillAmount = targetAmount;
    }
}
