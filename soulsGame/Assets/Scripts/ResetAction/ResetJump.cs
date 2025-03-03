using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : StateMachineBehaviour
{
    PlayManager playManager;
    PlayerContrl playerContrlScript;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playManager == null){
            playManager = animator.GetComponent<PlayManager>();
        }

        if (playerContrlScript == null){
            playerContrlScript = animator.GetComponent<PlayerContrl>();
        }
        
        playerContrlScript.HandleLandingVelocity();
        playManager.isJumping = false;
    }
}