using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningShootable : MonoBehaviour, TurretInterface
{
    //The radius for the turret and all it's targets.
    public float lightningRadius = 4;

    //Damage per zap.
    public float damage = 10;

    //Lightning VFX
    public GameObject drainRay;

    //LighntingEffect duration
    public float effectDuration = 0.3f;

    //What layers the collider will check in, should be player and enemy layers.
    public LayerMask shockLayers;

    //Marked objects
    private List<Transform> zappTargets = new List<Transform>();

    //Making lightning spawn from top of turret
    [SerializeField]
    private Transform zapOrigin;

    /// <summary>
    /// Called through an animation event on `Lightning.anim`.
    /// </summary>
    public void Shoot()
    {
        CheckZap(transform);

        foreach (var zap in zappTargets)
            zap.GetComponent<HealthLogic>().DealDamage(damage);

        //Clear all marked objects.
        zappTargets.Clear();
    }

    //Marks soon-to-be zapped objects and adds a VFX.
    private void AddZap(Transform target)
    {
        if (zappTargets.Contains(target))
            return;

        Transform previousTarget = zappTargets.LastOrDefault() ?? zapOrigin;
        LightningVFX(previousTarget, target);
        zappTargets.Add(target);
        CheckZap(target);
    }

    //Checks for non-marked zappable objects.
    private void CheckZap(Transform target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, lightningRadius, shockLayers);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<HealthLogic>())
                AddZap(hitCollider.transform);
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
    }
}
