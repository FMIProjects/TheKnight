using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called after all updates have been called
    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            // target that keeps the camera in place on the z axis so thatit does not go out of bounds
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);


            // camera bounding part
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            // interpolates between current object 
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
