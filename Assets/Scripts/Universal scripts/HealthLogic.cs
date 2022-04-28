using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthLogic : MonoBehaviour
{
    public delegate void OnStatusUpdate(DamageInfo damage);

    public OnStatusUpdate onDamage;

    public OnStatusUpdate onDeath;

    public OnStatusUpdate onHeal;

    public float health;
    public float maxHealth;

    public bool Dead => health <= 0;

    public virtual void OnReceiveDamage(MonoBehaviour fromSource, float damage, Vector3? knockBackDir = null, float? knockBackForce = null)
    {
        if (Dead)
            return;

        // Prevent receiving negative damage (should instead use `Heal()`) and damage greater than the remaining health
        health -= Mathf.Clamp(damage, 0, health);

        DamageInfo damageInfo = new DamageInfo(
            fromSource,
            damage,
            health,
            health <= 0,
            knockBackDir ?? Vector3.up,
            knockBackForce ?? 1f
        );
        if (Dead)
        {
            onDamage?.Invoke(damageInfo);
            onDeath?.Invoke(damageInfo);
        } else
            onDamage?.Invoke(damageInfo);
    }

    public virtual void Heal(MonoBehaviour fromSource, float healAmount)
    {
        health += healAmount;
        if (health > maxHealth && maxHealth > 0)
            health = maxHealth;

        DamageInfo healInfo = new DamageInfo(
            fromSource,
            -healAmount,
            health,
            health <= 0,
            Vector3.zero,
            0
        );

        onHeal?.Invoke(healInfo);
    }
}
