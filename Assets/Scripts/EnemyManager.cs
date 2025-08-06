using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public int basicEnemy;
    public int fastEnemy;
}

public class EnemyManager : MonoBehaviour
{
    [Header("Wave Details")]
    [Tooltip("Details of the current wave including enemy types and counts")]
    [SerializeField] private WaveDetails currentWave;

    [Header("Enemy Spawn Point")]
    [Tooltip("Point where enemies will spawn")]
    [SerializeField] private Transform enemySpawnPoint;

    [Header("Enemy Spawn Settings")]
    [Tooltip("Time between enemy spawns")]
    [SerializeField] private float spawnCooldown = 3f;

    private float spawnTimer; // Timer to track spawn cooldown

    private List<GameObject> enemiesToSpawn; // List of enemies to spawn in the current wave

    [Header("Enemy Prefabs")]
    [Tooltip("Prefabs for different types of enemies")]
    [SerializeField] private GameObject enemyBasicPrefab;
    [SerializeField] private GameObject enemyFastPrefab;

    private void Start()
    {
        enemiesToSpawn = GenerateEnemies();
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f && enemiesToSpawn.Count > 0)
        {
            SpawnEnemy();
            spawnTimer = spawnCooldown; // Reset the timer
        }
    }

    private void SpawnEnemy()
    {
        GameObject randomEnemy = GetRandomEnemyPrefab();
        GameObject enemy = Instantiate(randomEnemy, enemySpawnPoint.position, Quaternion.identity);
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemiesToSpawn.Count);
        GameObject randomEnemy = enemiesToSpawn[randomIndex];

        enemiesToSpawn.Remove(randomEnemy);

        return randomEnemy;
    }

    private List<GameObject> GenerateEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();

        for (int i = 0; i < currentWave.basicEnemy; i++)
        {
            enemies.Add(enemyBasicPrefab);
        }

        for (int i = 0; i < currentWave.fastEnemy; i++)
        {
            enemies.Add(enemyFastPrefab); // Uncomment and define enemyFastPrefab if needed
        }

        return enemies;
    }
}
