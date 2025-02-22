using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayManager : MonoBehaviour
{
    PlayerContrl playerContrl;
    Animator animator;
    InputManeger inputManeger;
    CameraManager cameraManager;
    PlayerStaminaManager playerStaminaManager;

    public bool applyRootMotion;
    public bool isPerformingAction = false;
    public bool isJumping;
    public bool isGrounded;

    private void Awake() {
        playerContrl = GetComponent<PlayerContrl>();
        inputManeger = GetComponent<InputManeger>();
        cameraManager = FindAnyObjectByType<CameraManager>();
        playerStaminaManager = FindAnyObjectByType<PlayerStaminaManager>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool("isGround", isGrounded);
        inputManeger.HandleAllInput();
        playerStaminaManager.HandleAllStaminaChange();
    }

    private void FixedUpdate() {
        playerContrl.HandleAllMovement();
    }

    //handle camera follow  
    private void LateUpdate() {
        cameraManager.HandleAllCameraMovement();
    }
}
