using UnityEngine;

public struct DamageInfo
{
    public DamageInfo(float damage, float remainingHealth, bool dead, Vector3 knockBackDir, float knockBackForce)
    {
        Damage = damage;
        RemainingHealth = remainingHealth;
        Dead = dead;
        KnockBackDir = knockBackDir;
        KnockBackForce = knockBackForce;
    }

    public float Damage { get; }

    public float RemainingHealth { get; }
    public bool Dead { get; }
    public Vector3 KnockBackDir { get; }
    public float KnockBackForce { get; }
}
