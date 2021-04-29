using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    public Text resourceAmount = null;

    private InventoryManager inventory;

    void Start()
    {
        inventory = InventoryManager.Instance;
    }

    void Update()
    {
        resourceAmount.text = inventory.GetResourceAmount().ToString();
    }
}
