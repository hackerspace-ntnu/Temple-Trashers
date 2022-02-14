using UnityEngine;
using System.Collections;

public class EndlessMode : MonoBehaviour
{
    // Public variables
    public HexGrid hexGrid;
    public Enemy[] enemies;

    [Header("Enemies: f(x) = ax^k")]
    [Range(1,10)]
    [Tooltip("The linear factor")]
    public float a = 3;
    [Range(1,3)]
    [Tooltip("The exponential factor")]
    public float k = 1.1f;

    [Header("Wave Interval")]
    public float waveInterval = 15f;

    // Private variables
    private int wave = 1;

    private void Start()
    {   // Ensure values are assigned
        if(enemies.Length == 0)
            Debug.LogError("Enemies are not assigned");
        if (hexGrid == null)
            hexGrid = GameObject.Find("Terrain").GetComponent<HexGrid>();
    }

    private float t = 0;
    private void Update()
    {
        t += Time.deltaTime;

        if (t >= waveInterval)
        {
            // Start the coroutine
            Wave();
            t = 0;
        }
    }

    void Wave()
    {
        // Calculate the amount of enemies to spawn
        int spawnNum = Mathf.RoundToInt(a * Mathf.Pow(wave, k));

        for(int i = 0; i < spawnNum; i++)
        {
            SpawnEnemy();
        }
        wave++;
    }
    /// <summary>
    /// Spawn a random enemy at a random edgecell
    /// </summary>
    void SpawnEnemy()
    {
        GameObject prefab = enemies[Random.Range(0, enemies.Length)].gameObject;
        Vector3 spawnPos = hexGrid.edgeCells[Random.Range(0, hexGrid.edgeCells.Length - 1)].transform.position;
        prefab = Instantiate(prefab, spawnPos, Quaternion.identity, transform);
    } 
}
