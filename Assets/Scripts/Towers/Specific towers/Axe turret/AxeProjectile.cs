using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxeProjectile : MonoBehaviour
{
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthLogic>() is HealthLogic healthLogic)
        {
            Vector3 knockBackDir = (transform.right + Vector3.up * 0.5f).normalized;
            healthLogic.OnReceiveDamage(damage, knockBackDir, 10f);
        }
        GetComponentInParent<AxeTowerAnimationController>().Hit();
        Destroy(gameObject);
    }
}
