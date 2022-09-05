using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Singleton { get; private set; }

    [SerializeField]
    private UIManager ui = default;

    //Total amount of resources.
    private int resourceAmount = 100;

    public int ResourceAmount
    {
        get => resourceAmount;
        set
        {
            int diff = value - resourceAmount;
            resourceAmount = value;
            ui.UpdateResourceUI();
            BaseController.Singleton.updateCrystalInventory(diff);
        }
    }

    void Awake()
    {
        #region Singleton boilerplate

        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Debug.LogWarning($"There's more than one {Singleton.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        Singleton = this;

        #endregion Singleton boilerplate
    }

    void Start()
    {
        if (ui == null)
            ui = UIManager.Singleton;
    }
}
