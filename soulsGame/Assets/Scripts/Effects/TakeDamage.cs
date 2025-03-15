using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterEffect/InstantEffect/Take Damage Effect")]
public class TakeDamage : InstantCharacterEffect
{
    [Header("Damage")] //各種屬性傷害
    public float physicalDamage;
    public float magicDamage;
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("FinalDamage")] //將各屬性加總後的最終傷害
    private float finalDamageDealt = 0;

    [Header("Poise")] //軀幹值
    public float poiseDamage = 0; //軀幹傷害
    public bool poiseIsBroken = false; //是否失衡

    [Header("Animation")]
    public bool playAnimation = true; //是否播放damageAnimation(例如中毒不會撥放)
    public bool manuallySelectAnimation = false; //是否手動選擇要撥放的動畫，否則撥放預設動畫
    public string damageAnimation; //動畫名稱

    [Header("Sound FX")]
    public bool willPlayDamageSFX = true; //是否播放damageSoundFx
    public AudioClip elemantalDamageSoundFX; //屬性傷害的音效 ex: fire, ice, lightning

    [Header("Direction Damage Taken From")]
    public float angleHitFrom; //受到攻擊的方向
    public Vector3 contactPoint; //腳色collider上受到攻擊的點(決定撥放出血等特效位置)

    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        if (character.isDead) return;
        
        //判斷是否在無敵幀
        CalculateFinalDamage(character);
        //check direction damage came from
        //play damage animation
        //play damage sound Fx
        //play damageVFX(blood, etc)
    }

    private void CalculateFinalDamage(CharacterManager character){
        finalDamageDealt = physicalDamage + magicDamage + fireDamage + iceDamage + lightningDamage;

        if (finalDamageDealt <= 0) finalDamageDealt = 1; //最低傷害為1 

        character.currentHealth -= finalDamageDealt;

        //calculate poise dmamge
    }
}
