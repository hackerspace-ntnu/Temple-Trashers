using UnityEngine;
public class SkeletonEventHelper : MonoBehaviour
{
    [SerializeField]
    private SkeletonController controller;

    public void OnWalkEvent()
    {
        controller.Walk();
    }
    public void OnAttackEvent()
    {
        controller.DealDamage();
    }
}
