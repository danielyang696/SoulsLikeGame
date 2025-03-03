using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectManager : MonoBehaviour
{
    CharacterManager character;
    protected virtual void Awake(){
        character = GetComponent<CharacterManager>();
    }

    public void ProcessInstantEffect(){
        
    }
}
