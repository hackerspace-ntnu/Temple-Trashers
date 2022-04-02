using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }

    [Header("Resource UI")]
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI scoreText;
    public int score;

    [Header("Tutorial UI")]
    public RectTransform startHelpPanel;

    [Header("Health bar variables")]
    public Slider healthbar;
    public Slider followBar;

    [Range(0, 5)]
    public float followBarDelay = 2f;
    
    private float maxSize;  // The size of the bars
    private float followBarCountdown; 

    private InventoryManager inventory;
    private BaseController baseController;

    private float baseMaxHealth;

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

        healthbar.maxValue = followBar.maxValue = baseMaxHealth;
        healthbar.value = followBar.value = baseMaxHealth;
    }

    private void Update()
    {
        followBarCountdown -= Time.deltaTime;

        if(followBarCountdown <= 0)
        {
            followBar.value = Mathf.Lerp(followBar.value, healthbar.value, Time.deltaTime);
        }
    }

    void OnDestroy()
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
        healthbar.value = damage.RemainingHealth;
        followBarCountdown = followBarDelay;
    }

    public void DisableTutorial()
    {
        startHelpPanel.gameObject.SetActive(false);
    }
}
