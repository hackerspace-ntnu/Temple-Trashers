using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
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

        spawnProbabilityWeightSum = 0;
        foreach (EnemySpawnData spawnData in enemyPrefabs)
        {
            if (spawnData.probabilityWeight <= 0)
                Debug.LogError($"A probability weight cannot be less than 1! Was {spawnData.probabilityWeight} for \"{spawnData.prefab}\"");
            spawnProbabilityWeightSum += spawnData.probabilityWeight;
        }
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
        Assert.IsNotNull(enemyToSpawn);
        HexCell[] edgeCells = HexGrid.Singleton.SpawnableEdgeCells;
        Vector3 spawnPos = edgeCells[Random.Range(0, edgeCells.Length - 1)].transform.position;
        Instantiate(enemyToSpawn.gameObject, spawnPos, Quaternion.identity, transform);
    }

    private Enemy ChooseRandomEnemy()
    {
        int weightedSpawnProbabilityChoice = Random.Range(0, spawnProbabilityWeightSum);
        int cumulativeWeight = 0;
        foreach (EnemySpawnData spawnData in enemyPrefabs)
        {
            cumulativeWeight += spawnData.probabilityWeight;
            if (weightedSpawnProbabilityChoice < cumulativeWeight)
                return spawnData.prefab;
        }

        // Should never be reached
        return null;
    }
}
