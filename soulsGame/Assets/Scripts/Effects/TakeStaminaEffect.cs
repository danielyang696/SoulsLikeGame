using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CharacterEffect/InstantEffect/TakeStaminaEffect")]
public class TakeStaminaEffect : InstantCharacterEffect
{
    public float staminaDamage;

    public override void ProcessEffect(CharacterManager character)
    {
        CalulateStaminaDamage(character);
    }

    //在此先計算要扣除多少體力
    private void CalulateStaminaDamage(CharacterManager character){
        character.playerStaminaManager.currentStamina -= staminaDamage;
    }
}
