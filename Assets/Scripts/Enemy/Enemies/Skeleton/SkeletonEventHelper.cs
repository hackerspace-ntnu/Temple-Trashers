using UnityEngine;


public class SkeletonEventHelper : MonoBehaviour
{
    [SerializeField]
    private SkeletonController controller;

    /// <summary>
    /// Called through an animation event on `Armature_Attack_1.anim`.
    /// </summary>
    public void OnAttackEvent()
    {
        controller.DealDamage();
    }
}
