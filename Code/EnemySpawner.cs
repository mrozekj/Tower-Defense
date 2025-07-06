using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;   // Array of different enemy prefabs to spawn

    [Header("Attributes")]
    [SerializeField] private int firstWaveEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScaling = 0.75f;
    [SerializeField] private float enemiesPerSecondMaxCap = 15f;  //maximum cap for enemies spawned per second for balancing sake

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();    // Event to notify when an enemy is destroyed

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private float spawnInterval;

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);  // Register the EnemyDestroyed method to the onEnemyDestroy event

    }
    

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update() {    // Exit early if spawning not active
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / spawnInterval) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f; 
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
            EndWave();
        }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        spawnInterval = EnemiesPerSecond();
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f; 
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy() {
        int index = Random.Range(0, enemyPrefabs.Length);  // Select enemy prefarb at random 
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, GameManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave() {   // difficulty scaling
        return Mathf.RoundToInt(firstWaveEnemies * Mathf.Pow(currentWave, difficultyScaling));
    }

     private float EnemiesPerSecond() {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScaling), 0f, enemiesPerSecondMaxCap);
    }

}
