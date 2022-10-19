using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    private Text resourceAmount = default;

    private InventoryManager inventory;

    void Start()
    {
        inventory = InventoryManager.Singleton;
        UpdateUI();
    }

    public void UpdateUI()
    {
        resourceAmount.text = inventory.ResourceAmount.ToString();
    }
}
