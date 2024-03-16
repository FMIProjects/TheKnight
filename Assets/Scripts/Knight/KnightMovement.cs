using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float CurrentMovementSpeed;
    private Rigidbody2D Rigidbody;
    private Vector3 PositionUpdate;

    private Animator animator;

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
            Rigidbody.MovePosition(
                transform.position + PositionUpdate * MovementSpeed * Time.fixedDeltaTime * 2
                
            );
            CurrentMovementSpeed = MovementSpeed * 2;
        }
        else
        {
            Rigidbody.MovePosition(
                    transform.position + PositionUpdate * MovementSpeed * Time.deltaTime
            );
            CurrentMovementSpeed = MovementSpeed;
        }
    }
}
