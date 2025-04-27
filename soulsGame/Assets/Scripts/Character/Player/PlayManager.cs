using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace SG{
    public class PlayManager : CharacterManager
    {
        //PlayerContrl是player的移動腳本
        public PlayerContrl playerContrl;

        protected override void Awake() {
            base.Awake();

            playerContrl = GetComponent<PlayerContrl>();
            WorldSaveGameManager.instance.player = this;
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

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            PlayerUIManager.istance.popUpManager.SendYouDiedPopUp();

            return base.ProcessDeathEvent(manuallySelectDeathAnimation);
        }


        public void SaveGameToCurrentCharacterData(ref CharacterSaveData currentCharacterData){
            currentCharacterData.characterName = "Knight";
            currentCharacterData.characterPosition = transform.position;
        }

        public void LoadGameFormCurrentCharacterData(ref CharacterSaveData currentCharacterData){
            Vector3 myPosition = currentCharacterData.characterPosition;    
            transform.position = myPosition;
        }
    }
}
