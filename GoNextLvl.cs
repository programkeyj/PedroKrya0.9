using UnityEngine.SceneManagement;
using UnityEngine;

public class GoNextLvl : MonoBehaviour
{
    public string Scene;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            SceneManager.LoadScene(Scene);
        }
    }
}
