using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerStaminaManager : MonoBehaviour
{
    PlayerContrl playerContrlScripts;
    PlayManager playManager;
    private Slider staminaBarslider;


    //private float maxStamina = 100f;
    //public float currentStamina = 100f;
    public float rechargeRate = 10f;
    private Coroutine rechargeStamina;

    private void Awake()
    {
        staminaBarslider = GetComponent<Slider>();
        playerContrlScripts = FindAnyObjectByType<PlayerContrl>();
        playManager = FindAnyObjectByType<PlayManager>();
        playManager.OnStaminaChanged += HandleStaminaBarValue; //訂閱事件，事件觸發時會呼叫HandleStaminaBarValue
    }

    private void HandleStaminaBarValue(float value){

        staminaBarslider.value = value/playManager.maxStamina;
    }

    public void HandleStaminaRecharge(){
        if (playerContrlScripts.isSprinting || playManager.isPerformingAction || playManager.isJumping){
            StopAllCoroutines();
        } else if (!playerContrlScripts.isSprinting && !playManager.isPerformingAction && playManager.currentStamina < playManager.maxStamina){
            rechargeStamina = StartCoroutine(RechargeStamina());
        }
    }

    private IEnumerator RechargeStamina(){
        yield return new WaitForSeconds(0.5f);

        while (playManager.currentStamina < playManager.maxStamina){
            playManager.currentStamina += rechargeRate/100;
            if (playManager.currentStamina > playManager.maxStamina) playManager.currentStamina = playManager.maxStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
