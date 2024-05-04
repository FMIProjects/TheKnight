using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObjectsSpawner : MonoBehaviour
{

    [SerializeField] private GameObject resourceTree;
    [SerializeField] private GameObject resourceBoulder;
    // inventory needs to be manually assigned to each spawned object
    [SerializeField] private GameObject inventory;
    [SerializeField] private int numberEachObject;
    [SerializeField] private Vector2 minSpawnCoordinates;
    [SerializeField] private Vector2 maxSpawnCoordinates;
    [SerializeField] private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        // get the harvestable component of the tree and boulder    
        Harvestable treeHarvestable = resourceTree.GetComponent<Harvestable>();
        Harvestable boulderHarvestable = resourceBoulder.GetComponent<Harvestable>();

        // set the inventory of the harvestable component
        treeHarvestable.SetInventory(inventory);
        boulderHarvestable.SetInventory(inventory);

        StartCoroutine(SpawnObjectsCoroutine());
    }

    private IEnumerator SpawnObjectsCoroutine()
    {
        if(isActive)
        {
            SpawnObjects();
        }
        
        yield return new WaitForSeconds(2f);
    }

    private void SpawnObjects()
    {
        for(int i = 0; i < numberEachObject; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            while(IsNotValidSpawnPoint(spawnPosition))
            {
                spawnPosition = GetRandomSpawnPosition();
            }

            Instantiate(resourceTree, spawnPosition, Quaternion.identity);
            
        }


        for (int i = 0; i < numberEachObject; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            while (IsNotValidSpawnPoint(spawnPosition))
            {
                spawnPosition = GetRandomSpawnPosition();
            }

            Instantiate(resourceBoulder, spawnPosition, Quaternion.identity);

        }

    }


    // Get a random spawn position within the specified range
    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(minSpawnCoordinates.x, maxSpawnCoordinates.x),
                           Random.Range(minSpawnCoordinates.y, maxSpawnCoordinates.y));
    }

    // Check if the spawn position is valid
    private bool IsNotValidSpawnPoint(Vector2 spawnPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 2f);

        if(colliders.Length > 0)
        {
            return true;
        }

        return false;
    }
}
