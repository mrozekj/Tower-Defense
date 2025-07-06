using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretConstruction : MonoBehaviour {

    public static TurretConstruction main;  // Singleton instance for easy global access

    [Header("References")]
    [SerializeField] private TurretIndex[] towers;  //Array for all possible turret's types

    private int selectedTower = 0;

    private void Awake() {
        main = this;
    }

    public TurretIndex GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower) {
        selectedTower = _selectedTower;
    }
    
}
