using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public KeyCode EscKey = KeyCode.Escape;
    public string HomeSceneName = "MainMenu";
    [SerializeField] private GameObject Canvas;
    private bool Paused = false;


    private void Update() {
        if (Input.GetKeyUp(EscKey)){
            if (!Paused){
                Canvas.SetActive(true);
                Time.timeScale = 0f;
                Paused = true;
            }
            else{
                Canvas.SetActive(false);
                Time.timeScale = 1f;
                Paused = false;
            }    
        }
    }

    public void BackHome(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(HomeSceneName);
    } 
}
