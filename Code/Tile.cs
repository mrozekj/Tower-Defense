using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;  // SpriteRenderer component for changing the tile's color
    [SerializeField] private Color hoverColor;

    private GameObject tower;  // Reference to the tower currently placed on this tile
    private Color startColor;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {  
        if (PauseGame.isGamePaused) return;  // Check if the game is paused or game over to prevent interaction
        if (GameManager.gameOver) return;
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        if (PauseGame.isGamePaused) return;
        if (GameManager.gameOver) return;
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (PauseGame.isGamePaused) return;
        if (GameManager.gameOver) return;
        if (tower != null) return;   // Prevent placing another tower if there's one already there

        TurretIndex towerToBuild = TurretConstruction.main.GetSelectedTower();

        if (towerToBuild.cost > GameManager.main.credits) {
            Debug.Log("You can't afford this tower");
            return;
        }

        GameManager.main.SpendCredits(towerToBuild.cost);  // Deduct the cost of the tower from the player's credits
 
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }

}
