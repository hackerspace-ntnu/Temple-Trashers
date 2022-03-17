using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class GolemAnimationSyncer : MonoBehaviour
{
    public Transform targetPlayer;

    public Transform rigTarget;
    
    [SerializeField]
    private GolemnController controller;

    [SerializeField]
    private VisualEffect dust;
    public void AttackTrigger(string dir)
    {
        controller.DealDamage();
    }
    public void JumpTrigger()
    {
        controller.JumpTrigger();
    }

    public void FinishAttack()
    {
        controller.OnAttackFinish();
    }
    public void DustTrigger()
    {
        dust.SendEvent("OnPlay");
    }
}
