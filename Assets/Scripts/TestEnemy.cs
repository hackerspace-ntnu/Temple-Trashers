using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    int dmg = 200;

    void OnCollisionEnter()
    {
        Debug.Log("HIT");
        BaseController.Instance.DealDamage(dmg);
    }
}