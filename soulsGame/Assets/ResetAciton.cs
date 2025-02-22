using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAciton : StateMachineBehaviour
{
    PlayManager playManager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playManager == null){
            playManager = animator.GetComponent<PlayManager>();
        }

        playManager.applyRootMotion = false;
        playManager.isPerformingAction = false;
    }
}
