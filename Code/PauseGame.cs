using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

 [Header("References")]
    [SerializeField] private GameObject pauseMenuUI;  
    [SerializeField] private Button pauseButton;      
    [SerializeField] private Button resumeButton;     
    [SerializeField] private Button quitButton;   // Button to go back to the main menu

    public static bool isGamePaused = false; //public bool for Tile class to access 

    private void Start()
    {
        pauseMenuUI.SetActive(false);   // Hide the pause menu at the start of the game

        // Add listeners to all buttons
        pauseButton.onClick.AddListener(GameIsPaused);

        resumeButton.onClick.AddListener(ResumeGame);

        quitButton.onClick.AddListener(GoToMainMenu);
    }

    public void GameIsPaused()
    {
        // Pause the game
        Time.timeScale = 0f;          // Pauses game
        isGamePaused = true;
        pauseMenuUI.SetActive(true);  // Show the pause menu

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;          
        isGamePaused = false;
        pauseMenuUI.SetActive(false); 

    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;  // Resume time before navigating back to the main menu 

        isGamePaused = false;

        SceneManager.LoadScene(0);  // Main menu scene
    }
}
