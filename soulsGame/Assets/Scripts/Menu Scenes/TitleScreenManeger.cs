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
        [SerializeField] Button deleteSlotPopUpConfirmButton;

        [Header("Pop Up")]
        [SerializeField] GameObject noCharacterSlotPopUp;
        [SerializeField] Button noCharacterSlotPopUpOkButton;
        
        [SerializeField] GameObject deleteSlotPopUp;


        [Header("Character Slot")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.No_Slot;

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


        //[CharacterSlot]
        public void DisPlayNoSlotPopUp(){
            noCharacterSlotPopUp.SetActive(true);
            noCharacterSlotPopUpOkButton.Select();
        }

        public void CloseNoSlotPopUp(){
            noCharacterSlotPopUp.SetActive(false);
            MainMenuLoadGameButton.Select();
            MainMenuNewGameButton.Select();
        }

        public void SelectCharacterSlot(CharacterSlot characterSlot){
            currentSelectedSlot = characterSlot;
        }

        //當return button被select時呼叫此方法更新TitleScreenManeger的currentSelectedSlot
        public void SelectNoSlot(){
            currentSelectedSlot = CharacterSlot.No_Slot;
        }

        public void AttemptDeleteCharacterSlot(){
            //Check if the slot is empty
            if (currentSelectedSlot != CharacterSlot.No_Slot){
                deleteSlotPopUp.SetActive(true);

                deleteSlotPopUpConfirmButton.Select();
            }
        }

        public void DeleteCharacterSlot(){
            deleteSlotPopUp.SetActive(false);
            WorldSaveGameManager.instance.DeleteCharacterSlot(currentSelectedSlot);
            titleScreenLoadGameMenu.SetActive(false);
            titleScreenLoadGameMenu.SetActive(true);
            loadGameMenuReturnButton.Select();
        }

        public void CloseDeleteSlotPopUp(){
            deleteSlotPopUp.SetActive(false);
            loadGameMenuReturnButton.Select();
        } 
    }
}
