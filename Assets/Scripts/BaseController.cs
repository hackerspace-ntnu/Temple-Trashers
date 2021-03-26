using UnityEngine;
using System.Collections.Generic;
using UnityEngine.VFX;

public class BaseController : MonoBehaviour
{
    // Base Singleton
    public static BaseController Instance;

    // GameOverScreen
    [SerializeField]
    private GameObject GameOverScreen = null;

    //Explosion
    [SerializeField]
    private GameObject Explosion = null;

    [SerializeField]
    private Transform spawnPoint = null;

    // Death flag
    private bool dead = false;

    // Stored Resources
    public int crystals = 0;

    // Lightning arc
    public GameObject drainRay;
    private List<Ray> rays = new List<Ray>();


    void Awake()
    {
        // Makes sure there is only one base
        if ( Instance == null ) { Instance = this; }
        else
        {
            print("Tried to make two bases, removed the latter");
            Destroy(gameObject);
        }
    }

    private void Die(){
        if(dead == false)
        {
            //BIG EXPLOSION
            Instantiate(Explosion, this.transform.position, new Quaternion(0, 0, 0, 0), this.transform);
            // Creates the GUI "GameOverScreen"
            Instantiate(GameOverScreen);
        }
        dead = true;
    }
    public Transform SpawnPoint { get => spawnPoint; }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateController player = other.GetComponentInParent<PlayerStateController>();
        Loot loot = player.GetComponentInChildren<Loot>();
        if (loot != null)
        {
           loot.Absorb(this);

            // Add VFX
            if(GetIdVFX(player.transform) == -1) // Check that we have not added one already
            {
                GameObject ray = Instantiate(drainRay, transform.position, transform.rotation);
                ray.transform.SetParent(transform);

                Transform target = ray.GetComponentsInChildren<Transform>()[1];
                target.SetParent(loot.transform);
                target.localPosition = Vector3.zero;

                rays.Add(new Ray(ray.transform, target));
                loot.target = target;
            }
        }
    }
    private void OnTriggerExit(Collider other)
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
    
    public void RemoveRayVFX(Transform target, float delay)
    {
        int i = GetIdVFX(target);
        if(i >= 0)
        {
            Transform t = rays[i].target;
            Transform r = rays[i].ray;
            rays.RemoveAt(i);
            Destroy(r.gameObject, delay);
            Destroy(t.gameObject, delay);
        }
    }

    public void ArcLengthVFX(Transform target, float change)
    {
        int i = GetIdVFX(target);
        if(i >= 0)
        {
            Transform r = rays[i].ray;
            r.GetComponent<VisualEffect>().SetFloat("Length", change);
            if (change <= 0.1)
            {
                r.GetComponent<VisualEffect>().SendEvent("OnDie");
            }
        }
        
    }
    // Returns -1 if t does not contain loot, -2 if loot was not registered
    public int GetIdVFX(Transform t)
    {
        Transform target = t.GetComponentInChildren<Loot>().target;
        if(target != null)
        {
            for (int i = 0; i < rays.Count; i++)
            {
                if (rays[i].target == target)
                {
                    return i;
                }
            }
            return -2;
        }
        return -1;
    }
}

public struct Ray
{
    public Transform ray;
    public Transform target;

    public Ray(Transform Ray, Transform Target)
    {
        ray = Ray;
        target = Target;
    }
}

