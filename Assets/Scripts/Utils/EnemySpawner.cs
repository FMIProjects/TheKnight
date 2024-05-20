using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberEnemies = 3;
    public int maxEnemies = 20;
    public Vector2 minSpawnCoordinates;
    public Vector2 maxSpawnCoordinates;
    public float spawnInterval = 10f;
    public bool isActive = true;

    // needed for the enemy health manager
    public GameObject knightObject;

    private void Start()
    {
        // set the knight object for the enemy health manager
        enemyPrefab.GetComponent<EnemyHealthManager>().knightObject = knightObject;   
        StartCoroutine(SpawnEnemiesRoutine());
    }

    // Coroutine to continuously spawn enemies
    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            if (isActive)
                SpawnEnemies();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawn enemies
    private void SpawnEnemies()
    {
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length; // Get the current number of enemies

        if (currentEnemies >= maxEnemies) // Check if the maximum cap is reached
            return;

        int enemiesToSpawn = Mathf.Min(numberEnemies, maxEnemies - currentEnemies); // Calculate the number of enemies to spawn

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            while (IsNotValidSpawnPoint(spawnPosition))
            {
                spawnPosition = GetRandomSpawnPosition();
            }

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
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

        foreach (Collider2D currentCollider in colliders)
        {
            if (currentCollider != null && currentCollider.gameObject.CompareTag("TileMapCollider"))
            {
                return true;
            }
        }

        return false;
    }
}
