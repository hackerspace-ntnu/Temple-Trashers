using UnityEngine;


public struct DamageInfo
{
    public MonoBehaviour FromSource { get; }
    public float Damage { get; }

    public float RemainingHealth { get; }
    public bool Dead { get; }
    public Vector3 KnockBackDir { get; }
    public float KnockBackForce { get; }

    public DamageInfo(MonoBehaviour fromSource, float damage, float remainingHealth, bool dead, Vector3 knockBackDir, float knockBackForce)
    {
        FromSource = fromSource;
        Damage = damage;
        RemainingHealth = remainingHealth;
        Dead = dead;
        KnockBackDir = knockBackDir;
        KnockBackForce = knockBackForce;
    }
}
