using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;   // Used for navigation and AI 

public class TurretScript : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;  // Layer mask to filter which objects are considered enemies
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;  // Point where projectiles are fired from

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bulletPerSec = 1f;

    private Transform target;
    private float timeUntilFire;  // Timer to manage the firing rate

    private void Update() {
        if (target == null) {
            FindTarget();   // Find a new target if there is none
            return; 
        } 
        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;   // Clear the target if it's out of range
        } else {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bulletPerSec) {   // Shoot if the time interval has passed
                Shoot();
                timeUntilFire = 0f;   // Reset firing timer
            }
        }
    }

    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);  // Instantiate a bullet at the firing point
        Projectile bulletScript = bulletObj.GetComponent<Projectile>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        // Use a circle cast to detect enemies within the targeting range

        if (hits.Length > 0) {
            target = hits[0].transform; // First detected enemy set as a target
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;

    }

    private void RotateTowardsTarget() {
        // Calculate the angle between the turret and the target
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) *
        Mathf.Rad2Deg - 270f;

        Quaternion targetRotation = Quaternion.Euler( new Vector3( 0f, 0f, angle));    // Create a rotation based on the angle
        turretRotationPoint.rotation = targetRotation;   // Then apply rotation to the turret
    }

    private void OnDrawGizmosSelected() {   // Draw a wireframe disc in the editor to visualize the targeting range
#if UNITY_EDITOR
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
#endif
    }

}

