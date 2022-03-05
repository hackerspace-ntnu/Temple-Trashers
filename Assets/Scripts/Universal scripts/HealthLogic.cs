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

    public bool Dead => health <= 0;

    public virtual void DealDamage(float damage, Vector3? knockBackDir = null, float? knockBackForce = null)
    {
        if (Dead)
            return;

        health -= Mathf.Max(damage, health);

        DamageInfo damageInfo = new DamageInfo(
            damage,
            health,
            health <= 0,
            knockBackDir ?? Vector3.up,
            knockBackForce ?? 1f
        );
        if (Dead)
            onDeath?.Invoke(damageInfo);
        else
            onDamage?.Invoke(damageInfo);
    }

    public virtual void Heal(float input)
    {
        health += input;
        if (health > maxHealth && maxHealth > 0)
            health = maxHealth;
    }
}
