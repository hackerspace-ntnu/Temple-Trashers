﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLogic : MonoBehaviour
{
    //Loot to be instantiated
    public GameObject lootPrefab;

    //Extra height to be added when instantiating loot.
    public float spawnHeight = 2f;

    private void Start()
    {
        //Using delegates to add loot drop function to death
        GetComponent<HealthLogic>().onDeath += DropLoot;
    }

    private void DropLoot(DamageInfo dmg)
    {
        //... instantiating loot at position of enemy upon death. Need the new vector for the loot to spawn over the map.
        Vector3 spawnPos = transform.position + new Vector3(0, spawnHeight, 0);
        Instantiate(lootPrefab, spawnPos, Quaternion.identity);
    }
}
