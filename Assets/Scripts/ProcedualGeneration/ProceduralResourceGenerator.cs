using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralResourceGenerator : MonoBehaviour
{
    [SerializeField] private GameObject resourceTree;
    [SerializeField] private GameObject resourceBoulder;
    // inventory needs to be manually assigned to each spawned object
    [SerializeField] private GameObject inventory;

    private int countEachObject;

    public void GenerateResources(HashSet<MapCell2> currentFloorPositions)
    {
        // get the harvestable component of the tree and boulder    
        Harvestable treeHarvestable = resourceTree.GetComponent<Harvestable>();
        Harvestable boulderHarvestable = resourceBoulder.GetComponent<Harvestable>();

        // set the inventory of the harvestable component
        treeHarvestable.SetInventory(inventory);
        boulderHarvestable.SetInventory(inventory);

        countEachObject = currentFloorPositions.Count / 20;

        SpawnObjects(currentFloorPositions);
    }

    private void SpawnObjects(HashSet<MapCell2> currentFloorPositions)
    {
        // convert the hash set to list in order to randomly select the positions 
        List<MapCell2> floorCells = new List<MapCell2>(currentFloorPositions);

        for (int i = 0; i < countEachObject; i++)
        {
            // get a random cell
            var chosenCell = floorCells[UnityEngine.Random.Range(0, floorCells.Count)];
            Vector2 spawnPosition = MapCell2.ComputeMiddle(chosenCell);

            // remove it to mark as used
            floorCells.Remove(chosenCell);

            // set the z position to -1 so that the tree is visible
            Instantiate(resourceTree, new Vector3(spawnPosition.x, spawnPosition.y, -1), Quaternion.identity);

        }


        for (int i = 0; i < countEachObject; i++)
        {
            // get a random cell
            var chosenCell = floorCells[UnityEngine.Random.Range(0, floorCells.Count)];
            Vector2 spawnPosition = MapCell2.ComputeMiddle(chosenCell);

            // remove it to mark as used
            floorCells.Remove(chosenCell);

            // set the z position to -1 so that the boulder is visible
            Instantiate(resourceBoulder, new Vector3(spawnPosition.x, spawnPosition.y, -1), Quaternion.identity);

        }

    }
}
