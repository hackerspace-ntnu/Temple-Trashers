using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    public delegate void OnStatusUpdate();

    public OnStatusUpdate onDeath;

    public float health;
    public float maxHealth;

    public virtual void DealDamage(float input)
    {
       
        if (health <= input)
            onDeath?.Invoke();
        else
            health -= input;
    }

    public virtual void Heal(float input)
    {
        health += input;
        if (health > maxHealth && maxHealth > 0)
            health = maxHealth;
    }
}
