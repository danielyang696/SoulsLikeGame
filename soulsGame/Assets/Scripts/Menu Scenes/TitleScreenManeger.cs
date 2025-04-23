using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

namespace SG{
    public class TitleScreenManeger : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadGameMenu;

        [Header("Buttons")]
        [SerializeField] Button loadGameMenuReturnButton;
        [SerializeField] Button MainMenuLoadGameButton;

        public void StartNewGame(){
            WorldSaveGameManager.instance.CreateNewGame();
            
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }

        public void OpenLoadGameMenu(){
            //Disable the main menu
            titleScreenMainMenu.SetActive(false);

            //Open the load game menu
            titleScreenLoadGameMenu.SetActive(true);

            loadGameMenuReturnButton.Select();
        }

        public void CloseLoadGameMenu(){
            //Disable the load game menu
            titleScreenLoadGameMenu.SetActive(false);

            //Open the main menu
            titleScreenMainMenu.SetActive(true);

            MainMenuLoadGameButton.Select();
        }
    }
}
