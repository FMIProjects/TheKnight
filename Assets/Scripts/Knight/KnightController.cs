using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    public GameObject knightObject;

    KnightHealthManager healthManager;

    private Rigidbody2D rigidBody;
    private Vector3 positionUpdate;
    private Animator animator;
    private Coroutine recharge;
    private Vector2 mousePosition,pointerPosition;
    private SwordParent swordParent;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float currentMovementSpeed;
    [SerializeField] private Image staminaBar;
    [SerializeField] private float stamina = 100;
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float runningCost = 35;

    //MaxCharge -> 3 seconds
    [SerializeField] private float ChargingRate = 33;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        swordParent = GetComponentInChildren<SwordParent>();
        knightObject = GameObject.Find("Knight");
        healthManager = knightObject.GetComponent<KnightHealthManager>();
    }

    void FixedUpdate()
    {
        pointerPosition = getMousePosition();
        swordParent.mousePosition = pointerPosition;
        UpdatePosition();
        UpdateAnimation();
    }

    private void UpdatePosition()
    {
        //The position is updated to zero at every frame
        positionUpdate = Vector3.zero;
        if (healthManager.healthAmount > 0f)
        {
            positionUpdate.x = Input.GetAxisRaw("Horizontal");
            positionUpdate.y = Input.GetAxisRaw("Vertical");
        }
        //Check if shift is pressed 
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        //The character is moving only when there is a key pressed
        if (positionUpdate != Vector3.zero)
        {
            MoveCharacter(isShiftPressed);
        }
    }

    private void UpdateAnimation()
    {
        if (positionUpdate != Vector3.zero)
        {
            animator.SetFloat("moveX", positionUpdate.x);
            animator.SetFloat("moveY", positionUpdate.y);
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
            if (stamina > 0)
            {
                rigidBody.MovePosition(
                    transform.position + positionUpdate * movementSpeed * Time.fixedDeltaTime * 1.5f
                );
                currentMovementSpeed = movementSpeed * 1.5f;
                animator.SetBool("isMoving", false);
                animator.SetBool("isSprinting", true);
            }
            else
            {
                rigidBody.MovePosition(
                   transform.position + positionUpdate * movementSpeed * Time.fixedDeltaTime
                );
                currentMovementSpeed = movementSpeed;
                animator.SetBool("isSprinting", false);
                animator.SetBool("isMoving", true);
            }


            stamina -= runningCost * Time.fixedDeltaTime;

            if (stamina < 0)
            {
                stamina = 0;
            }
            staminaBar.fillAmount = stamina / maxStamina;

            //Stop recharging if sprinting again and start again when stoping
            if (recharge != null)
            {
                StopCoroutine(recharge);

            }
            recharge = StartCoroutine(RechargeStamina());

        }
        else
        {
            rigidBody.MovePosition(
                    transform.position + positionUpdate * movementSpeed * Time.fixedDeltaTime
            );
            currentMovementSpeed = movementSpeed;
            animator.SetBool("isMoving", true);
            animator.SetBool("isSprinting", false);
        }
    }

    private IEnumerator RechargeStamina()
    {
        //Wait for a second before recharging
        yield return new WaitForSeconds(1f);

        while (stamina < maxStamina)
        {
            //Gradually recharge stamina
            stamina += ChargingRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            float fillRatio = stamina / maxStamina;

            //Start recharging the stamina bar from current amount of stamina to the next "milestone", but do it gradually 
            StartCoroutine(UpdateStaminaBar(staminaBar.fillAmount, fillRatio, 0.1f));

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
            staminaBar.fillAmount = Mathf.Lerp(startAmount, targetAmount, percentageComplete);
            yield return null;
        }

        //Make sure that at the end of the interpolation the Stamina Bar reached the target amount
        staminaBar.fillAmount = targetAmount;
    }

    private Vector2 getMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
