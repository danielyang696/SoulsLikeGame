using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayManager : CharacterManager
{
    //PlayerContrl是player的移動腳本
    public PlayerContrl playerContrl;
    Animator animator;
    public AnimatorManager animatorManager;



    public bool applyRootMotion;
    public bool isPerformingAction = false;
    public bool isJumping;
    public bool isGrounded;

    protected override void Awake() {
        base.Awake();

        playerContrl = GetComponent<PlayerContrl>();
        animator = GetComponent<Animator>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void Update() {
        animator.SetBool("isGround", isGrounded);
        InputManeger.istance.HandleAllInput();
        playerStaminaManager.HandleAllStaminaChange();
    }

    private void FixedUpdate() {
        playerContrl.HandleAllMovement();
    }

    //handle camera follow  
    private void LateUpdate() {
        CameraManager.istance.HandleAllCameraMovement();
    }
}
