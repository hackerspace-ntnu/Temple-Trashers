using UnityEngine;
using System.Collections.Generic;

public class BaseController : MonoBehaviour
{
    // Base Singleton
    public static BaseController Instance;

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

    // Loot in collection range
    private List<Transform> loot = new List<Transform>();

    // Stored Resources
    public int crystals = 0;


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Loot>() && !loot.Contains(other.transform))
        {
            loot.Add(other.GetComponentInChildren<Loot>().transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<Loot>())
        {
            loot.Remove(other.transform);
        }
    }

    private void Update()
    {
        // Collect loot
        if(loot.Count != 0)
        {
            loot[0].GetComponent<Loot>().Absorb();
            crystals++;
            loot.Remove(loot[0]);
        }
    }
}
