using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

namespace SG{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance {get; private set;}
        [SerializeField] PlayManager player;

        [Header("bool to test load/save")]
        [SerializeField] bool loadGame = false;
        [SerializeField] bool saveGame = false;


        [Header("World Sences Index")]
        [SerializeField] int WorldScenesIndex = 1;

        [Header("SaveDataWriter")]
        private SaveDataWriter saveDataWriter;

        [Header("Current Character data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName = "";

        [Header("Character Slot")]
        public CharacterSaveData characterSlot1;
        /*
        public CharacterSaveData characterSlot2;
        public CharacterSaveData characterSlot3;
        public CharacterSaveData characterSlot4;
        public CharacterSaveData characterSlot5;
        public CharacterSaveData characterSlot6;
        public CharacterSaveData characterSlot7;
        public CharacterSaveData characterSlot8;
        public CharacterSaveData characterSlot9;
        public CharacterSaveData characterSlot10;
        */

        void Awake()
        {
            if (instance == null){
                instance = this;
            }else{
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (loadGame){
                LoadGame();
                loadGame = false;
            }

            if (saveGame){
                SaveGame();
                saveGame = false;
            }
        }

        private void DecideCharacterFileNameOnCharacterSlotBeingUsed(){
            switch (currentCharacterSlotBeingUsed){
                case CharacterSlot.CharacterSlot_01:
                    saveFileName = "CharacterSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    saveFileName = "CharacterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    saveFileName = "CharacterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    saveFileName = "CharacterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    saveFileName = "CharacterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    saveFileName = "CharacterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    saveFileName = "CharacterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    saveFileName = "CharacterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    saveFileName = "CharacterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_10:
                    saveFileName = "CharacterSlot_10";
                    break;
                default:
                    break;
            }
        }

        public void CreateNewGame(){
            //Decide which character slot is being used and Create a new file
            DecideCharacterFileNameOnCharacterSlotBeingUsed();

            currentCharacterData = new CharacterSaveData();
        }

        public void LoadGame(){
            //read the file name from the character slot being used and Load the file
            DecideCharacterFileNameOnCharacterSlotBeingUsed();

            saveDataWriter = new SaveDataWriter();

            saveDataWriter.saveFilePath = Application.persistentDataPath;
            saveDataWriter.saveFileName = saveFileName;

            currentCharacterData = saveDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        public void SaveGame(){
            //read the file name from the character slot being used and save the file
            DecideCharacterFileNameOnCharacterSlotBeingUsed();

            saveDataWriter = new SaveDataWriter();

            saveDataWriter.saveFilePath = Application.persistentDataPath;
            saveDataWriter.saveFileName = saveFileName;

            //pass the character info to curentCharacterData
            player.SaveGameToCurrentCharacterData(ref currentCharacterData);

            //write currentCharacterData to json and saved in computer
            saveDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public IEnumerator LoadWorldScene(){
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(WorldScenesIndex);
            yield return null;
        }

        public int GetWorldScenesIndex(){
            return WorldScenesIndex;
        }
    }
}
