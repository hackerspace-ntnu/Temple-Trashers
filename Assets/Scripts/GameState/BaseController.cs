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
    private GameObject gameOverScreen;

    // Rigidbody base
    [SerializeField]
    private GameObject destroyedBase;

    // Base Animator
    private Animator anim;

    // Particle Effect
    [SerializeField]
    private GameObject deathParticles;

    [SerializeField]
    private Transform spawnPoint;

    private HealthLogic healthController;

    // Death flag
    private bool dead = false;

    // Stored Resources
    public int crystals = 0;

    // Lightning arc
    public GameObject drainRay;
    private List<Ray> rays = new List<Ray>();

    public int explosionLightningCount = 20;
    public float explosionLightningSpawnDelay = 0.2f;

    public Transform SpawnPoint => spawnPoint;

    // Crystal Transform
    [SerializeField]
    private Transform mainCrystal;

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
        healthController.onDeath += Die;
        anim = GetComponent<Animator>();

        if (mainCrystal == null)
            Debug.LogError("Main Crystal not set.");
    }

    void OnDestroy()
    {
        healthController.onDeath -= Die;
    }

    private void Die(DamageInfo dmg)
    {
        if (!dead)
        {
            // Disable spawning of enemies
            GameObject.Find("EnemyWaveHandler").GetComponent<EnemyWaveManager>().enabled = false;

            // Start overloading the crystal
            anim.SetBool(deathAnimatorParam, true);

            // Prepare the explosion
            StartCoroutine(nameof(Explode));

            // Start Distortions
            //distortionField.enabled = true;
        }

        dead = true;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStateController player = other.GetComponentInParent<PlayerStateController>();
        Loot loot = player?.GetComponentInChildren<Loot>();
        if (loot)
        {
            loot.Absorb(this);

            // Add VFX
            if (GetIdVFX(player.transform) == -1) // Check that we have not added one already
            {
                Transform ray = Instantiate(drainRay, mainCrystal.transform.position, mainCrystal.transform.rotation).transform;
                ray.SetParent(mainCrystal);

                // 1 is the index of the first child (after the parent itself)
                Transform target = ray.GetComponentsInChildren<Transform>()[1];
                target.SetParent(loot.transform);
                target.localPosition = Vector3.zero;

                rays.Add(new Ray(ray, target));
                loot.target = target;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerStateController player = other.GetComponentInParent<PlayerStateController>();
        Loot loot = player.GetComponentInChildren<Loot>();
        if (loot)
        {
            // Stop the absorbtion
            loot.CancelAbsorb();
            // Remove VFX
            RemoveRayVFX(player.transform, 0f);
        }
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
    /// Returns -1 if `t` does not contain loot, -2 if loot was not registered
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

    IEnumerator Explode()
    {
        // Get a list of all transforms (lighning targets)
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

        // Switch camera focus to the new base
        Camera.main.GetComponent<CameraFocusController>().Focus(destroyedBase.transform);

        // Creates the GUI "GameOverScreen"
        Instantiate(gameOverScreen);

        // Add particle system
        Instantiate(deathParticles, transform.position + new Vector3(0, 3, 0), deathParticles.transform.rotation);

        // Clean up
        Destroy(gameObject);
    }
}
