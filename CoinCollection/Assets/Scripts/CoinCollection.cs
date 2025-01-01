using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int coinValue = 1; // Value of the coin (default is 1)
    private CoinManager coinManager; // Reference to the CoinManager to update the coin count

    // Sound to play when coin is collected
    public AudioClip collectSound;
    private AudioSource audioSource;

    void Start()
    {
        // TO DO
        coinManager = FindObjectOfType<CoinManager>();

        // Get reference to AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // Trigger event when player touches the coin
    void OnTriggerEnter(Collider other)
    {
        // Check if the player touched the coin
        if (other.CompareTag("Player"))
        {
            // Play the coin collection sound if assigned
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // Add the coin value to the player's coin count
            if (coinManager != null)
            {
                coinManager.AddCoins(coinValue);
            }

            // Destroy the coin after it is collected (optional delay to allow sound to finish)
            Destroy(gameObject, 0.2f);  // Wait for a short time before destroying the coin
        }
    }
}
