using UnityEngine.SceneManagement;
using UnityEngine;

public class WhatTheLevel : MonoBehaviour
{
    private int LevelsInt = 0;    
    private static WhatTheLevel instance;


    void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "CutEScene"){
            if (LevelsInt < 1){
                LevelsInt = 1;
                PlayerPrefs.SetInt("OpenedLevels", LevelsInt);
            }    
        }
        if (SceneManager.GetActiveScene().name == "Main"){
            if (LevelsInt < 2){
                LevelsInt = 2;
                PlayerPrefs.SetInt("OpenedLevels", LevelsInt);
            }    
        }
    }
}
