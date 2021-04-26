using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager SINGLETON;

    public static readonly Type[] ENEMY_TYPES = {typeof(PlaceholderEnemy)};
    private static readonly Dictionary<Type, GameObject> ENEMY_TYPE_TO_PREFAB = new Dictionary<Type, GameObject>(ENEMY_TYPES.Length);

    public GameObject[] enemyPrefabs;

    public HexGrid hexGrid;

    [Header("Enemy Variables")]
    public float speed = 5f;

    void Awake()
    {
        #region Singleton boilerplate

        if (SINGLETON != null)
        {
            if (SINGLETON != this)
            {
                Debug.LogWarning($"There's more than one {SINGLETON.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        SINGLETON = this;

        #endregion Singleton boilerplate

        foreach (GameObject prefab in enemyPrefabs)
        {
            Type enemyType = prefab.GetComponent<Enemy>().GetType();
            if (ENEMY_TYPE_TO_PREFAB.ContainsKey(enemyType))
                throw new ArgumentException($"Enemy type {enemyType} appears more than once among the enemy prefabs!");

            ENEMY_TYPE_TO_PREFAB.Add(enemyType, prefab);
        }
    }

    public void SpawnEnemy(Type enemyType)
    {
        GameObject prefab = GetEnemyPrefab(enemyType);
        Vector3 spawnPos = ChoosePosForSpawningEnemy();
        Enemy spawnedEnemy = Instantiate(prefab, spawnPos, Quaternion.identity, transform).GetComponent<Enemy>();
        // Pass along the enemy settings
        spawnedEnemy.speed = speed;
    }

    private Vector3 ChoosePosForSpawningEnemy()
    {
        return hexGrid.edgeCells[Random.Range(0, hexGrid.edgeCells.Length)].transform.position;
    }

    public static GameObject GetEnemyPrefab(Type enemyType)
    {
        return ENEMY_TYPE_TO_PREFAB[enemyType];
    }
}
