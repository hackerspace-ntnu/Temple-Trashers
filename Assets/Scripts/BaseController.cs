using UnityEngine;

public class BaseController : MonoBehaviour
{
    // Base Singleton
    public static BaseController Instance;

    // Base stats
    [SerializeField]
    private int maxHealth = 1000, currentHealth = 1000;

    // GameOverScreen
    [SerializeField]
    private GameObject GameOverScreen = null;

    //Explosion
    [SerializeField]
    private GameObject Explosion = null;

    [SerializeField]
    private Transform spawnPoint = null;

    // Death flag
    private bool dead = false;


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

    private void Die(){
        if(dead == false)
        {
            //BIG EXPLOSION
            Instantiate(Explosion, this.transform.position, new Quaternion(0, 0, 0, 0), this.transform);
            // Creates the GUI "GameOverScreen"
            Instantiate(GameOverScreen);
        }
        dead = true;
    }
    public Transform SpawnPoint { get => spawnPoint; }
}
