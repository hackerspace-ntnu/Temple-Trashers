using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    public Text resourceAmount;

    private InventoryManager inventory;

    void Start()
    {
        inventory = InventoryManager.Singleton;
        UpdateUI();
    }

    public void UpdateUI()
    {
        resourceAmount.text = inventory.GetResourceAmount().ToString();
    }
}
