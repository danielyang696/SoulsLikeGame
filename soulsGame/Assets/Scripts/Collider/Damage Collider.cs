using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Damage")] //各種屬性傷害
    public float physicalDamage = 25f;
    public float magicDamage;
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("Characters Damaged")]
    public List<CharacterManager> charactersDamaged = new List<CharacterManager>(); //儲存已經受到傷害的腳色，為了讓腳色不會受到重覆傷害

    [Header("Contact Point")]
    protected Vector3 contactPoint;

    void OnTriggerEnter(Collider other)
    {
        CharacterManager damageTarget = other.GetComponent<CharacterManager>();

        if (damageTarget != null){
            contactPoint = other.GetComponent<Collider>().ClosestPointOnBounds(transform.position); //設定contactPoint為other.collider上離此物件的Position最近的點

            //Check if target is blocking
            //是否在無敵幀
            
            DamageTarger(damageTarget);
        } 
    }

    protected virtual void DamageTarger(CharacterManager damageTarget){
        if (charactersDamaged.Contains(damageTarget)) return; //如果已經受到傷害，就不重複判定

        charactersDamaged.Add(damageTarget); //將受到傷害的腳色加到清單

        TakeDamage damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);

        damageEffect.contactPoint = contactPoint;
        damageEffect.physicalDamage = physicalDamage;
        damageEffect.magicDamage = magicDamage;
        damageEffect.fireDamage = fireDamage;
        damageEffect.iceDamage = iceDamage;
        damageEffect.lightningDamage = lightningDamage;

        damageTarget.characterEffectManager.ProcessInstantEffect(damageEffect);
    }
}
