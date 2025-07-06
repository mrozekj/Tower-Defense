using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Button backToMenu;

    private void Start() {
        backToMenu.onClick.AddListener(BackToMenu);
    }

    public void BackToMenu() {
        Time.timeScale = 1f;        // resume game time to avoid issues with main menu   
        SceneManager.LoadScene(0);   // Scene 0 refers to Main menu
    }
 
}
