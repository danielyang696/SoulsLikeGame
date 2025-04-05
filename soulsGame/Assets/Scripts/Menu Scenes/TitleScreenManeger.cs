using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class TitleScreenManeger : MonoBehaviour
    {
        public void StartNewGame(){
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }
    }
}
