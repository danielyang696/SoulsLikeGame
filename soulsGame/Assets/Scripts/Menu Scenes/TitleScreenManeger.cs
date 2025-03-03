using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManeger : MonoBehaviour
{
    public void StartNewGame(){
        StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
    }
}
