using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShootable : MonoBehaviour
{

    //*********************************************************************************************************
    //This script should be called by lightning tower using the shoot() function from the animation controller.
    //*********************************************************************************************************


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
    private List<GameObject> zappTargets = new List<GameObject>();


    public void Shoot()
    {
        CheckZap(this.gameObject);

        foreach (var zap in zappTargets)
        {
            zap.GetComponent<HealthLogic>().DealDamage(damage);
        }
        //Clear all marked objects.
        zappTargets.Clear();
    }


    //Marks soon-to-be zapped objects and adds a VFX.
    private void AddZap(GameObject target)
    {
        if (target.GetComponent<HealthLogic>() && !zappTargets.Contains(target))
        {
            GameObject previousTarget = null;
            if (zappTargets.Count > 0)
            {
                previousTarget = zappTargets[zappTargets.Count - 1];
            }
            else
            {
                previousTarget = this.gameObject;
            }
            LightningVFX(previousTarget.transform, target.transform);
            zappTargets.Add(target);
            CheckZap(target);
        }
    }

    //Checks for non-marked zappable objects.
    private void CheckZap(GameObject target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, lightningRadius, shockLayers);
        foreach (var hitCollider in hitColliders)
        {
            if (!zappTargets.Contains(hitCollider.gameObject) && hitCollider.GetComponent<HealthLogic>())
            {
                AddZap(hitCollider.gameObject);
            }
        }
    }


    //Lightning VFX
    private void LightningVFX(Transform previousTarget, Transform newTarget)
    {
        GameObject ray = Instantiate(drainRay, previousTarget.position, previousTarget.rotation);
        ray.transform.SetParent(previousTarget.transform);
        //component[1] so we get the first child and not the parent itself.
        Transform target = ray.GetComponentsInChildren<Transform>()[1];
        target.SetParent(newTarget);
        target.localPosition = Vector3.zero;

        //Destroying effect after duration:
        Transform r = ray.transform;
        Destroy(r.gameObject, effectDuration);
    }
}
