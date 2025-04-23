using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

namespace SG{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;
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
        private string savefileName = "";

        [Header("Character Slot")]
        public CharacterSaveData characterSlot1;
        public CharacterSaveData characterSlot2;
        public CharacterSaveData characterSlot3;
        public CharacterSaveData characterSlot4;
        public CharacterSaveData characterSlot5;
        public CharacterSaveData characterSlot6;
        public CharacterSaveData characterSlot7;
        public CharacterSaveData characterSlot8;
        public CharacterSaveData characterSlot9;
        public CharacterSaveData characterSlot10;
        

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
            LoadAllSlotProfiles();
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

        public string DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot characterSlot){
            string fileName = "";   
            
            switch (characterSlot){
                case CharacterSlot.CharacterSlot_01:
                    fileName = "CharacterSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    fileName = "CharacterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    fileName = "CharacterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    fileName = "CharacterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    fileName = "CharacterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    fileName = "CharacterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    fileName = "CharacterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    fileName = "CharacterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    fileName = "CharacterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_10:
                    fileName = "CharacterSlot_10";
                    break;
                default:
                    break;

            }
            return fileName;    
        }

        public void CreateNewGame(){
            //Decide which character slot is being used and Create a new file
            savefileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            currentCharacterData = new CharacterSaveData();
        }

        public void LoadGame(){
            //read the file name from the character slot being used and Load the file
            savefileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveDataWriter = new SaveDataWriter();

            saveDataWriter.saveFilePath = Application.persistentDataPath;
            saveDataWriter.saveFileName = savefileName;

            currentCharacterData = saveDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        public void SaveGame(){
            //read the file name from the character slot being used and save the file
            savefileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveDataWriter = new SaveDataWriter();

            saveDataWriter.saveFilePath = Application.persistentDataPath;
            saveDataWriter.saveFileName = savefileName;

            //pass the character info to curentCharacterData
            player.SaveGameToCurrentCharacterData(ref currentCharacterData);

            //write currentCharacterData to json and saved in computer
            saveDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        private void LoadAllSlotProfiles(){
            //Load all character slot profiles
            saveDataWriter = new SaveDataWriter();
            saveDataWriter.saveFilePath = Application.persistentDataPath;
            
            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
            characterSlot1 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
            characterSlot2 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
            characterSlot3 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
            characterSlot4 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
            characterSlot5 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);
            characterSlot6 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);
            characterSlot7 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);
            characterSlot8 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);
            characterSlot9 = saveDataWriter.LoadSaveFile();

            saveDataWriter.saveFileName = DecideCharacterFileNameOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);
            characterSlot10 = saveDataWriter.LoadSaveFile();
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
