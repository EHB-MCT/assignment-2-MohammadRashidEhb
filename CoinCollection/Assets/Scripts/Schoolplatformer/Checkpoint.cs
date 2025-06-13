using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the PlayerRespawn script on the player object
            PlayerRespawn playerRespawn = GameObject.FindWithTag("Player").GetComponent<PlayerRespawn>();
            
            if (playerRespawn != null)
            {
                // Set checkpoint to THIS checkpoint's position, not the player's
                playerRespawn.SetCheckpoint(transform.position);
                Debug.Log($"[Checkpoint] Updated to: {transform.position}");
            }
        }
    }
}
