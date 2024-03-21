using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightMovement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float CurrentMovementSpeed;
    private Rigidbody2D Rigidbody;
    private Vector3 PositionUpdate;

    private Animator animator;

    public Image StaminaBar;
    public float Stamina = 100, MaxStamina = 100;
    public float RunningCost = 35;
    private Coroutine Recharge;

    //It should take 3 seconds to charge from 33 stamina to 100
    public float ChargingRate = 33;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The position is updated to zero at every frame
        PositionUpdate = Vector3.zero;

        PositionUpdate.x = Input.GetAxisRaw("Horizontal");
        PositionUpdate.y = Input.GetAxisRaw("Vertical");


        //Check if shift is pressed 
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);


        //The character is moving only when there is a key pressed
        if (PositionUpdate != Vector3.zero)
        {
            MoveCharacter(isShiftPressed);
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (PositionUpdate != Vector3.zero)
        {
            animator.SetFloat("moveX", PositionUpdate.x);
            animator.SetFloat("moveY", PositionUpdate.y);
            animator.SetBool("moving", true);
        }
        else
            animator.SetBool("moving", false);
    }

    void MoveCharacter(bool isShiftPressed)
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
            }
            else
            {
                Rigidbody.MovePosition(
                   transform.position + PositionUpdate * MovementSpeed * Time.fixedDeltaTime
                );
                CurrentMovementSpeed = MovementSpeed;
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
