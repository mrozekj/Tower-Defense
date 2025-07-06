using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;   // RigidBody2D controls movement within Unity

    [Header("Attributes")]

    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start(){
        target = GameManager.main.path[pathIndex];   // Called at the beginning of the game sends an enemy to initial point
    }

    private void Update(){          // Check if enemy reached the current target and move them along to the next, if close enough 
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == GameManager.main.path.Length) {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);   // Remove enemy once it reached the final point 
                GameManager.main.playerHealth--;  // remove health from player's HP
                return;
            } else {
                 target = GameManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate() {   // Smooth out enemy movement 
        Vector2 direction = (target.position - transform.position).normalized;

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

    }


}
