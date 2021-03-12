using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,10f);
    }
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthLogic>())
        {
            other.gameObject.GetComponent<HealthLogic>().DealDamage(damage);

        }
        Destroy(gameObject);
    }
}
