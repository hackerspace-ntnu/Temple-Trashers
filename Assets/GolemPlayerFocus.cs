using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPlayerFocus : StateMachineBehaviour
{
    private Transform targetPlayer, rigTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GolemAnimationSyncer sync = animator.GetComponent<GolemAnimationSyncer>();
        targetPlayer = sync.targetPlayer;
        rigTarget = sync.rigTarget;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (targetPlayer)
        {
            rigTarget.position = targetPlayer.position;   
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigTarget.position = Vector3.zero;
        animator.GetComponent<GolemAnimationSyncer>().FinishAttack();
    }
}
