using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


[Serializable]
public struct EnemySpawnData
{
    public Enemy prefab;
    public int probabilityWeight;
}


public class EndlessMode : MonoBehaviour
{
    public static EndlessMode Singleton { get; private set; }

    // Serialized variables
    [SerializeField]
    private EnemySpawnData[] enemyPrefabs;

    [Header("Enemies: f(x) = linearSpawnRate * x^(exponentialSpawnRate)")]
    [Range(1, 10), SerializeField]
    private float linearSpawnRate = 3;

    [Range(1, 3), SerializeField]
    private float exponentialSpawnRate = 1.1f;

    [Header("Wave Interval")]
    [SerializeField]
    private float waveInterval = 15f;

    // Private variables
    private int waveNumber = 1;
    private float timeSinceLastWaveStart = 0;

    private (int, int, Enemy)[] spawnProbabilityRangesAndEnemies;
    private int spawnProbabilityWeightSum;

    void Awake()
    {
        #region Singleton boilerplate

        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Debug.LogWarning($"There's more than one {Singleton.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        Singleton = this;

        #endregion Singleton boilerplate

        spawnProbabilityRangesAndEnemies = new (int, int, Enemy)[enemyPrefabs.Length];
        int cumulativeWeight = 0;
        // Fill the `spawnProbabilityRangesAndEnemies` array with tuples each covering a sub-range
        // of the entire probability range from 0 to `spawnProbabilityWeightSum`
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            EnemySpawnData spawnData = enemyPrefabs[i];
            int probabilityRangeStart = cumulativeWeight;
            int probabilityRangeEnd = cumulativeWeight + spawnData.probabilityWeight;
            spawnProbabilityRangesAndEnemies[i] = (
                probabilityRangeStart, probabilityRangeEnd, spawnData.prefab
            );
            cumulativeWeight += spawnData.probabilityWeight;
        }

        spawnProbabilityWeightSum = cumulativeWeight;
    }

    void Start()
    { // Ensure values are assigned
        if (enemyPrefabs.Length == 0)
            Debug.LogError("Enemies are not assigned");
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
        Enemy enemyToSpawn = ChooseRandomEnemy();
        HexCell[] edgeCells = HexGrid.Singleton.edgeCells;
        Vector3 spawnPos = edgeCells[Random.Range(0, edgeCells.Length - 1)].transform.position;
        Instantiate(enemyToSpawn.gameObject, spawnPos, Quaternion.identity, transform);
    }

    private Enemy ChooseRandomEnemy()
    {
        int spawnProbabilityPoint = Random.Range(0, spawnProbabilityWeightSum);
        // The probability range used for searching should only be a single point
        (int, int, Enemy) searchTuple = (spawnProbabilityPoint, spawnProbabilityPoint, null); // the enemy doesn't affect the search, and so can be `null`
        int foundIndex = Array.BinarySearch(spawnProbabilityRangesAndEnemies,
            searchTuple,
            Comparer<(int, int, Enemy)>.Create(
                // `rangeB` is always the search point (`searchTuple`)
                (rangeA, rangeB) =>
                {
                    (int probabilityRangeAStart, int probabilityRangeAEnd, Enemy enemyA) = rangeA;
                    (int probabilityRangeBStart, int probabilityRangeBEnd, Enemy enemyB) = rangeB;
                    if (probabilityRangeBEnd < probabilityRangeAStart)
                        return 1; // `rangeA` is "greater than" `rangeB`
                    if (probabilityRangeBStart >= probabilityRangeAEnd)
                        return -1; // `rangeA` is "less than" `rangeB`
                    // The search point is either equal to the start of the checked range, or somewhere in the middle of it
                    // (but not equal to the end of the range), and so the correct probability range has been found,
                    // and the index of `rangeA` will be returned
                    return 0; // `rangeA` is "equal" to `rangeB`
                })
        );
        Debug.Log(foundIndex);
        (int rangeStart, int rangeEnd, Enemy enemy) = spawnProbabilityRangesAndEnemies[foundIndex];
        return enemy;
    }
}
