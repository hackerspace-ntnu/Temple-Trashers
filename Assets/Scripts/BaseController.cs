using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    // Base Singleton
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
        // Starts the game with max health (should this be in start or awake?)
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call to deal damage to the base
    public void DealDamage(int dmg)
    {
        currentHealth -= dmg;
        if ( currentHealth <= 0 ) { Die(); }
    }

    private void Die(){

        Debug.Log("DIED");
        //Funksjonalitet for gui og restart
        //BIG EXPLOTION

        // Creates the GUI "GameOverScreen"
        Instantiate(GameOverScreen);
    }
}
