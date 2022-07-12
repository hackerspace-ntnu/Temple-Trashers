using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{
    private static EnemyManager SINGLETON;

    public static readonly Type[] ENEMY_TYPES = { typeof(SkeletonController) };
    private Dictionary<Type, GameObject> enemyTypeToPrefab;

    [SerializeField]
    private GameObject[] enemyPrefabs = default;

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

        enemyTypeToPrefab = new Dictionary<Type, GameObject>(ENEMY_TYPES.Length);
        foreach (GameObject prefab in enemyPrefabs)
        {
            Type enemyType = prefab.GetComponent<Enemy>().GetType();
            if (enemyTypeToPrefab.ContainsKey(enemyType))
            {
                Debug.LogError($"Enemy type {enemyType} appears more than once among the enemy prefabs!");
                break;
            }

            enemyTypeToPrefab.Add(enemyType, prefab);
        }
    }

    public void SpawnEnemy(Type enemyType)
    {
        GameObject prefab = GetEnemyPrefab(enemyType);
        Vector3 spawnPos = ChoosePosForSpawningEnemy();
        Instantiate(prefab, spawnPos, Quaternion.identity, transform);
    }

    private Vector3 ChoosePosForSpawningEnemy()
    {
        HexCell[] edgeCells = HexGrid.Singleton.SpawnableEdgeCells;
        return edgeCells[Random.Range(0, edgeCells.Length)].transform.position;
    }

    public static GameObject GetEnemyPrefab(Type enemyType)
    {
        return SINGLETON.enemyTypeToPrefab[enemyType];
    }
}
