using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace SG{
    public class SaveDataWriter
    {
        public string saveFilePath = "";
        public string saveFileName = "";

        //before create a new file, check if the save file exists
        public bool CheckToSeeFileExists(){
            if (File.Exists(Path.Combine(saveFilePath, saveFileName))){
                return true;
            }else{
                return false;
            }
        }

        //Use to delete character save file
        public void DeleteSaveFile(){
            File.Delete(Path.Combine(saveFilePath, saveFileName));
        }

        //Use to create a save file to save file
        public void CreateNewCharacterSaveFile(CharacterSaveData characterSaveData){
            //Made a path to save file(on computer
            string savePath = Path.Combine(saveFilePath, saveFileName);

            try{
                //Create a directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(saveFilePath));
                Debug.Log("createing save file at:" + savePath);

                //Serialize the c# game data to json
                string dataToStore = JsonUtility.ToJson(characterSaveData, true);
                
                //Write the json data to a file
                using (FileStream Stream = new FileStream(savePath, FileMode.Create)){
                    using (StreamWriter fileWriter = new StreamWriter(Stream)){
                        fileWriter.Write(dataToStore);
                    }
                }
            }catch (Exception ex){
                Debug.LogError("Error creating save file: " + ex.Message);
            }
        }

        //Use to Load game
        public CharacterSaveData LoadSaveFile(){
            CharacterSaveData characterSaveData = null;

            //Made a path to load(on computer
            string loadPath = Path.Combine(saveFilePath, saveFileName);

            if (File.Exists(loadPath)){
                try{
                    string dataToLoad = "";

                    //Read the json data from a file
                    using (FileStream Stream = new FileStream(loadPath, FileMode.Open)){
                        using (StreamReader fileReader = new StreamReader(Stream)){
                            dataToLoad = fileReader.ReadToEnd();
                        }
                    }

                    //Deserialize the json data to c# game data
                    characterSaveData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);  
                }catch (Exception ex){
                    Debug.LogError("Error loading save file: " + ex.Message);
                }
            }

            return characterSaveData;
        }
    }
}

