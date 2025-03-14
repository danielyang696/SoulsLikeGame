using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterHealthManager : MonoBehaviour
{
    public Slider characterSlider;
    protected abstract CharacterManager HealthSource { get; set; }

    protected virtual void Awake()
    {
        characterSlider = GetComponent<Slider>();
        HealthSource = FindAnyObjectByType<CharacterManager>();
        HealthSource.OnHealthChanged += HandleHealthBarValue; //訂閱事件，在事件觸發時會呼叫，在事件觸發時會呼叫HandleHealthBarValue
    }


    protected virtual void HandleHealthBarValue(float value){
        characterSlider.value = value/HealthSource.maxHealth;
    }

}
