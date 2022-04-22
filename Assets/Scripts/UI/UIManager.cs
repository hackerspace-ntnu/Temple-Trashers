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

    public Material greenUI;
    public Material redUI;

    [Range(0, 5)]
    public float followBarDelay = 2f; 

    private InventoryManager inventory;
    private BaseController baseController;

    private float baseMaxHealth;

    private float actualHealth = 0;

    private int followTweenId = -1;

    private float healthAnimDiff = 0f;
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
        baseController.HealthController.onHeal += UpdateBaseHealth;

        baseMaxHealth = baseController.HealthController.maxHealth;
        actualHealth = baseController.HealthController.health;

        healthbar.maxValue = followBar.maxValue = baseMaxHealth;
        healthbar.value = followBar.value = actualHealth;
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
        var clampedDamage = Mathf.Clamp(damage.Damage, actualHealth-baseMaxHealth, actualHealth);

        actualHealth = damage.RemainingHealth;

        // Keeps track of recent damage changes, allows damage to negate healing so the bar animates smoother
        healthAnimDiff += clampedDamage;
        
        // Prevents several damage sources to animate simultaneously
        if (followTweenId != -1)
        {
            LeanTween.cancel(followTweenId);
        }
        
        followBar.fillRect.GetComponent<Image>().material = healthAnimDiff >= 0 ? redUI  : greenUI;
        
        // Which bar to animate
        var tweenBar = healthAnimDiff >= 0 ? followBar : healthbar;

        followTweenId = LeanTween
            //Lerps from healthAnimDiff to 0 in the given time
            .value(healthAnimDiff, 0, 0.7f) 
            //Runs function on each update
            .setOnUpdate(x => {
                healthAnimDiff = x;
                tweenBar.value = actualHealth + healthAnimDiff;
            })
            .setEaseInOutCubic()
            .setDelay(followBarDelay).id;

        // Sets the noon-tweened bar to the actual health 
        (healthAnimDiff >= 0 ? healthbar : followBar).value = actualHealth;
    }

    public void DisableTutorial()
    {
        startHelpPanel.gameObject.SetActive(false);
    }

    IEnumerator healthbarTest()
    {
        baseController.HealthController.OnReceiveDamage(this, 20, null, null);

        yield return new WaitForSeconds(5f);

        baseController.HealthController.Heal(this, 20);

        yield return new WaitForSeconds(5f);

        StartCoroutine("healthbarTest");
    }
}
