using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootLightning : MonoBehaviour
{
    public float LightningRadius;
    public float damage;
    //public int maxTargets;
    private List<GameObject> ZappTargets = new List<GameObject>();
    public void shoot()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, LightningRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<HealthLogic>())
            {
                addZap(hitCollider.gameObject);
                checkZap(hitCollider.gameObject);
            }
        }
        foreach (var zap in ZappTargets)
        {
            zap.GetComponent<HealthLogic>().dealDamage(damage);
        }
        ZappTargets.Clear();
    }




    private void addZap(GameObject target)
    {
        if (target.GetComponent<HealthLogic>() && !ZappTargets.Contains(target))
        {
            ZappTargets.Add(target);
        }
    }

    private void checkZap(GameObject target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, LightningRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (!ZappTargets.Contains(hitCollider.gameObject) && hitCollider.GetComponent<HealthLogic>())
            {
                addZap(hitCollider.gameObject);
                checkZap(hitCollider.gameObject);
            }
        }
    }
}
