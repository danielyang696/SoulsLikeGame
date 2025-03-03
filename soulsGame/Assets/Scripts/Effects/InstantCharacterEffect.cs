using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCharacterEffect : ScriptableObject
{
    [Header("Effect ID")]
    public int effectID;

    public virtual void ProcessEffect(CharacterManager character){
        
    }
}
