using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float enemyHP = 2f;
    [SerializeField] private int creditsWorth = 50;
    [SerializeField] private int pointsWorth = 50;

    [SerializeField] public Image healthBar;  // Health bar above enemy

    private bool isDestroyed = false;

    private float startingEnemyHP;

    void Start () {
        startingEnemyHP = enemyHP;
    }

    public void TakeDamage(float dmg) {
        enemyHP -= dmg;

        healthBar.fillAmount = enemyHP / startingEnemyHP;  // Because fill amount in unity goes from 0 to 1

        if (enemyHP <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            GameManager.main.IncreaseCredits(creditsWorth);
            GameManager.main.IncreasePoints(pointsWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
