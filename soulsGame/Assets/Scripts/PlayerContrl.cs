using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.AI;
using UnityEditor.Animations;
using UnityEditor.MPE;

public class PlayerContrl : MonoBehaviour
{
    [Header("references")]
    Rigidbody rb;
    PlayManager playManager;
    InputManeger inputManeger;
    AnimatorManager animatorManager;
    PlayerStaminaManager playerStaminaManager;
    Transform cameraObject;

    [Header("movement")]
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runningSpeed ;
    [SerializeField] public float sprintingSpeed ;
    [SerializeField] public float rotationSpeed;
    [SerializeField] public float sprintingStaminaCost;
    public bool isSprinting;
    Vector3 moveDirection;
    Vector3 moveVelocity;

    [Header("falling and ground check")]
    [SerializeField] public float fallStartVelocity;
    public float inAirTimer;
    public bool isSetFallVelocity;
    [SerializeField] public float gravityForce;
    [SerializeField] public float fallingGravityForce;
    public LayerMask whatIsGround;
    [SerializeField] public float groundCheckRadius;
    [SerializeField] public float maxGroundCheckDistance;
    [SerializeField] public float raycastOffset = 0.5f;
    private Vector3 yVelocity;
    private RaycastHit hit;

    [Header("Actions")]
    [SerializeField] public float dodgeStaminaCost;
    [SerializeField] public float jumpStaminaCost;
    [SerializeField] public float jumpForce;

    private Vector3 targetPosition;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManeger = GetComponent<InputManeger>();
        cameraObject = Camera.main.transform;
        playManager = GetComponent<PlayManager>();
        playerStaminaManager = FindAnyObjectByType<PlayerStaminaManager>();
    }

    private void Update()
    { 
        HandleGravity();
    }

    public void HandleAllMovement(){
        HandleMovement();
        HandleRotation();

        //放在fixUpdate確保不會影響到移動
        HandleGroundCheck();
    }

    private void HandleMovement(){
        if (playManager.isPerformingAction) return;
        if (!playManager.isGrounded) return;

        moveDirection = cameraObject.forward * inputManeger.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManeger.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0f;
        
        if (isSprinting && inputManeger.moveAmount >= 0.5f){
            moveVelocity = moveDirection * sprintingSpeed;

            playerStaminaManager.currentStamina -= sprintingStaminaCost * Time.deltaTime;
        }else{
            if (inputManeger.moveAmount > 0.5f){
            moveVelocity = moveDirection * runningSpeed;
            }else{
                moveVelocity = moveDirection * walkSpeed;
            }
        }

        //確保不會覆蓋jumpforce
        if (!playManager.isJumping && playManager.isGrounded) rb.velocity = moveVelocity;
    }

    private void HandleRotation(){ 
        if (playManager.isPerformingAction) return;
        if (!playManager.isGrounded) return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManeger.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManeger.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero){
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation,  targetRotation, rotationSpeed * Time.deltaTime);

        //確保跳躍時無法旋轉
        if (!playManager.isJumping && playManager.isGrounded) transform.rotation = playerRotation;
    }

    public void TryDodge(){
        if (playManager.isPerformingAction) return;

        if (!playManager.isGrounded) return;

        if (playerStaminaManager.currentStamina <= 0) return;


        if (inputManeger.moveAmount > 0){
            Vector3 rollDirection = Vector3.zero;

            rollDirection = cameraObject.forward * inputManeger.verticalInput;
            rollDirection = rollDirection + cameraObject.right * inputManeger.horizontalInput;
            rollDirection.Normalize();
            rollDirection.y = 0f;

            Quaternion rollRotation = Quaternion.LookRotation(rollDirection);
            transform.rotation = rollRotation;

            animatorManager.PlayTargetAction("RollAnimation", true);
        }else{
            animatorManager.PlayTargetAction("Back step", true);
        }

        playerStaminaManager.currentStamina -= dodgeStaminaCost;
    }
    

    private void HandleGroundCheck(){
        Vector3 raycastOrigin = transform.position;
        targetPosition = transform.position;
        raycastOrigin.y += raycastOffset;
        playManager.isGrounded = Physics.SphereCast(raycastOrigin, groundCheckRadius, -Vector3.up, out hit, maxGroundCheckDistance, whatIsGround);
        targetPosition.y = hit.point.y;
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;

        float groundHeight = hit.point.y;
        float characterHeight = transform.position.y;
        //使腳色不陷入地板
        if (playManager.isGrounded && !playManager.isJumping){
            if ((playManager.isPerformingAction || inputManeger.moveAmount > 0f) && !playManager.isJumping){
                // 若角色低於地面，則稍微推動角色向上，避免誤差
                if (characterHeight != groundHeight)
                {   
                    float transLateVelocity = groundHeight - characterHeight;
                    velocity.y = transLateVelocity * walkSpeed * 30f; //調整高度

                    rb.velocity = velocity; // 更新角色速度
                }          
            }else{
                transform.position = targetPosition;
            }
        }
    }

    private void HandleGravity(){
        if (playManager.isGrounded){
            if (rb.velocity.y <= 0){
                inAirTimer = 0f;
                isSetFallVelocity = false;
            }
        }else{
            if (!isSetFallVelocity && !playManager.isJumping){
                inAirTimer += Time.deltaTime;
                isSetFallVelocity = true;
                yVelocity.y = fallStartVelocity;
            }

            inAirTimer += Time.deltaTime;
            animatorManager.animator.SetFloat("airTimer", inAirTimer);

            rb.AddForce(-Vector3.up * fallingGravityForce * inAirTimer);
        }
    }

    //使groundcheck半徑可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, groundCheckRadius);
    }
    

    public void TryJump(){
        if (playManager.isPerformingAction) return;
        
        if (playerStaminaManager.currentStamina <= 0) return;

        if (!playManager.isGrounded) return;

        if (playManager.isJumping) return;

        playManager.isJumping = true;
       
        animatorManager.PlayTargetAction("Jump start", false, false);
        
        playerStaminaManager.currentStamina -= jumpStaminaCost;
    }

    public void ApplyJumpVelocity(){
        float jumpVelocity = Mathf.Sqrt(2 * gravityForce * jumpForce);
        Vector3 playVelocity = moveVelocity;
        playVelocity.y = 0f;
        playVelocity.y = jumpVelocity;
        rb.velocity = playVelocity;
    }

    public void HandleLandingVelocity(){
        playManager.isPerformingAction = true;
        rb.velocity = Vector3.zero;
    }

    //用rigbody達成ApplyRootMotion的效果
    private void OnAnimatorMove()
    {
        // 如果你不想应用 RootMotion，首先确保 animator.applyRootMotion 为 false
        if (playManager.applyRootMotion)
        {
            // 获取动画中的位移和旋转数据
            Vector3 deltaPosition = animatorManager.animator.deltaPosition;
            deltaPosition.y = 0;

            //使動畫的位移沿著斜坡角度移動
            if (playManager.isGrounded) {
                deltaPosition = Vector3.ProjectOnPlane(deltaPosition, hit.normal).normalized * deltaPosition.magnitude;
            }

            rb.velocity = deltaPosition / Time.deltaTime;
        }
    }
}