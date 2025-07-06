using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Button playGame;
    [SerializeField] private Button quitGame;

    private void Start() {
        playGame.onClick.AddListener(PlayGame);

        quitGame.onClick.AddListener(QuitGame);
    }

    public void PlayGame () {
        SceneManager.LoadScene(1);   // Move to game scene (scene 1)
        GameManager.main?.ResetGameState();  // Reset game stats 
   }

   public void QuitGame() {
    // If running in the Unity editor, stop the play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If in a build, quit the application
        Application.Quit();   // Only for PC version 
#endif

   }

}

