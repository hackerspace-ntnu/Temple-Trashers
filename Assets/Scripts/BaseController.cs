using UnityEngine;

public class BaseController : MonoBehaviour
{
    // Base Singleton
    public static BaseController Instance;

    // GameOverScreen
    public GameObject GameOverScreen;

    // Explosion
    public GameObject Explosion;

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
}
