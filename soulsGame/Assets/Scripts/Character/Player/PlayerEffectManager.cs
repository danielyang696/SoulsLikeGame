using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerEffectManager : CharacterEffectManager
{
    [Header("Debug Delete later")]
    [SerializeField] InstantCharacterEffect effectToTest;
    [SerializeField] bool isProcessingEffect;
    PlayManager player;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (isProcessingEffect){
            isProcessingEffect = false;
            InstantCharacterEffect effect = Instantiate(effectToTest) as TakeStaminaEffect;
            effect.ProcessEffect(player);
        }
    }
}
