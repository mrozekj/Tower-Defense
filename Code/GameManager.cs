using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour {

    public static GameManager main;  // Singleton instance for easy global access

    public Transform startPoint;

    public Transform[] path;  // Array of points that define enemy's path

    public int credits;

    public int points;

    public int playerHealth;



    private void Awake() {
        main = this;
    }

    private void Start() {
        credits = 250;
        points = 0;
        playerHealth = 10;

    }

    public void IncreaseCredits(int amount) {
        credits += amount;
    }

    public void IncreasePoints(int amount) {
        points += amount;
    }

    public bool SpendCredits(int amount) {  //Calculate currency spent on turrets
        if (amount <= credits) {
            credits -= amount;
            return true;
        } else {
            Debug.Log("You do not have enough credits to but this");
            return false;
        }
    }

    public GameObject gameOverUI;

    public static bool gameOver = false;

    void Update() {
        if (gameOver) {   
            return;       // Exit the update if the game is over
        }
        if (playerHealth <= 0) {
            EndGame();
        }
    }

    void EndGame() {   // Pause game and activate game over screen 
        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetGameState()
    {
        gameOver = false;
    }
}
