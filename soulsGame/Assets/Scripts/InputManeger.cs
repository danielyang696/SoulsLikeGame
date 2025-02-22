using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Windows;

public class InputManeger : MonoBehaviour
{
    //這個是input system
    PlayerControls playerContrl;

    //這個是scripts
    PlayerContrl playerContrlScripts;
    PlayManager playManager;
    AnimatorManager animatorManager;
    PlayerStaminaManager playerStaminaManager;
    
    public Vector2 moveInput{get; private set;}
    public Vector2 mouseInput{get; private set;}
    private bool dodgeInput = false;
    private bool sprintingInput;
    private bool walkInput;
    private bool jumpInput = false;

    public float mouseX;
    public float mouseY;
    public float moveAmount;

    public float horizontalInput{get; private set;}
    public float verticalInput{get; private set;}

    private void Awake() {
        animatorManager = GetComponent<AnimatorManager>();
        playerContrlScripts = GetComponent<PlayerContrl>();
        playerStaminaManager = FindAnyObjectByType<PlayerStaminaManager>();
        playManager = GetComponent<PlayManager>();
    }

    private void OnEnable()
    {
        if (playerContrl == null){
            playerContrl = new PlayerControls();

            playerContrl.PlayerlockFov.Move.performed += i => moveInput = i.ReadValue<Vector2>();
            playerContrl.PlayerlockFov.Camera.performed += i => mouseInput = i.ReadValue<Vector2>();
            playerContrl.Playeraction.Dodge.performed += i => dodgeInput = true;
            playerContrl.Playeraction.Jump.performed += i => jumpInput = true;
            playerContrl.Playeraction.Sprinting.performed += i => sprintingInput = true;
            playerContrl.Playeraction.PCWalk.performed += i => walkInput = true;

            playerContrl.Playeraction.Sprinting.canceled += i => sprintingInput = false;
            playerContrl.Playeraction.PCWalk.canceled += i => walkInput = false;
            playerContrl.PlayerlockFov.Move.canceled += i => moveInput = new Vector2(0f,0f);
        }

        playerContrl.Enable();
    }

    private void OnDisable()
    {
        playerContrl.Disable();
    }

    public void HandleAllInput(){
        HandleMovementInput();
        HandleDodgeInput();
        HandleSprintInput();
        HandleJumpInput();
    }


    private void HandleMovementInput(){
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;

        mouseX = mouseInput.x;
        mouseY = mouseInput.y;

        //handle animation
        moveAmount = Mathf.Clamp01(math.abs(horizontalInput) + math.abs(verticalInput));

        if (walkInput && moveAmount >= 0.5f){
            moveAmount = 0.5f;
            animatorManager.UpdateAnimation(0, moveAmount, playerContrlScripts.isSprinting);
        }else{
            animatorManager.UpdateAnimation(0, moveAmount, playerContrlScripts.isSprinting);
        }
    }

    private void HandleDodgeInput(){
        if (dodgeInput){
            dodgeInput = false;

            playerContrlScripts.TryDodge();
        }
    }

    private void HandleJumpInput(){
        if (jumpInput){
            jumpInput = false;

            playerContrlScripts.TryJump();
        }
    }

    private void HandleSprintInput(){
        if (sprintingInput && moveAmount > 0.55f && playerStaminaManager.currentStamina > 0){
            playerContrlScripts.isSprinting = true;
        }else{
            playerContrlScripts.isSprinting = false;
        }
    }
}