using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{

    public struct DamageInstance
    {
        public DamageInstance(float damage, float remainingHealth, bool dead, Vector3 knockBackDir, float knockBackForce)
        {
            Damage = damage;
            KnockBackForce = knockBackForce;
            RemainingHealth = remainingHealth;
            Dead = dead;
            KnockBackDir = knockBackDir;
        }
        public float Damage { get; }
        public float KnockBackForce { get; }

        public float RemainingHealth { get; }
        public bool Dead { get; }
        public Vector3 KnockBackDir { get; }
    }

    public delegate void OnStatusUpdate(DamageInstance damage);

    public OnStatusUpdate onDeath;

    public OnStatusUpdate onDamage;

    public float health;
    public float maxHealth;

    public virtual void DealDamage(float input, Vector3? knockBackDir = null, float? knockBackForce = null)
    {
        health -= input;

        DamageInstance damage = new DamageInstance(
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
