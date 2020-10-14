using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public static BaseController Instance;
    
    [SerializeField]
    private int maxHealth = 101, currentHealth = 101;
    [SerializeField]
    private GameObject GUIPrefab;


    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            print("Tried to make two bases, removed the latter");
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DealDamage(int dmg){
        currentHealth -= dmg;
        if(currentHealth <= 0){ Die(); }
    }
    private void Die(){
        //Funksjonalitet for gui og restart
        //BIG EXPLOTION
        Instantiate(GUIPrefab);
    }
}
