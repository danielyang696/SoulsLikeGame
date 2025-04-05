using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager istance;
        [Header("References")]
        public PlayerHUDManager playerHUDManager;
        public PopUpManager popUpManager;

        void Awake()
        {
            if (istance == null){
                istance = this;
            }else{
                Destroy(gameObject);
            }

            playerHUDManager = GetComponentInChildren<PlayerHUDManager>();
            popUpManager = GetComponentInChildren<PopUpManager>();
        }

        void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
