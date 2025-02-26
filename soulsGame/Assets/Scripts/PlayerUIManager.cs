using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager istance;

    void Awake()
    {
        if (istance == null){
            istance = this;
        }else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
