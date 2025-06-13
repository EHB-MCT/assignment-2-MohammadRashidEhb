using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile hits the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
            // Optionally, apply damage or other effects here

            // Destroy the projectile on impact
            Destroy(gameObject);
        }

        // Destroy the projectile if it hits anything else (optional)
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject, 5f);
        }
    }
}
