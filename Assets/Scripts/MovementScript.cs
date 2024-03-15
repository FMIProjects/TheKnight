using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float MovementSpeed = 10;
    public float CurrentMovementSpeed;
    private Rigidbody2D Rigidbody;
    private Vector3 PositionUpdate;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
    }
    void MoveCharacter(bool isShiftPressed)
    {
        //If shift is pressed sprint
        if (isShiftPressed)
        {
            Rigidbody.MovePosition(
                transform.position + PositionUpdate * MovementSpeed * Time.deltaTime * 2
                
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
