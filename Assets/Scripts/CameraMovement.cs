using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothing;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called after all updates have been called
    void LateUpdate()
    {
        if(transform.position != target.position)
        {   
            // target that keeps the camera in place on the z axis so thatit does not go out of bounds
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // interpolates between current object 
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
