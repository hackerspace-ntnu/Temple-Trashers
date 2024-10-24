﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LavaExplosionController : MonoBehaviour
{
    [SerializeField]
    private float damage = 0f;

    [SerializeField]
    private float knockBackForce = 0f;

    [SerializeField]
    private float delay = 0f;

    [SerializeField]
    private LayerMask mask = default;

    [SerializeField]
    private float radius = default;

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
            float dist = (hit.transform.position - transform.position).magnitude;
            hit.GetComponent<HealthLogic>()?.OnReceiveDamage(
                this,
                damage,
                (hit.transform.position + Vector3.up / 2f - transform.position).normalized,
                knockBackForce * Mathf.Clamp(1 - dist / (2 * radius), 0.5f, 1f)
            );
        }

        if (BaseController.Singleton)
        {
            Transform baseTransform = BaseController.Singleton.transform;

            if (transform.position.DistanceLessThan(radius, baseTransform.position))
                baseTransform.GetComponent<HealthLogic>()?.OnReceiveDamage(this, damage, Vector3.up, knockBackForce);
        }

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
