using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private Transform player;

    private IEnumerator enemySpawnCoroutine;
    private float maxSpawnInterval = 1.5f;
    private float minSpawnInterval = 0.5f;

    // Monobehaviour Function: Called before the first frame update if the script instance is enabled
    private void Start()
    {
        // Start coroutine that spawns enemies
        enemySpawnCoroutine = SpawnEnemy();
        StartCoroutine(enemySpawnCoroutine);
    }

    // Monobehaviour Function: Called when the gameobject/component becomes active
    private void OnEnable()
    {
        // Start having OnGameOver() be called whenever GameOverAction is invoked
        GameManager.GameOverAction += OnGameOver;
    }

    // Monobehaviour Function: Called when the gameobject/component becomes inactive
    private void OnDisable()
    {
        // Stop having OnGameOver() be called whenever GameOverAction is invoked
        GameManager.GameOverAction -= OnGameOver;
    }

    // Continuously spawn enemies at random spawn points with intervals that decrease over time,
    // where the spawn interval is interpolated based on the remaining game time
    private IEnumerator SpawnEnemy()
    {
        // Make a list of the spawnpoints' transforms
        List<Transform> spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>().ToList();

        while (true)
        {
            // Choose index of a random spawnpoint
            int randomIndex = UnityEngine.Random.Range(1, spawnPoints.Count);

            // Instantiate enemy at random spawnpoint
            GameObject enemyObject = Instantiate(enemyPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity, transform);
            EnemyPathFinding enemy = enemyObject.GetComponent<EnemyPathFinding>();
            
            // Set target of enemy
            enemy.destinationTransform = player;
            
            // Interpolate spawn interval as time goes by.
            // When current time is total time, use max spawn interval. When current time is 0, use min spawn interval.
            float spawnInterval = Mathf.Lerp(minSpawnInterval, maxSpawnInterval, gameManager.remainingTime / gameManager.totalTime);

            // Wait for spawnInterval seconds before proceeding
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnGameOver()
    {
        // Stop coroutine that spawns enemies
        StopCoroutine(enemySpawnCoroutine);
    }
}
