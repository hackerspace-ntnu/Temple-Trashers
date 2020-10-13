using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void updateHealth(float input)
    {
        health += input;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
