using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HurtBox : MonoBehaviour
{
    [SerializeField]
    public string[] tagsToDamage;
    [SerializeField]
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagsToDamage.Length; i++) {
            if (other.tag == tagsToDamage[i]) {
                HealthLogic playerHealth = other.GetComponent<HealthLogic>();
                playerHealth.DealDamage(damage);
            }
        }
    }
}

