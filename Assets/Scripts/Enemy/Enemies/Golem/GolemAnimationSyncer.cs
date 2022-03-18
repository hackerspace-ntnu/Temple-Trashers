using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class GolemAnimationSyncer : MonoBehaviour
{
    public Transform targetPlayer;

    public Transform rigTarget;

    [SerializeField]
    private GolemController controller;

    [SerializeField]
    private VisualEffect dust;

    /// <summary>
    /// Called through an animation event on the <c>Armature_HeadButtFront.anim</c>,
    /// <c>Armature_SlapLeft.anim</c> and <c>Armature_SlapRight.anim</c> golem animations.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public void AttackTrigger(string dir)
    {
        controller.OnDealDamage();
    }

    /// <summary>
    /// Called through an animation event on the <c>Armature_JumpImproved.anim</c> golem animation.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public void JumpTrigger()
    {
        controller.JumpTrigger();
    }

    public void FinishAttack()
    {
        controller.OnAttackFinish();
    }

    /// <summary>
    /// Called through an animation event on the <c>Armature_JumpImproved.anim</c> golem animation.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public void DustTrigger()
    {
        dust.SendEvent("OnPlay");
    }
}
