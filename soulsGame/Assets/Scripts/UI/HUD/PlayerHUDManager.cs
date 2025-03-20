using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{
    public PlayerHealthManager playerHealthManager;
    public PlayerStaminaManager playerStaminaManager;

    void Awake()
    {
        playerHealthManager = GetComponentInChildren<PlayerHealthManager>();
        playerStaminaManager = GetComponentInChildren<PlayerStaminaManager>();
    }
}
