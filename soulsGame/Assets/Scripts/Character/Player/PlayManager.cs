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

    protected override CharacterHealthManager characterHealthManager { get ; set ;}  //繼承自CharacterManager
    public bool applyRootMotion;
    public bool isPerformingAction = false;
    public bool isJumping;
    public bool isGrounded;

    protected override void Awake() {
        base.Awake();

        characterHealthManager = FindAnyObjectByType<PlayerHealthManager>();//將CharacterHealthManager覆蓋為PlayerHealthManager
        playerContrl = GetComponent<PlayerContrl>();
        animator = GetComponent<Animator>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    void Start()
    {
        maxHealth = 100f;
        maxStamina = 100f;
        currentStamina = maxStamina;
        currentHealth = maxHealth;
    }

    protected override void Update() {
        base.Update();
        
        animator.SetBool("isGround", isGrounded);
        InputManeger.istance.HandleAllInput();
    }

    private void FixedUpdate() {
        playerContrl.HandleAllMovement();
    }

    //handle camera follow  
    private void LateUpdate() {
        CameraManager.istance.HandleAllCameraMovement();
    }
}
