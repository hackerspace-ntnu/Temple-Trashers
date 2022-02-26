using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }

    public Text resourceText;
    public Text scoreText;
    public Scrollbar healthSlider;

    private InventoryManager inventory;
    private BaseController baseController;
    
    private float baseMaxHealth;
    
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
        baseController = BaseController.Singleton;

        UpdateResourceUI();
        baseController.HealthController.onDamage += UpdateBaseHealth;
        baseMaxHealth = baseController.HealthController.maxHealth;

    }

    private void OnDestroy()
    {
        baseController.HealthController.onDamage -= UpdateBaseHealth;
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

    private void UpdateBaseHealth(DamageInfo damage)
    {
        //The healthslider goes from right to left instead of left to right, thus it needs to be inverse (1-x)
        healthSlider.size = 1 - damage.RemainingHealth/baseMaxHealth;
    }
}
