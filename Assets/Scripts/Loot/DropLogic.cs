using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLogic : MonoBehaviour
{
    //Loot to be instantiated
    public GameObject lootPrefab;
    //Extra height to be added when instantiating loot.
    public float spawnHeight = 2;


    private void Start()
    {
        //Using delegates to add loot drop function to death
        GetComponent<HealthLogic>().OnDeath += DropLoot;    
    }

    private void DropLoot()
    {
        //... instantiating loot at position of enemy upon death. Need the new vector for the loot to spawn over the map.
        Instantiate(lootPrefab, transform.position + new Vector3(0, spawnHeight, 0), Quaternion.identity);

    }
}
