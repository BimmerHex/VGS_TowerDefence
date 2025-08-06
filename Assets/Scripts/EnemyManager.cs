using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float spawnCooldown = 3f; // Time between enemy spawns
    public float spawnTimer = 0f; // Timer to track spawn intervals

    [SerializeField] private Transform enemySpawnPoint;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject enemyBasicPrefab;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy(enemyBasicPrefab);
            spawnTimer = spawnCooldown; // Reset the timer
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
    }
}
