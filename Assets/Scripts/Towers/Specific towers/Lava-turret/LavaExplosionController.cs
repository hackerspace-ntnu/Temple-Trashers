﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaExplosionController : MonoBehaviour
{
    [SerializeField]
    float damage = 0f;

    [SerializeField]
    float knockBackForce = 0f;

    [SerializeField]
    float delay = 0f;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float radius;
    public void Explode()
    {
        StartCoroutine(WaitAndExplode());
    }
    private IEnumerator WaitAndExplode()
    {
        yield return new WaitForSeconds(delay);
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, mask);
        foreach (var hit in hits)
        {
            hit.GetComponent<HealthLogic>()?.OnReceiveDamage(damage, (hit.transform.position + Vector3.up/2f - transform.position).normalized, knockBackForce);
        }
        var baseTransform = BaseController.Singleton.transform;

        if ((baseTransform.position-transform.position).magnitude < radius)
        {
            baseTransform.GetComponent<HealthLogic>()?.OnReceiveDamage(damage, Vector3.up, knockBackForce);

        }

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
}