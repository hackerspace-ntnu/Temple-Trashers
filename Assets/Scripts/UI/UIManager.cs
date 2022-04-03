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
    
    private float followBarCountdown; 

    private InventoryManager inventory;
    private BaseController baseController;

    private float baseMaxHealth;

    private float actualhealth = 0;

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
        actualhealth = baseController.HealthController.health;

        healthbar.maxValue = followBar.maxValue = baseMaxHealth;
        healthbar.value = followBar.value = actualhealth;

        StartCoroutine("healthbarTest");
    }

    private void Update()
    {
        followBarCountdown -= Time.deltaTime;

        if (actualhealth > healthbar.value)
        {
            // We are healing
            followBar.fillRect.GetComponent<Image>().material = greenUI;
            followBar.value = actualhealth;

            if (followBarCountdown <= 0)
            {
                healthbar.value +=baseMaxHealth / 10f * Time.deltaTime;
            }
        }

        if (actualhealth < followBar.value)
        {
            // We are taking damage
            followBar.fillRect.GetComponent<Image>().material = redUI;
            healthbar.value = actualhealth;

            if (followBarCountdown <= 0)
            {
                followBar.value = Mathf.Lerp(followBar.value, healthbar.value, Time.deltaTime);
            }
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
        actualhealth = damage.RemainingHealth;
        followBarCountdown = followBarDelay;
    }

    public void DisableTutorial()
    {
        startHelpPanel.gameObject.SetActive(false);
    }

    IEnumerator healthbarTest()
    {
        baseController.HealthController.OnReceiveDamage(20, null, null);

        yield return new WaitForSeconds(5f);

        baseController.HealthController.Heal(20);

        yield return new WaitForSeconds(5f);

        StartCoroutine("healthbarTest");
    }
}
