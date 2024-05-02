using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovement : MonoBehaviour
{

    // the amount that will be added to the current boundries of the camera
    public Vector2 cameraChangeMin;
    public Vector2 cameraChangeMax;

    public Vector3 playerChange;
    // camera movement script
    private CameraController cam;


    void Start()
    {
        // gets the camera movement script
        cam = Camera.main.GetComponent<CameraController>();   
    }

    void Update(){}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // the change amounts are added to the current positions
            cam.minPosition += cameraChangeMin;
            cam.maxPosition += cameraChangeMax;

            // the change of the position of the player
            other.transform.position += playerChange;
        }
    }

}
