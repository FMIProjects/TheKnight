using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int numberEnemies = 3;
    public Vector2 minSpawnCoordinates;
    public Vector2 maxSpawnCoordinates;
    public float spawnInterval = 10f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while(true)
        {
            SpawnEnemies();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemies()
    {

        for(int i = 0;i<3;i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(minSpawnCoordinates.x, maxSpawnCoordinates.x),
                                           Random.Range(minSpawnCoordinates.y, maxSpawnCoordinates.y));

          
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

    }
}
