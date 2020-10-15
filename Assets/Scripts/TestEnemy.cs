using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    int dmg = 1000;

    void OnCollisionEnter()
    {
        Debug.Log("HIT");
        BaseController.Instance.DealDamage(dmg);
    }
}
