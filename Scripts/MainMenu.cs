using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviour
{
    public string[] Worlds = {"CutEScene","Main"};
    [SerializeField] private bool SettingsPanelActive = false;
    [SerializeField] private bool LevelsPanelActive = false;
    [SerializeField] private bool PlayPanelActive = false;
    private bool NowChanging;
    private AudioSource audio;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject PlayPanel;
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private GameObject YouCantGoThisLevel;

    private KeyCode SlideButton; 

    private void Start() {
        audio = GetComponent<AudioSource>();
    }
    
    public void Play(){
        if (SettingsPanelActive){
            PlayPanel.SetActive(false);
            PlayPanelActive = false;
        }else{
            PlayPanel.SetActive(true);
            PlayPanelActive = true;
            SettingsPanel.SetActive(false);
        }
        if (audio != null) 
            audio.Play();
    }

    public void Settings(){

        if (SettingsPanelActive){
            SettingsPanel.SetActive(false);
            SettingsPanelActive = false;
        }else{
            SettingsPanel.SetActive(true);
            SettingsPanelActive = true;
            PlayPanel.SetActive(false);
        }
        if (audio != null) 
            audio.Play();
    }


    public void ExitGame(){
        if (audio != null) 
            audio.Play();
        Application.Quit();
    }


    public void Continue(){
        if (audio != null) 
            audio.Play();
        if (PlayerPrefs.GetInt("OpenedLevels") <= 1)
            SceneManager.LoadScene(Worlds[0]);
        else if (PlayerPrefs.GetInt("OpenedLevels") >=2){
            SceneManager.LoadScene(Worlds[1]);
        }    
    }

    public void NewGame () {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(Worlds[0]);
    }



    public void ChooseLevel(){
        if (LevelsPanelActive){
            LevelsPanel.SetActive(false);
            LevelsPanelActive = false;
        }else{
            LevelsPanel.SetActive(true);
            LevelsPanelActive = true;
            PlayPanel.SetActive(false);
        }
        if (audio != null) 
            audio.Play();
    }



    public void LevelZero() {
        YouCantGoThisLevel.SetActive(false);
        SceneManager.LoadScene(Worlds[0]);    
    }
    public void LevelOne(){
        if (PlayerPrefs.GetInt("OpenedLevels") >= 2){
            YouCantGoThisLevel.SetActive(false); 
            SceneManager.LoadScene(Worlds[1]);
        }                   
        else
            YouCantGoThisLevel.SetActive(true);             
    }
}
