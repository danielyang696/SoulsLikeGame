using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class TitleScreenInputManager : MonoBehaviour
    {
        PlayerControls playerControls;
        [Header("Title Screen Input")]
        public bool deleteCharacterSlot = false;

        void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.UI.mouseright.performed += i => deleteCharacterSlot = true;
            }
            playerControls.Enable();
        }

        void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            if (deleteCharacterSlot)
            {
                deleteCharacterSlot = false;
                TitleScreenManeger.instance.AttemptDeleteCharacterSlot();
                Debug.Log("Delete Character Slot");
            }
        }
    }
}
