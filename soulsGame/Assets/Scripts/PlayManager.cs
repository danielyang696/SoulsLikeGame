using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayManager : MonoBehaviour
{
    //PlayerContrl是player的移動腳本
    public PlayerContrl playerContrl;
    Animator animator;
    public PlayerStaminaManager playerStaminaManager;
    public AnimatorManager animatorManager;



    public bool applyRootMotion;
    public bool isPerformingAction = false;
    public bool isJumping;
    public bool isGrounded;

    private void Awake() {
        DontDestroyOnLoad(this);

        playerContrl = GetComponent<PlayerContrl>();
        playerStaminaManager = FindAnyObjectByType<PlayerStaminaManager>();
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
