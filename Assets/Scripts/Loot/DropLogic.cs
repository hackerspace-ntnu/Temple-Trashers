using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLogic : MonoBehaviour
{
    private int randomNumber;
    public GameObject currency; //put loot here

    private void Start()
    {
        GetComponent<HealthLogic>().OnDeath += DropLoot;    
    }

    private void DropLoot()
    {
        randomNumber = Random.Range(0, 100); //Random number between 0-100

        if (randomNumber >= 0 && randomNumber <= 80) //80% probability for:
        {
            Instantiate(currency, transform.position + new Vector3(0, 4, 0), Quaternion.identity); //... instantiating currency at position of enemy upon death. Need the new vector for the loot to spawn over the map.
        }
    }
}
