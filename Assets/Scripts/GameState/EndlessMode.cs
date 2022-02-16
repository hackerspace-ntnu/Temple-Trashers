using UnityEngine;
using System.Collections;


public class EndlessMode : MonoBehaviour
{
    // Public variables
    public HexGrid hexGrid;
    public Enemy[] enemyPrefabs;

    [Header("Enemies: f(x) = linearSpawnRate * x^(exponentialSpawnRate)")]
    [Range(1, 10)]
    public float linearSpawnRate = 3;

    [Range(1, 3)]
    public float exponentialSpawnRate = 1.1f;

    [Header("Wave Interval")]
    public float waveInterval = 15f;

    // Private variables
    private int waveNumber = 1;

    private float timeSinceLastWaveStart = 0;

    void Start()
    { // Ensure values are assigned
        if (enemyPrefabs.Length == 0)
            Debug.LogError("Enemies are not assigned");
        if (hexGrid == null)
            hexGrid = GameObject.Find("Terrain").GetComponent<HexGrid>();
    }

    void Update()
    {
        timeSinceLastWaveStart += Time.deltaTime;

        if (timeSinceLastWaveStart >= waveInterval)
        {
            // Start the coroutine
            StartSpawningWave();
            timeSinceLastWaveStart = 0;
        }
    }

    private void StartSpawningWave()
    {
        // Calculate the amount of enemies to spawn
        int spawnNum = Mathf.RoundToInt(linearSpawnRate * Mathf.Pow(waveNumber, exponentialSpawnRate));

        for (int i = 0; i < spawnNum; i++)
            SpawnEnemy();

        waveNumber++;
    }

    /// <summary>
    /// Spawn a random enemy at a random edgecell
    /// </summary>
    private void SpawnEnemy()
    {
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].gameObject;
        Vector3 spawnPos = hexGrid.edgeCells[Random.Range(0, hexGrid.edgeCells.Length - 1)].transform.position;
        Instantiate(prefab, spawnPos, Quaternion.identity, transform);
    }
}
