using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int numberEnemies = 3;
    public Vector2 minSpawnCoordinates;
    public Vector2 maxSpawnCoordinates;
    public float spawnInterval = 10f;
    public bool isActive = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while(true)
        {
            // spawn while active
            if (isActive)
                SpawnEnemies();

            yield return new WaitForSeconds(spawnInterval);
        }
    }


    void SpawnEnemies()
    {

        for (int i = 0;i<3;i++)
        {
            // generate a random position in the given area 
            Vector2 spawnPosition = new Vector2(Random.Range(minSpawnCoordinates.x, maxSpawnCoordinates.x),
                                           Random.Range(minSpawnCoordinates.y, maxSpawnCoordinates.y));

            // 
            while (IsNotValidSpawnPoint(spawnPosition))
            {
                
                spawnPosition.x = Random.Range(minSpawnCoordinates.x, maxSpawnCoordinates.x);
                spawnPosition.y = Random.Range(minSpawnCoordinates.y, maxSpawnCoordinates.y);
            }


            // instantiate a new enemies based on a prefab
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

    }


    bool IsNotValidSpawnPoint(Vector2 spawnPosition)
    {
        // get all colliders close of the spawn-point in a radius of 2
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 2f);

        foreach (Collider2D currentCollider in colliders)
        {
            // if the collider exists and the tag is tilemap return true
            if (currentCollider != null && currentCollider.gameObject.CompareTag("TileMapCollider"))
            {
                return true; 
            }
        }
        // returns false if the spawnpoint is valid
        return false;
    }
}
