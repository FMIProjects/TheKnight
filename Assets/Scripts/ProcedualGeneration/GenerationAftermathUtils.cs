using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerationAftermathUtils : MonoBehaviour
{
    // used to fill the observable gaps outside the map
    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject knightObject;

    [SerializeField]
    private ProceduralResourceGenerator resourceGeneration;

    // needed to set the camera bounds
    CameraController cameraController;

    // needed to set the camera bounds and to fill the gaps
    private Vector2 minPosition;
    private Vector2 maxPosition;

    private bool boundsAreSet = false;
    private bool gapsAreFilled = false;
    private bool sceneSwitcherPlaced = false;
    private bool enemiesPlaced = false;
    private bool resourceObjectsPlaced = false;
    private int numberEnemies;
    private bool isCompleted = false;
    // needed to fill the gaps and to set the camera bounds
    HashSet<MapCell2> wallPositions;
    HashSet<MapCell2> floorPositions;

    //needed to place scene switcher
    GameObject sceneSwitcher;

    void Update()
    {
        
        SetCameraBounds();
        FillGaps();
        PlaceSceneSwitcher();
        PlaceEnemies();
        PlaceResourceObjects();

        //real update

        TryCompleteDungeon();
    }

    private void PlaceResourceObjects()
    {
        if(resourceObjectsPlaced)
        {
            return;
        }

        resourceGeneration.GenerateResources(floorPositions);

        resourceObjectsPlaced = true;
    }

    private void PlaceEnemies()
    {
        if (enemiesPlaced)
        {
            return;
        }

        // set the knight object for the enemy health manager
        enemyPrefab.GetComponent<EnemyHealthManager>().knightObject = knightObject;

        numberEnemies = ComputeNumberOfEnemies();


        // remove the neighbours of the center cell so that the enemies are not spawned near the player

        RemoveCenterNeighbours();

        // convert the hash set to list in order to randomly select the positions
        List<MapCell2> floorCells = new List<MapCell2>(floorPositions);

        for(int i=0;i < numberEnemies; ++i)
        {
            // get a random cell
            var randomCell = floorCells[UnityEngine.Random.Range(0, floorCells.Count)];

            // remove it to mark as used
            floorCells.Remove(randomCell);
            floorPositions.Remove(randomCell);

            // spawn the enemy in the middle of the selected cell
            Vector2 spawnPosition = MapCell2.ComputeMiddle(randomCell);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        }



        enemiesPlaced = true;
    }

    private void PlaceSceneSwitcher()
    {
        if(sceneSwitcherPlaced)
        {
            return;
        }
         
        floorPositions = GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>().GetFloorPositions();
        sceneSwitcher = GameObject.FindGameObjectWithTag("SceneSwitcher");

        // calculate the fartherst position from the center of the map using the euclidian distance

        MapCell2 bestPosition = MapCell2.zero;
        float maxDistance = 0.0f;

        foreach(var cell in floorPositions)
        {
            float currentDistance = MapCell2.Distance(cell, bestPosition);
            if(currentDistance >= maxDistance)
            {
                maxDistance = currentDistance;
                bestPosition = cell;
            }
        }
        
        // get the middle 
        Vector2 middle = MapCell2.ComputeMiddle(bestPosition); 

        // set the position of the scene switcher to the middle of the farthest position
        sceneSwitcher.transform.position = new Vector3(middle.x, middle.y, -1);

        // remove the position to mark it as used
        floorPositions.Remove(bestPosition);

        // deactivate the scene switcher
        sceneSwitcher.SetActive(false);
        sceneSwitcherPlaced = true;
    }

    private void FillGaps()
    {
        if(gapsAreFilled)
        {
            return;
        }

        // get the wall and floor positions
        wallPositions = GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>().GetWallPositions();
        floorPositions = GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>().GetFloorPositions();

        Debug.Log(floorPositions);

        // get all positions
        var allPositions = new HashSet<MapCell2>(wallPositions); 
        allPositions.UnionWith(floorPositions);     

        // flatten the positions to vectors
        var allVectorPositions = new HashSet<Vector2Int>(allPositions.SelectMany(cell => cell.getCorners()));

        // the minPosition and maxPosition will be updated based on this offstet
        // 2 * 5
        var offset = new Vector2Int(10, 10);

        minPosition -= offset;
        maxPosition += offset;

        // iterate through all positions and paint all unpainted floor tiles
        for(int i= (int)minPosition.x; i <= (int)maxPosition.x; ++i)
        {
            for(int j = (int)minPosition.y; j <= (int)maxPosition.y; ++j)
            {
                if(!allVectorPositions.Contains(new Vector2Int(i,j)))
                {
                    tilemapVisualizer.PaintSingleFloorTile(new Vector2Int(i,j));
                }
            }
        }

        minPosition += offset;
        maxPosition -= offset;
        gapsAreFilled = true;

    }

    private void SetCameraBounds()
    {
        // this implementation guarantees that the bounds will be set onlt once and only after the dungeon is generated
        if (boundsAreSet)
        {
            return;
        }
        
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        wallPositions = GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>().GetWallPositions();

        if (wallPositions == null)
            return;

        Debug.Log(GameObject.FindGameObjectWithTag("Grid").GetComponent<SimpleRandomWalkDungeonGenerator>());

        computeBounds(wallPositions);

        cameraController.minPosition = minPosition;
        cameraController.maxPosition = maxPosition;


        Debug.Log("Camera bounds set");

        boundsAreSet = true;
        
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

    private void RemoveCenterNeighbours()
    {
        var centerNeighbours = MapCell2.GetNeighbours(MapCell2.zero);

        floorPositions.ExceptWith(centerNeighbours);
        
        foreach(var neighbour in centerNeighbours)
        {
            floorPositions.ExceptWith(MapCell2.GetNeighbours(neighbour));
        }
        
    }

    private int ComputeNumberOfEnemies()
    {
        return floorPositions.Count / 20;
    }

    private void TryCompleteDungeon()
    {
        if(isCompleted)
        {
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        if (enemies.Length <= (int) (numberEnemies/2.0))
        {
            Debug.Log("Dungeon completed");
            sceneSwitcher.SetActive(true);
            isCompleted = true;

        }

    }


}
