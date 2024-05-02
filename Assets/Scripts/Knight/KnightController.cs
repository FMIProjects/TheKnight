using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    public GameObject knightObject;

    private KnightHealthManager healthManager;
    private Rigidbody2D rigidBody;
    private Vector3 positionUpdate;
    private Animator animator;
    private Coroutine recharge;
    private Vector2 mousePosition, pointerPosition;
    private SwordParent swordParent;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float currentMovementSpeed;
    [SerializeField] private Image staminaBar;
    [SerializeField] private float stamina = 100;
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float runningCost = 35;
    [SerializeField] private float chargingRate = 33;

    private void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
        // Get the rigidbody component
        rigidBody = GetComponent<Rigidbody2D>();
        // Get the SwordParent component
        swordParent = GetComponentInChildren<SwordParent>();
        // Find the knight object in the scene
        knightObject = GameObject.Find("Knight");
        // Get the KnightHealthManager component from the knight object
        healthManager = knightObject.GetComponent<KnightHealthManager>();
    }

    private void FixedUpdate()
    {
        // Get the mouse position in world coordinates
        pointerPosition = GetMousePosition();
        if (swordParent != null)
        {
            // Set the mouse position for the SwordParent component
            swordParent.mousePosition = pointerPosition;
        }

        // Update the position and animation
        UpdatePosition();
        UpdateAnimation();
    }

    private void UpdatePosition()
    {
        // Reset the position update vector
        positionUpdate = Vector3.zero;
        if (healthManager.healthAmount > 0f)
        {
            // Get the horizontal and vertical input axes
            positionUpdate.x = Input.GetAxisRaw("Horizontal");
            positionUpdate.y = Input.GetAxisRaw("Vertical");
        }

        // Check if the left shift key is pressed
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        if (positionUpdate != Vector3.zero)
        {
            // Move the character
            MoveCharacter(isShiftPressed);
        }
    }

    private void UpdateAnimation()
    {
        if (positionUpdate != Vector3.zero)
        {
            // Set the moveX and moveY parameters of the animator
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
        if (isShiftPressed)
        {
            if (stamina > 0)
            {
                // Move the character with increased speed
                rigidBody.MovePosition(
                    transform.position + positionUpdate * movementSpeed * Time.fixedDeltaTime * 1.5f
                );
                currentMovementSpeed = movementSpeed * 1.5f;
                animator.SetBool("isMoving", false);
                animator.SetBool("isSprinting", true);
            }
            else
            {
                // Move the character with normal speed
                rigidBody.MovePosition(
                    transform.position + positionUpdate * movementSpeed * Time.fixedDeltaTime
                );
                currentMovementSpeed = movementSpeed;
                animator.SetBool("isSprinting", false);
                animator.SetBool("isMoving", true);
            }

            // Decrease stamina
            stamina -= runningCost * Time.fixedDeltaTime;

            if (stamina < 0)
            {
                stamina = 0;
            }
            staminaBar.fillAmount = stamina / maxStamina;

            if (recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            // Move the character with normal speed
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
        yield return new WaitForSeconds(1f);

        while (stamina < maxStamina)
        {
            // Increase stamina
            stamina += chargingRate / 10f;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
            float fillRatio = stamina / maxStamina;

            StartCoroutine(UpdateStaminaBar(staminaBar.fillAmount, fillRatio, 0.1f));

            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator UpdateStaminaBar(float startAmount, float targetAmount, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float percentageComplete = elapsedTime / duration;
            staminaBar.fillAmount = Mathf.Lerp(startAmount, targetAmount, percentageComplete);
            yield return null;
        }

        staminaBar.fillAmount = targetAmount;
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
