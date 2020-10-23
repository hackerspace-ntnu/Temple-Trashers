using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void dealDamage(float input)
    {
        if (health < input)
        {
            Debug.Log("You are dead, not big surprise");
        }
        else
        {
            health -= input;
        }
    }
    public virtual void heal(float input)
    {
        health += input;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
