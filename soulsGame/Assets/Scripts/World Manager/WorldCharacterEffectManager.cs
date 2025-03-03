using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharacterEffectManager : MonoBehaviour
{
    static WorldCharacterEffectManager instance;
    [SerializeField] List<InstantCharacterEffect> characterEffectsList;

    void Awake()
    {
        if (instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }

        GenerateEffectID();
    }

    private void GenerateEffectID(){
        for (int i = 0; i < characterEffectsList.Count; i++){
            characterEffectsList[i].effectID = i;
        }
    }


}
