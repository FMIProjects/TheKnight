
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounder : MonoBehaviour
{

    private Vector2 minPosition;
    private Vector2 maxPosition;

    private bool boundsAreSet = false;

    void Update()
    {
        // this implementation guarantees that the bounds will be set onlt once and only after the dungeon is generated

        if (!boundsAreSet)
        {
            var cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            var wallPositions = GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>().GetWallPositions();

            if (wallPositions == null)
                return;

            Debug.Log(GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>());

            computeBounds(wallPositions);

            cameraController.minPosition = minPosition - coordinatesFromCameraCenter;
            cameraController.maxPosition = maxPosition - coordinatesFromCameraCenter;


            Debug.Log("Camera bounds set");

            boundsAreSet = true;
        }



    }

    private void computeBounds(IEnumerable<MapCell2> wallPositions)
    {

        foreach (var cell in wallPositions)
        {
            minPosition.x = Mathf.Min(cell.topLeftCorner.x, minPosition.x);
            minPosition.y = Mathf.Min(cell.bottomLeftCorner.y, minPosition.y);

            maxPosition.x = Mathf.Max(cell.topRightCorner.x, maxPosition.x);
            maxPosition.y = Mathf.Max(cell.topRightCorner.y, maxPosition.y);
        }

    }


}
