using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private static EnemyWaveManager SINGLETON;

    public TextAsset waveFile;

    [Tooltip("In seconds.")]
    public float durationBetweenEnemies = 3f;

    public EnemyManager enemyManager;

    #region State variables for debugging

    [ReadOnly, SerializeField]
    private bool hasWaveEnded;

    [ReadOnly, SerializeField]
    private bool readyForNextWave;

    [ReadOnly, SerializeField]
    private float timeUntilNextSpawn;

    [ReadOnly, SerializeField]
    private int currentWaveIndex;

    [ReadOnly, SerializeField]
    private int currentSubWaveIndex;

    [ReadOnly, SerializeField]
    private int currentSubWaveEnemyCounter;

    #endregion State variables for debugging

    private List<EnemyWave> waves;

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

        waves = WaveFileParser.ParseWaveFile(waveFile);
    }

    void Start()
    {
        hasWaveEnded = true;
        readyForNextWave = false;
        currentWaveIndex = -1;
        PrepareNextWave();
    }

    void FixedUpdate()
    {
        // TODO: should probably refactor this logic to use a state machine instead..?
        if (readyForNextWave && hasWaveEnded)
        {
            if (currentWaveIndex + 1 >= waves.Count)
            {
                // No more waves, so unmark as ready
                readyForNextWave = false;
                return;
            }

            // The game is ready for the next wave, so prepare it
            hasWaveEnded = false;
            readyForNextWave = false;
            currentWaveIndex++;
            PrepareNextWave();
        }

        if (ShouldSpawnNextEnemy())
            SpawnNextEnemy();
    }

    public static void ReadyForNextWave()
    {
        SINGLETON.readyForNextWave = true;
    }

    private void PrepareNextWave()
    {
        currentSubWaveIndex = -1;
        currentSubWaveEnemyCounter = 0;
        timeUntilNextSpawn = durationBetweenEnemies;
    }

    /// <summary>
    /// Should only be called once per FixedUpdate!
    /// </summary>
    private bool ShouldSpawnNextEnemy()
    {
        if (currentWaveIndex >= waves.Count)
            hasWaveEnded = true;
        if (hasWaveEnded)
            return false;

        // TODO: this should ideally be moved to a separate function,
        // as one would not expect this function to alter the state based on the function's name
        timeUntilNextSpawn -= Time.fixedDeltaTime;
        if (timeUntilNextSpawn <= 0)
        {
            timeUntilNextSpawn = durationBetweenEnemies;
            return true;
        }

        return false;
    }

    private void SpawnNextEnemy()
    {
        EnemyWave.SubWave subWave = PrepareSubWave();
        if (subWave == null)
        {
            // Reached the end of the sub-wave(s), and therefore also the current wave
            hasWaveEnded = true;
            return;
        }

        if (subWave.IsPause)
        {
            timeUntilNextSpawn = subWave.pauseDuration;
            return;
        }

        enemyManager.SpawnEnemy(subWave.enemyType);
        currentSubWaveEnemyCounter++;
    }

    private EnemyWave.SubWave PrepareSubWave()
    {
        if (currentSubWaveIndex < 0)
            currentSubWaveIndex = 0;

        // `subWave` should never be `null`, as `currentSubWaveIndex` should always reflect the current, valid sub-wave
        EnemyWave.SubWave subWave = waves[currentWaveIndex].GetSubWave(currentSubWaveIndex);
        if (subWave.IsPause)
        {
            // If the current sub-wave is a pause sub-wave, that means it's already over,
            // so return the next one (if it exists)
            return GoToNextSubWave(currentWaveIndex);
        }

        if (currentSubWaveEnemyCounter < subWave.number)
        {
            // Can continue with sub-wave, so return it
            return subWave;
        } else
        {
            // Sub-wave is over, so return the next one (if it exists)
            return GoToNextSubWave(currentWaveIndex);
        }
    }

    private EnemyWave.SubWave GoToNextSubWave(int waveIndex)
    {
        // Check next sub-wave:
        EnemyWave.SubWave subWave = waves[waveIndex].GetSubWave(currentSubWaveIndex + 1);
        if (subWave != null)
        {
            // Update sub-wave index and reset enemy counter
            currentSubWaveIndex++;
            currentSubWaveEnemyCounter = 0;
        }

        return subWave;
    }
}
