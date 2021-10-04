using UnityEngine;
using System.Collections.Generic;
using UnityEngine.VFX;

public class BaseController : MonoBehaviour
{
    public static BaseController Singleton { get; private set; }

    // GameOverScreen
    [SerializeField]
    private GameObject gameOverScreen;

    //Explosion
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private Transform spawnPoint;

    // Death flag
    private bool dead = false;

    // Stored Resources
    public int crystals = 0;

    // Lightning arc
    public GameObject drainRay;
    private List<Ray> rays = new List<Ray>();

    public Transform SpawnPoint => spawnPoint;

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

        GetComponent<HealthLogic>().onDeath += Die;
    }

    void OnDestroy()
    {
        GetComponent<HealthLogic>().onDeath -= Die;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStateController player = other.GetComponentInParent<PlayerStateController>();
        Loot loot = player.GetComponentInChildren<Loot>();
        if (loot != null)
        {
            loot.Absorb(this);

            // Add VFX
            if (GetIdVFX(player.transform) == -1) // Check that we have not added one already
            {
                Transform ray = Instantiate(drainRay, transform.position, transform.rotation).transform;
                ray.SetParent(transform);

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
        if (loot != null)
        {
            // Stop the absorbtion
            loot.CancelAbsorb();
            // Remove VFX
            RemoveRayVFX(player.transform, 0f);
        }
    }

    private void Die()
    {
        if (!dead)
        {
            //BIG EXPLOSION
            Instantiate(explosion, transform.position, Quaternion.identity, transform);
            // Creates the GUI "GameOverScreen"
            Instantiate(gameOverScreen);
        }

        dead = true;
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
            r.GetComponent<VisualEffect>().SetFloat("Length", change);
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
}
