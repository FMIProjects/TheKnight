using UnityEngine;

public class CameraController : MonoBehaviour,IDataPersistance
{

    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    [SerializeField] public Vector2 maxPosition;
    [SerializeField] public Vector2 minPosition;

  
    public void LoadData(GameData data)
    {
        // Load the camera position + minPosition + maxPosition from the game data
        transform.position = data.cameraPosition;
        minPosition = data.cameraMinPosition;
        maxPosition = data.cameraMaxPosition;
    }

    public void SaveData(ref GameData data)
    {
        if (this != null)
        { // Save the camera position + minPosition + maxPosition from the game data
            data.cameraPosition = transform.position;
            data.cameraMinPosition = minPosition;
            data.cameraMaxPosition = maxPosition;
        }
    }

    private void FixedUpdate()
    {
        //if (target.position == new Vector3(0, 0, 0))
        //{
        //    maxPosition = new Vector2(27.06f, 22.33f);
        //    minPosition = new Vector2(-24f, -25.4f);
        //}

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
