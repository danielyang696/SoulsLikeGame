using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace SG
{
    public class UI_Characte_Slot : MonoBehaviour
    {
        SaveDataWriter saveDataWriter;

        [Header("Game Slot")]
        public CharacterSlot characterSlot;

        [Header("Character Info")]
        public TextMeshProUGUI CharacterNameText;
        public TextMeshProUGUI TimePlayedText;


        void OnEnable()
        {
            LoadSaveSlot();
        }

        private void LoadSaveSlot(){
            saveDataWriter = new SaveDataWriter();
            saveDataWriter.saveFilePath = Application.persistentDataPath;

            switch(characterSlot){
                case CharacterSlot.CharacterSlot_01:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot1.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_02:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot2.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_03:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot3.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_04:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot4.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_05:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot5.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_06:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot6.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_07:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot7.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_08:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot8.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_09:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot9.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlot.CharacterSlot_10:
                    saveDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameOnCharacterSlotBeingUsed(characterSlot);

                    // if file exists, get info from instance
                    if (saveDataWriter.CheckToSeeFileExists()){
                        CharacterNameText.text = WorldSaveGameManager.instance.characterSlot10.characterName;
                    // if not exists, disable the game object
                    }else{
                        Debug.Log("Character" );
                        gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }

        public void LoadGameFromCharacterSlot(){
            WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = characterSlot;
            WorldSaveGameManager.instance.LoadGame();
        }
    }
}


