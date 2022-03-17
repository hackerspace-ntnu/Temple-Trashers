using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;


public class LightningShootable : MonoBehaviour, TurretInterface
{
    [SerializeField]
    private VisualEffect sparksEffect;

    //The radius for the turret and all it's targets.
    public float lightningRadius = 4;

    //All targets in range of lightning turret's mesh collider hitbox
    public CollisionManager collisionTargets;

    //Damage per zap.
    public float damage = 10;

    //Lightning VFX
    public GameObject drainRay;

    //LighntingEffect duration
    public float effectDuration = 0.7f;

    //What layers the collider will check in, should be player and enemy layers.
    public LayerMask shockLayers;

    //Marked objects
    private List<Transform> zapTargets = new List<Transform>();

    //Making lightning spawn from top of turret
    [SerializeField]
    private Transform zapOrigin;

    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// Called through an animation event on the <c>ShootLightning.anim</c> lightning turret animation.
    /// </summary>
    /// 
    public void Shoot()
    {

        foreach (var target in collisionTargets.getColliders())
        {
            if (target && target.GetComponent<HealthLogic>()) 
            { 
                //Adds the first lightning vfx between Lightning Tower and first hit
                AddZap(target.transform, transform);
                //Starts recursion
                CheckZap(target.transform);
            }
        }
        
        foreach (var zap in zapTargets)
        {
            Vector3 diff = (zap.position - transform.position).normalized;
            zap.GetComponent<HealthLogic>().OnReceiveDamage(damage, new Vector3(diff.x, 2, diff.z), 5);
        }

        if (zapTargets.Count > 0)
        {
            audioSource.Play();
        }
        //Clear all marked objects.
        zapTargets.Clear();
    }

    //Marks soon-to-be zapped objects and adds a VFX.
    private void AddZap(Transform target, Transform previous)
    {
        if (zapTargets.Contains(target))
            return;

        if (previous == transform)
            previous = zapOrigin;

        LightningVFX(previous, target);
        zapTargets.Add(target);
        CheckZap(target);
    }

    //Checks for non-marked zappable objects.
    private void CheckZap(Transform target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, lightningRadius, shockLayers);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<HealthLogic>())
                AddZap(hitCollider.transform, target);
        }
    }

    //Lightning VFX
    private void LightningVFX(Transform previousTarget, Transform newTarget)
    {
        GameObject ray = Instantiate(drainRay, previousTarget.position, previousTarget.rotation, previousTarget);
        // 1 is the index of the first child (after the parent itself)
        Transform target = ray.GetComponentsInChildren<Transform>()[1];
        target.SetParent(newTarget);
        target.localPosition = Vector3.zero;

        //Destroying effect after duration:
        Destroy(ray, effectDuration);
        Destroy(target.gameObject, effectDuration);
    }

    public void StartSparks()
    {
        sparksEffect.SendEvent("OnPlay");
    }

    public void StopSparks()
    {
        sparksEffect.SendEvent("OnStop");
    }
}
