using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;

public class BaseController : MonoBehaviour
{
    public static BaseController Singleton { get; private set; }

    // GameOverScreen
    [SerializeField]
    private GameObject gameOverScreen;

    //Death Effects
    [SerializeField]
    private GameObject destroyedBase;
    private Animator anim;

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
        anim = GetComponent<Animator>();
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
            // Start overloading the crystal
            anim.SetBool("death", true);

            // Prepare the explosion
            StartCoroutine("Explode");

            // Focus the camera
            
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

    IEnumerator Explode()
    {
        // Get a list of all transforms (lighning targets)
        Transform[] transforms = GameObject.FindObjectsOfType<Transform>();
        Debug.Log(transforms.Length);
        // Create lightning as the crystal charges
        for (float t = 4f; t >= 0; t -= 0.2f)
        {
            int i = Random.Range(0, transforms.Length);

            Transform ray = Instantiate(drainRay, transform.position + new Vector3(Random.Range(-1, 1), 5f, Random.Range(-1, 1)), transform.rotation).transform;
            ray.SetParent(transform);

            // 1 is the index of the first child (after the parent itself)
            Transform target = ray.GetComponentsInChildren<Transform>()[1];
            target.SetParent(transforms[i]);
            target.localPosition = Vector3.zero;

            // Ensure correct camera focus
            Camera.main.GetComponent<CameraFocusController>().Focus(transform);

            yield return new WaitForSeconds(0.2f);
        }

        // Replace the base with a rigidbody based one
        GameObject deadBase = Instantiate(destroyedBase, transform.position, Quaternion.identity);

        // Add an explosion force on the base
        foreach(Rigidbody rb in deadBase.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(new Vector3(Random.Range(-250f, 250f), Random.Range(500f, 800f), Random.Range(-250f, 250f)));
        }

        // Switch camera focus to the new base
        Camera.main.GetComponent<CameraFocusController>().Focus(destroyedBase.transform);

        // Creates the GUI "GameOverScreen"
        Instantiate(gameOverScreen);

        // Clean up
        Destroy(gameObject);
    }
}
