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
    private Slider slider;


    private float maxStamina = 100f;
    public float currentStamina = 100f;
    public float rechargeRate = 10f;
    private Coroutine rechargeStamina;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        playerContrlScripts = FindAnyObjectByType<PlayerContrl>();
        playManager = FindAnyObjectByType<PlayManager>();
    }

    public void HandleAllStaminaChange(){
        HandleStaminaValue();
        HandleStaminaRecharge();
    }
    private void HandleStaminaValue(){
        currentStamina = Mathf.Clamp(currentStamina, 0f, 100f);

        slider.value = currentStamina/maxStamina;
    }

    private void HandleStaminaRecharge(){
        if (playerContrlScripts.isSprinting || playManager.isPerformingAction || playManager.isJumping){
            StopAllCoroutines();
        } else if (!playerContrlScripts.isSprinting && !playManager.isPerformingAction && currentStamina < maxStamina){
            rechargeStamina = StartCoroutine(RechargeStamina());
        }
    }

    private IEnumerator RechargeStamina(){
        yield return new WaitForSeconds(0.5f);

        while (currentStamina < maxStamina){
            currentStamina += rechargeRate/100;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
