using UnityEngine;

public class SkeletonEventHelper : MonoBehaviour
{
    [SerializeField]
    private SkeletonController controller;

    public void OnAttackEvent()
    {
        controller.DealDamage();
    }
}
