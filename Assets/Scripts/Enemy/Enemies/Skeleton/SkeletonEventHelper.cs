using UnityEngine;


public class SkeletonEventHelper : MonoBehaviour
{
    [SerializeField]
    private SkeletonController controller = default;

    /// <summary>
    /// Called through an animation event on the <c>Armature_Attack_1.anim</c> skeleton animation.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public void OnAttackEvent()
    {
        controller.OnDealDamage();
    }
}
