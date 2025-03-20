using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public PlayerStaminaManager playerStaminaManager; //由於AI敵人沒有耐力，為了處理TakeStaminaEffect所以先這樣寫
    public CharacterEffectManager characterEffectManager;
    public CharacterAnimatorManager characterAnimatorManager;
    protected abstract CharacterHealthManager characterHealthManager { get; set; }

    [Header("States")]
    public bool applyRootMotion;
    public bool isPerformingAction = false;
    public bool isJumping;
    public bool isGrounded;

    [Header("Character Stats")]
    public bool isDead = false;
    public float maxStamina;
    private float _currentStamina;
    public float currentStamina //提供對 _currentStamina 的間接訪問
    {
        get => _currentStamina;
        set {
            _currentStamina = Mathf.Clamp(value, 0f, maxStamina);
            OnStaminaChanged?.Invoke(_currentStamina); //在此時觸發事件，並傳遞_currentStamina
        }
    }

    public float maxHealth;
    [SerializeField] private float _currentHealth;
    public float currentHealth //提供對 _currentHealth 的間接訪問
    {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0f, maxHealth);
            OnHealthChanged?.Invoke(_currentHealth); //在此時觸發事件，並傳遞_currentHealth
        } 
    }

    public event System.Action<float> OnHealthChanged;//當血量改變時觸發的事件，為了在血量改變時也改變Health Bar的value
    public event System.Action<float> OnStaminaChanged;//當體力改變時觸發的事件，為了在體力改變時也改變Stamina Bar的value


    protected virtual void Awake() {
        characterHealthManager = FindAnyObjectByType<CharacterHealthManager>();
        playerStaminaManager = FindAnyObjectByType<PlayerStaminaManager>();
        characterEffectManager = GetComponent<CharacterEffectManager>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        animator = GetComponent<Animator>();
        OnHealthChanged += CheckHP;

        DontDestroyOnLoad(this);
    }

    protected virtual void Update()
    {
        playerStaminaManager.HandleStaminaRecharge();
    }

    public virtual void CheckHP(float value){
        if (isDead) return;
        if (currentHealth <= 0) {
            StartCoroutine(ProcessDeathEvent());
        }

        //確保血量不超過最大值
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false){
        isDead = true;

        if (!manuallySelectDeathAnimation){
            characterAnimatorManager.PlayTargetAction("Dead_01", true);
        }

        yield return new WaitForSeconds(5f);
        //處理掉落物品之類的
    }
}
