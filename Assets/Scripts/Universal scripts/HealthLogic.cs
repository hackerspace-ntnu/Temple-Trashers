using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour {
    public float health;

    void Start() {
        
    }

    public virtual void DealDamage(float input) {

        if (health <= input) {
            Debug.Log("You are dead, not big surprise");
        } else {
            health -= input;
        }

    }
    public virtual void Heal(float input) {
        health += input;
    }
    // Update is called once per frame
    void Update() {
        
    }
}
