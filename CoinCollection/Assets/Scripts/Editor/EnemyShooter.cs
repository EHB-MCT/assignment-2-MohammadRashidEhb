using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile to shoot
    public Transform firePoint;         // The point where the projectile will be fired
    public float projectileSpeed = 10f; // Speed of the projectile
    public float shootInterval = 2f;    // Time between shots

    private Transform player;           // Reference to the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        InvokeRepeating("ShootProjectile", 1f, shootInterval);         // Repeatedly call ShootProjectile
    }

    private void ShootProjectile()
    {
        if (player == null) return;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        // Calculate the direction to the player
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Add velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        // Destroy the projectile after 5 seconds to clean up
        Destroy(projectile, 5f);
    }
}
