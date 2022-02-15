using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }

    public Text resourceText;
    public Text scoreText;


    private InventoryManager inventory;
    public int score;

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
        inventory = InventoryManager.Singleton;
        UpdateResourceUI();
    }

    public void UpdateResourceUI()
    {
        resourceText.text = inventory.ResourceAmount.ToString();
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = score.ToString();
    }
}
