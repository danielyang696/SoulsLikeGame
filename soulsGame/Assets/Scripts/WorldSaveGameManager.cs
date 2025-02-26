using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance {get; private set;}
    [SerializeField] int WorldScenesIndex = 1;

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

    public IEnumerator LoadNewGame(){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(WorldScenesIndex);
        yield return null;
    }

    public int GetWorldScenesIndex(){
        return WorldScenesIndex;
    }
}
