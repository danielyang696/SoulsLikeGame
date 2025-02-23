using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance {get; private set;}
    [SerializeField] int WorldSenceIndex = 1;

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
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(WorldSenceIndex);
        yield return null;
    }
}
