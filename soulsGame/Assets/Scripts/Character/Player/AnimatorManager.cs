using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    PlayManager playManager;
    int xVelocity;
    int yVelocity;

    private void Awake() {
        animator = GetComponent<Animator>();
        playManager = GetComponent<PlayManager>();
        xVelocity = Animator.StringToHash("xVelocity");
        yVelocity = Animator.StringToHash("yVelocity");
    }

    public void UpdateAnimation(float horizontalInput, float verticalInput, bool isSprinting){
        float snappedHorizontal;
        float snappedVertical;
        //使狀態改變時，動畫不會處於兩個狀態之間，而是直接進行下個狀態
#region "snapped horizontalInput"
        if (horizontalInput > 0 && horizontalInput < 0.55f){
            snappedHorizontal = 0.5f;
        }else if(horizontalInput > 0.55f){
            snappedHorizontal = 1f;
        }else if (horizontalInput < 0 && horizontalInput > -0.55f){
            snappedHorizontal = -0.5f;
        }else if(horizontalInput < -0.55f){
            snappedHorizontal = -1f;
        }else{
            snappedHorizontal = 0f;
        }
#endregion 
#region "snapped verticalInput"
        if (verticalInput > 0 && verticalInput < 0.55f){
            snappedVertical = 0.5f;
        }else if(verticalInput > 0.55f){
            snappedVertical = 1f;
        }else if (verticalInput < 0 && verticalInput > -0.55f){
            snappedVertical = -0.5f;
        }else if(verticalInput < -0.55f){
            snappedVertical = -1f;
        }else{
            snappedVertical = 0f;
        }
#endregion

        if (isSprinting){
            snappedVertical = 2f;
        }
        
        animator.SetFloat(xVelocity, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(yVelocity, snappedVertical, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAction(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true){
        playManager.applyRootMotion = applyRootMotion;
        playManager.isPerformingAction = isPerformingAction;

        animator.CrossFade(targetAnimation, 0.2f);
    }
}
