using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Singleton related logic
    private static InventoryManager _instance;
    public static InventoryManager Instance { get { return _instance; } }

    //Total amount of resources.
    private int resourceAmount = 0;


    //Get-er
    public int GetResourceAmount()
    {
        return resourceAmount;
    }

    //Adders
    public void AddResource(int amount)
    {
        if (amount > 0)
        {
            resourceAmount += amount;
        }
    }
    public void AddResource()
    {
        resourceAmount ++;
    }

    //Subtracters
    public bool SubtractResource(int amount)
    {
        if (amount <= resourceAmount) {
            resourceAmount -= amount;
            return true;
        }
        return false;
    }
    public bool SubtractResource()
    {
        if (resourceAmount >= 1)
        {
            resourceAmount --;
            return true;
        }
        return false;
    }





    //Singleton-related function
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
