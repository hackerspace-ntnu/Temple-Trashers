using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootLightning : MonoBehaviour
{

    //*********************************************************************************************************
    //This script should be called by lightning tower using the shoot() function from the animation controller.
    //*********************************************************************************************************


    //The radius for the turret and all it's targets.
    public float LightningRadius = 4;
    //Damage per zap.
    public float damage = 10;
    //Lightning VFX
    public GameObject drainRay;
    //LighntingEffect duration
    public float effectDuration = 0.3f;
    //What layers the collider will check in, should be player and enemy layers.
    public LayerMask ShockLayers;


    //Marked objects
    private List<GameObject> ZappTargets = new List<GameObject>();


    public void shoot()
    {
        checkZap(this.gameObject);

        foreach (var zap in ZappTargets)
        {
            zap.GetComponent<HealthLogic>().DealDamage(damage);
        }
        //Clear all marked objects.
        ZappTargets.Clear();
    }


    //Marks soon-to-be zapped objects and adds a VFX.
    private void addZap(GameObject target)
    {
        if (target.GetComponent<HealthLogic>() && !ZappTargets.Contains(target))
        {
            GameObject previousTarget = null;
            if (ZappTargets.Count > 0)
            {
                previousTarget = ZappTargets[ZappTargets.Count - 1];
            }
            else
            {
                previousTarget = this.gameObject;
            }
            lightningVFX(previousTarget.transform, target.transform);
            ZappTargets.Add(target);
            checkZap(target);
        }
    }

    //Checks for non-marked zappable objects.
    private void checkZap(GameObject target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, LightningRadius, ShockLayers);
        foreach (var hitCollider in hitColliders)
        {
            if (!ZappTargets.Contains(hitCollider.gameObject) && hitCollider.GetComponent<HealthLogic>())
            {
                addZap(hitCollider.gameObject);
            }
        }
    }


    //Lightning VFX
    private void lightningVFX(Transform previousTarget, Transform newTarget)
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
