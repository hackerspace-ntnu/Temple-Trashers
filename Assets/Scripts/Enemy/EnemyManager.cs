using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public HexGrid hexGrid;

    //Array of enemy objects
    public GameObject[] enemyObject;

    //Time it takes to spawn enemies
    [Space(3)]
    public float respawnTime = 1;
    private float theCountdown = 1;

    [Header("Enemy Variables")]
    public float speed = 5f;

    void Update()
    {
        // timer to spawn the next goodie Object
        theCountdown -= Time.deltaTime;
        if (theCountdown <= 0)
        {
            SpawnEnemy();
            theCountdown = respawnTime;
        }
    }
    
    int enemies = 0;
    void SpawnEnemy()
    {
        Transform trans = hexGrid.edgeCells[Random.Range(0, hexGrid.edgeCells.Length)].transform;

        // Choose a new enemy to spawn from the array (note I specifically call it a 'prefab' to avoid confusing myself!)
        GameObject enemyPrefab = enemyObject[Random.Range(0, enemyObject.Length)];

        // Pass along the enemy settings
        enemyPrefab.GetComponent<Enemy>().speed = speed;
        // Creates the random object at the random 3D position.
        GameObject enemy = Instantiate(enemyPrefab, trans.position, Quaternion.identity);
        enemy.transform.SetParent(this.transform);

        enemies++;
    }
}
