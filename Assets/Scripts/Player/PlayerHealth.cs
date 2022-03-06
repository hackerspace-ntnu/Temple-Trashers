using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthLogic
{
    public bool injured = false;
    [SerializeField] private float healTime = 10;
    private float timeUntilHeal;

    private void Start()
    {
        timeUntilHeal = healTime;
    }

    
    public override void DealDamage(float damage, Vector3? knockBackDir = null, float? knockBackForce = null)
    {
        
        if (injured)
        {
            // Die
            DamageInfo damageInfo = new DamageInfo(
                damage,
                health,
                true,
                Vector3.up,
                1f
            );

            onDeath.Invoke(damageInfo);
        } else
        {
            // ouch
            injured = true;
            timeUntilHeal = healTime;
        }

    }

    private void Update()
    {
        // Update timeUntilHeal
        if (injured)
        {
            timeUntilHeal -= Time.deltaTime;
            Debug.Log(timeUntilHeal);
        }


        
        // Heal
        if (timeUntilHeal < 0)
        {
            injured = false;
            timeUntilHeal = healTime; // Reset the timer
            Debug.Log("Healed");
        }
    }


}
