using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour {

      [Header("References")]
      [SerializeField] private Rigidbody2D rb;  // Rigidbody2D component for physics and movement

    [Header("Attributes")]
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float projectileDamage = 1f;

     private Transform target;

     public void SetTarget(Transform _target) {   // Sets the target for the projectile
        target = _target;

     }

     private void FixedUpdate() {   // Updates the projectile's position based on the target
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * projectileSpeed;
     }

     private void OnTriggerEnter2D(Collider2D other) {   // Handel collision with other objects 
        EnemyHealth targetHealth = other.gameObject.GetComponent<EnemyHealth>(); 

        if (targetHealth != null) {
            targetHealth.TakeDamage(projectileDamage);
            Destroy(gameObject);  // Destroys projectile once it connects with the enemy 
        }
    }
}


