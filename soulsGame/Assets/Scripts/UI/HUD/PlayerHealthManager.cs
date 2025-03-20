using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : CharacterHealthManager
{
    protected override CharacterManager HealthSource { get; set; }

    protected override void Awake()
    {
        base.Awake();
        HealthSource = FindAnyObjectByType<PlayManager>(); //將character覆蓋成PlayManager
    }


    protected override void HandleHealthBarValue(float value)
    {
        base.HandleHealthBarValue(value);
    }
    
}
