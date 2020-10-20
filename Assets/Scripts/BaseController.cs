using UnityEngine;

public class BaseController : MonoBehaviour
{

    // Base Singleton
    // Use "BaseController.Instance.DealDamage(int dmg) to damage the base
    public static BaseController Instance;

    // Base stats
    [SerializeField]
    private int maxHealth = 1000, currentHealth;

    // GameOverScreen
    [SerializeField]
    private GameObject GameOverScreen;


    void Awake()
    {
        // Makes sure there is only one base
        if ( Instance == null ) { Instance = this; }
        else
        {
            print("Tried to make two bases, removed the latter");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Starts the game with max health
        currentHealth = maxHealth;
    }


    // Call to deal damage to the base
    public void DealDamage(int dmg)
    {
        currentHealth -= dmg;
        if ( currentHealth <= 0 ) { Die(); }
    }

    private void Die(){

        Debug.Log("DIED");
        //BIG EXPLOSION

        // Creates the GUI "GameOverScreen"
        Instantiate(GameOverScreen);
    }
}
