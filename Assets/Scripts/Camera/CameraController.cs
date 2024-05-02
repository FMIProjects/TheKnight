using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    [SerializeField] public Vector2 maxPosition;
    [SerializeField] public Vector2 minPosition;

    private void Start() { }

    private void FixedUpdate()
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
