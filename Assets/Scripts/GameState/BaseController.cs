using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;


public class BaseController : MonoBehaviour
{
    public static BaseController Singleton { get; private set; }

    [Header("Death components")]
    // GameOverScreen
    [SerializeField]
    private GameObject gameOverScreen = default;

    // Rigidbody base
    [SerializeField]
    private GameObject destroyedBase = default;

    // Particle Effect
    [SerializeField]
    private GameObject deathParticles = default;

    [SerializeField]
    private Transform spawnPoint = default;

    // Crystal Transform
    [SerializeField]
    private Transform mainCrystal = default;

    [SerializeField]
    private AudioSource audioSource = default;

    // Death flag
    private bool dead = false;

    // Stored Resources
    public int crystals = 0;

    [Header("WaveExplosion parameters")]
    [SerializeField]
    private float timeToWaveExplode = 380;

    [ReadOnly, SerializeField]
    private float waveExplosionTimer = 0;

    [SerializeField]
    private float waveTimeReward = 20;

    [SerializeField]
    private float waveTimePunishment = 20;

    [Header("Base zapping parameters")]
    // Lightning arc
    [SerializeField]
    private GameObject drainRay = default;

    [SerializeField]
    private GameObject enemyZap = default;

    [SerializeField]
    private float enemyZapDamage = default;

    [SerializeField]
    private float enemyZapKnockBackForce = default;

    [SerializeField]
    private float enemyZapKnockBack_dirWeightAwayFromUp = 0.1f;

    private List<Ray> rays = new List<Ray>();

    [SerializeField]
    private int explosionLightningCount = 20;

    [SerializeField]
    private float explosionLightningSpawnDelay = 0.2f;

    private HealthLogic healthController;

    public HealthLogic HealthController => healthController;

    private Animator anim;

    [SerializeField]
    private Material matCrystal = default;

    public bool isGameOver = false;

    [Header("WaveExplosion parameters")]
    [SerializeField]
    private AudioClip audioCrystalAbsorbtion = default;

    [SerializeField]
    private AudioClip audioBaseZap = default;

    [SerializeField]
    private AudioClip audioBaseCharge = default;

    // The gamemanager object that organizes enemies and player spawning
    private EndlessMode gameManager = default;

    public Transform SpawnPoint => spawnPoint;

    private static readonly int deathAnimatorParam = Animator.StringToHash("death");
    private static readonly int lengthShaderProperty = Shader.PropertyToID("Length");

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

        healthController = GetComponent<HealthLogic>();
        healthController.onDamage += OnReceiveDamage;
        healthController.onDeath += Die;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (mainCrystal == null)
            Debug.LogError("Main Crystal not set.");

        isGameOver = false;
    }

    void Start()
    {
        gameManager = EndlessMode.Singleton;
        InvokeRepeating("WaveExplosionCounter",3f, 1f);
        matCrystal.SetFloat("Charge_Percent", 0);
        
    }

    void OnDestroy()
    {
        healthController.onDamage -= OnReceiveDamage;
        healthController.onDeath -= Die;
    }

    private void OnReceiveDamage(DamageInfo damageInfo)
    {
        if (damageInfo.FromSource is Enemy enemy)
            ZapAttackingEnemy(enemy);
    }

    private void ZapAttackingEnemy(Enemy enemy)
    {
        Transform ray = Instantiate(enemyZap, mainCrystal.transform.position, mainCrystal.transform.rotation, mainCrystal).transform;

        // TODO: refactor this, to reduce code duplication with `OnTriggerEnter()` below
        // 1 is the index of the first child (after the parent itself)
        Transform rayTarget = ray.GetComponentsInChildren<Transform>()[1];
        rayTarget.SetParent(enemy.transform);
        // Set the ray target's position to the center of the enemy
        rayTarget.position = enemy.GetComponent<Collider>().bounds.center;

        StartCoroutine(ZapSound());

        Destroy(ray.gameObject, 0.25f);

        HealthLogic enemyHealthLogic = enemy.GetComponent<HealthLogic>();
        Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
        Vector3 knockBackDir = (Vector3.up + enemyZapKnockBack_dirWeightAwayFromUp * dirToEnemy).normalized;
        enemyHealthLogic.OnReceiveDamage(this, enemyZapDamage, knockBackDir, enemyZapKnockBackForce);
    }

    private void Die(DamageInfo damageInfo)
    {
        if (dead)
            return;

        if (damageInfo.FromSource is PlayerSpecificManager)
        {
            SteamManager.Singleton.SetAchievement("ACH_MUTUAL_DESTRUCTION");
        }

        // Disable spawning of enemies
        gameManager.enabled = false;

        // Start overloading the crystal
        anim.SetBool(deathAnimatorParam, true);

        // Prepare the explosion
        StartCoroutine(nameof(Explode));

        dead = true;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStateController player = other.GetComponentInParent<PlayerStateController>();
        Loot loot = player?.GetComponentInChildren<Loot>();
        if (!loot)
            return;

        loot.Absorb();
        audioSource.Play();

        // Add VFX
        //if (GetIdVFX(player.transform) == -1) // Check that we have not added one already
        //{
            Transform ray = Instantiate(drainRay, mainCrystal.transform.position, mainCrystal.transform.rotation).transform;
            ray.SetParent(mainCrystal);

            // TODO: refactor this, to reduce code duplication with `ZapAttackingEnemy()` above
            // 1 is the index of the first child (after the parent itself)
            Transform target = ray.GetComponentsInChildren<Transform>()[1];
            target.SetParent(loot.transform);
            target.localPosition = Vector3.zero;

            rays.Add(new Ray(ray, target));
            loot.target = target;
        //}
    }

    void OnTriggerExit(Collider other)
    {
        PlayerStateController player = other.GetComponentInParent<PlayerStateController>();
        if (!player)
            return;

        Loot loot = player.GetComponentInChildren<Loot>();
        if (!loot)
            return;

        // Stop the absorbtion
        loot.CancelAbsorb();
        // Remove VFX
        RemoveRayVFX(player.transform, 0f);
    }

    public void RemoveRayVFX(Transform target, float delay)
    {
        int i = GetIdVFX(target);
        if (i >= 0)
        {
            Ray ray = rays[i];
            rays.RemoveAt(i);
            Destroy(ray.ray.gameObject, delay);
            Destroy(ray.target.gameObject, delay);
        }
    }

    public void ArcLengthVFX(Transform target, float change)
    {
        int i = GetIdVFX(target);
        if (i >= 0)
        {
            Transform r = rays[i].ray;
            r.GetComponent<VisualEffect>().SetFloat(lengthShaderProperty, change);
            if (change <= 0.1f)
                r.GetComponent<VisualEffect>().SendEvent("OnDie");
        }
    }

    /// <summary>
    /// Returns -1 if <c>t</c> does not contain loot, -2 if loot was not registered
    /// </summary>
    public int GetIdVFX(Transform t)
    {
        Transform target = t.GetComponentInChildren<Loot>().target;
        if (target == null)
            return -1;

        for (int i = 0; i < rays.Count; i++)
        {
            if (rays[i].target == target)
                return i;
        }

        return -2;
    }

    public void OnPlayerDeath()
    {
        waveExplosionTimer = waveExplosionTimer - waveTimePunishment < 0 ? 0 : waveExplosionTimer - waveTimePunishment;
    }

    public void OnCrystalCollected()
    {
        waveExplosionTimer += waveTimeReward;
    }

    //Called in start and then invoked every second
    private void WaveExplosionCounter()
    {
        waveExplosionTimer++;
        matCrystal.SetFloat("Charge_Percent",waveExplosionTimer/timeToWaveExplode);
        if (!(waveExplosionTimer >= timeToWaveExplode)) { return; }
        StartCoroutine(ZapAllEnemies());
        waveExplosionTimer = 0;
        matCrystal.SetFloat("Charge_Percent", 0);

    }

    private IEnumerator SlowMo()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private IEnumerator ZapSound()
    {
        audioSource.clip = audioBaseZap;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = audioCrystalAbsorbtion;
    }

    private IEnumerator ZapAllEnemies()
    {
        audioSource.clip = audioBaseCharge;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        StartCoroutine(SlowMo());
        audioSource.clip = audioCrystalAbsorbtion;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            ZapAttackingEnemy(enemy.GetComponent<Enemy>());
        }
        
    }

    IEnumerator Explode()
    {
        // Get a list of all transforms (lightning targets)
        Transform[] transforms = FindObjectsOfType<Transform>();

        // Create lightning as the crystal charges
        for (int i = 0; i <= explosionLightningCount; i++)
        {
            Vector3 rayPos = transform.position + new Vector3(Random.Range(-1, 1), 5f, Random.Range(-1, 1));
            Transform ray = Instantiate(drainRay, rayPos, transform.rotation, transform).transform;

            // 1 is the index of the first child (after the parent itself)
            Transform target = ray.GetComponentsInChildren<Transform>()[1];
            Transform randomLightningTarget = transforms[Random.Range(0, transforms.Length)];
            target.SetParent(randomLightningTarget);
            target.localPosition = Vector3.zero;

            // Ensure correct camera focus
            Camera.main.GetComponent<CameraFocusController>().Focus(transform);

            yield return new WaitForSeconds(explosionLightningSpawnDelay);
        }

        // Replace the base with a rigidbody based one
        GameObject deadBase = Instantiate(destroyedBase, transform.position, Quaternion.identity);

        // Add an explosion force on the base
        foreach (Rigidbody rb in deadBase.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(new Vector3(Random.Range(-250f, 250f), Random.Range(500f, 800f), Random.Range(-250f, 250f)));
        }

        //Make every tower selfdestruct
        RepairController[] repairControllers = FindObjectsOfType<RepairController>();
        foreach (RepairAnimationController rp in repairControllers)
        {
            rp.Explode();
        }

        // Switch camera focus to the new base
        Camera.main.GetComponent<CameraFocusController>().Focus(deadBase.transform);

        // Creates the GUI "GameOverScreen"
        Instantiate(gameOverScreen);

        // Add particle system
        Instantiate(deathParticles, transform.position + new Vector3(0, 3, 0), deathParticles.transform.rotation);

        //Mark state as Game over
        isGameOver = true;

        //Achievement triggers
        if (crystals >= 20) { SteamManager.Singleton.SetAchievement("ACH_HOARDER"); }

        // Clean up
        Destroy(gameObject);
    }
}
