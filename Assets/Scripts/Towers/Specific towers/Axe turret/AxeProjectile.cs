using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxeProjectile : MonoBehaviour
{
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthLogic>())
            other.gameObject.GetComponent<HealthLogic>().DealDamage(damage);

        Destroy(gameObject);
    }
}
