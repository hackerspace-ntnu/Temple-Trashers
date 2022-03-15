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
        if (other.GetComponent<HealthLogic>())
            other.gameObject.GetComponent<HealthLogic>().OnReceiveDamage(damage, (transform.right + Vector3.up*0.5f).normalized, 10f);

        Destroy(gameObject);
    }
}
