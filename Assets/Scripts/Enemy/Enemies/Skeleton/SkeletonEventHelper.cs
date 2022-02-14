using UnityEngine;


public class SkeletonEventHelper : MonoBehaviour
{
    [SerializeField]
    private SkeletonController controller;

    /// <summary>
    /// Called through an animation event on the <c>Armature_Attack_1.anim</c> skeleton animation.
    /// </summary>
    public void OnAttackEvent()
    {
        controller.DealDamage();
    }
}
