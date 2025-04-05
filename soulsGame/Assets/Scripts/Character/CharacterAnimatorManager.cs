using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager characterManager; //需到Unity Editor中手動設定
        int xVelocity;
        int yVelocity;

        protected virtual void Awake() {
            characterManager = GetComponent<CharacterManager>();
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
            
            characterManager.animator.SetFloat(xVelocity, snappedHorizontal, 0.1f, Time.deltaTime);
            characterManager.animator.SetFloat(yVelocity, snappedVertical, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAction(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true){
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.isPerformingAction = isPerformingAction;

            characterManager.animator.CrossFade(targetAnimation, 0.2f);
        }
    }
}

