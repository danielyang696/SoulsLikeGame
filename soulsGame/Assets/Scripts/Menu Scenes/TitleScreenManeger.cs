using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class TitleScreenManeger : MonoBehaviour
    {
        public void StartNewGame(){
            WorldSaveGameManager.instance.CreateNewGame();
            
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }
    }
}
