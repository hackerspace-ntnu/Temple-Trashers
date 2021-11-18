using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    public delegate void OnStatusUpdate(DamageInfo damage);

    public OnStatusUpdate onDeath;

    public OnStatusUpdate onDamage;

    public float health;
    public float maxHealth;

    public virtual void DealDamage(float input, Vector3? knockBackDir = null, float? knockBackForce = null)
    {
        health -= input;

        DamageInfo damage = new DamageInfo(
                input,
                health,
                health <= 0,
                knockBackDir ?? Vector3.up,
                knockBackForce ?? 1f
            );
        if (health <= 0)
            onDeath?.Invoke(damage);
        else
            onDamage?.Invoke(damage);
        
        
    }

    public virtual void Heal(float input)
    {
        health += input;
        if (health > maxHealth && maxHealth > 0)
            health = maxHealth;
    }
}
