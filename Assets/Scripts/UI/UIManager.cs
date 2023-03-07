using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct ResourceInfo
{
    public int delta;
    public GameObject source;

    public ResourceInfo(int delta, GameObject source)
    {
        this.delta = delta;
        this.source = source;
    }
}
public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }

    [Header("Resource UI")]

    [SerializeField]
    private int resourceStartAmount = 100;

    [SerializeField]
    private TextMeshProUGUI resourceText = default;

    [SerializeField]
    private TextMeshProUGUI scoreText = default;

    [SerializeField]
    private int score = default;

    [Header("Tutorial UI")]
    [SerializeField]
    private RectTransform startHelpPanel = default;

    [Header("Health bar variables")]
    [SerializeField]
    private Slider healthbar = default;

    [SerializeField]
    private Slider followBar = default;

    [SerializeField]
    private Material greenUI = default;

    [SerializeField]
    private Material redUI = default;

    [Range(0, 5), SerializeField]
    private float followBarDelay = 2f;

    private BaseController baseController;

    private float baseMaxHealth;

    private float actualHealth = 0;

    private int followTweenId = -1;

    private float healthAnimDiff = 0f;

    public int Score => score;

    private int resourceAmount = 0;

    public int ResourceAmount { get => resourceAmount; }

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
        resourceAmount = resourceStartAmount;
    }

    void Start()
    {
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
        if (baseController)
        {
            baseController.HealthController.onDamage -= UpdateBaseHealth;
        }
    }

    public void UpdateResourceUI()
    {
        resourceText.text = ResourceAmount.ToString();
    }

    public void SetResourceAmount(ResourceInfo resourceInfo)
    {
        resourceAmount += resourceInfo.delta;
        UpdateResourceUI();
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = score.ToString();
    }

    private void UpdateBaseHealth(DamageInfo damage)
    {
        float clampedDamage = Mathf.Clamp(damage.Damage, actualHealth - baseMaxHealth, actualHealth);

        actualHealth = damage.RemainingHealth;

        // Keeps track of recent damage changes, allows damage to negate healing so the bar animates smoother
        healthAnimDiff += clampedDamage;

        // Prevents several damage sources to animate simultaneously
        if (followTweenId != -1)
            LeanTween.cancel(followTweenId);

        followBar.fillRect.GetComponent<Image>().material = healthAnimDiff >= 0 ? redUI : greenUI;

        // Which bar to animate
        Slider tweenBar = healthAnimDiff >= 0 ? followBar : healthbar;

        followTweenId = LeanTween
            //Lerps from healthAnimDiff to 0 in the given time
            .value(healthAnimDiff, 0, 0.7f)
            //Runs function on each update
            .setOnUpdate(x =>
            {
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
}
