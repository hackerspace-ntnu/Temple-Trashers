using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderEnemy : Enemy
{
    void Start()
    {
        base.Start();
        GetComponent<HealthLogic>().onDeath += () => Destroy(gameObject);

    }
}
