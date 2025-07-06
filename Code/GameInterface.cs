using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInterface : MonoBehaviour {

    [Header("References")]
    [SerializeField] TextMeshProUGUI creditsUI;
    [SerializeField] TextMeshProUGUI pointsUI; 

    [SerializeField] TextMeshProUGUI finalPointsUI; // for game over screen
    [SerializeField] TextMeshProUGUI playerHealthUI;

    private void OnGUI() {
        creditsUI.text = GameManager.main.credits.ToString() + " $";
        pointsUI.text = GameManager.main.points.ToString() + " PTS";
        finalPointsUI.text = GameManager.main.points.ToString() + " PTS";
        playerHealthUI.text = "HP " + GameManager.main.playerHealth.ToString() + "/10";
    }

}

