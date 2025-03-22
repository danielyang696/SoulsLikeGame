using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class CharacterHealthManager : MonoBehaviour
{
    public Slider characterSlider;
    [SerializeField] public CharacterManager HealthSource; 

    protected virtual void Awake()
    {
        characterSlider = GetComponent<Slider>();
        HealthSource.OnHealthChanged += HandleHealthBarValue; //訂閱事件，在事件觸發時會呼叫，在事件觸發時會呼叫HandleHealthBarValue
    }


    protected virtual void HandleHealthBarValue(float value){
        characterSlider.value = value/HealthSource.maxHealth;
    }

}
