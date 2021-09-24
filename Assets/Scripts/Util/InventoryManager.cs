using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager SINGLETON;
    public static InventoryManager Singleton => SINGLETON;

    public ResourceUI ui;

    //Total amount of resources.
    private int resourceAmount = 100;

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
            ui.UpdateUI();
        }
    }
    public void AddResource()
    {
        resourceAmount ++;
        ui.UpdateUI();
    }

    //Subtracters
    public bool SubtractResource(int amount)
    {
        if (amount <= resourceAmount) {
            resourceAmount -= amount;
            ui.UpdateUI();
            return true;
        }
        return false;
    }
    public bool SubtractResource()
    {
        if (resourceAmount >= 1)
        {
            resourceAmount --;
            ui.UpdateUI();
            return true;
        }
        return false;
    }

    void Awake()
    {
        #region Singleton boilerplate

        if (SINGLETON != null)
        {
            if (SINGLETON != this)
            {
                Debug.LogWarning($"There's more than one {SINGLETON.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        SINGLETON = this;

        #endregion Singleton boilerplate
    }
}
