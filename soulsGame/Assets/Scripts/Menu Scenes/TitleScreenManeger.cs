using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

namespace SG{
    public class TitleScreenManeger : MonoBehaviour
    {
        public static TitleScreenManeger instance;

        [Header("Menu")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadGameMenu;

        [Header("Menu Buttons")]
        [SerializeField] Button loadGameMenuReturnButton;
        [SerializeField] Button MainMenuLoadGameButton;

        [SerializeField] Button MainMenuNewGameButton;

        [Header("Pop Up")]
        [SerializeField] GameObject noCharacterSlotPopUp;
        [SerializeField] Button noCharacterSlotPopUpOkButton;


        void Awake()
        {
            if (instance == null){
                instance = this;
            }else{
                Destroy(gameObject);
            }
        }

        public void StartNewGame(){
            WorldSaveGameManager.instance.CreateNewGame();
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


        public void DisPlayNoSlotPopUp(){
            noCharacterSlotPopUp.SetActive(true);
            noCharacterSlotPopUpOkButton.Select();
        }

        public void CloseNoSlotPopUp(){
            noCharacterSlotPopUp.SetActive(false);
            MainMenuLoadGameButton.Select();
            MainMenuNewGameButton.Select();
        }   
    }
}
