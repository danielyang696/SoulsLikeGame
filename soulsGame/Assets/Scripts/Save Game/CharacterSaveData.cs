using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    [System.Serializable]
    public class CharacterSaveData
    {
        [Header("Character name")]
        public string characterName;
        
        [Header("Time played")]
        public float timePlayed;

        [Header("Character position")]
        public Vector3 characterPosition;

        [Header("Stats")]
        public float currentHealth = 100f;
        public float currentStamina = 100f;

        [Header("Scene")]
        public int sceneIndex = 1;
    }
}

