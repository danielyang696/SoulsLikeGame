using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayerStaminaManager playerStaminaManager;

    protected virtual void Awake() {
        playerStaminaManager = FindAnyObjectByType<PlayerStaminaManager>();
        DontDestroyOnLoad(this);
    }
}
