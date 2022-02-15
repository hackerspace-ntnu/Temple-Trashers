using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Singleton { get; private set; }

    public UIManager ui;

    //Total amount of resources.
    private int resourceAmount = 100;

    public int ResourceAmount
    {
        get => resourceAmount;
        set
        {
            resourceAmount = value;
            ui.UpdateResourceUI();
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
}
