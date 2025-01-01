using UnityEngine;
using UnityEngine.UI; // For showing the coin count in UI


// TO DO
public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;  // The total number of coins the player has collected
    public Text coinCountText; // Reference to a UI Text component to display the coin count (drag this in the inspector)

    // Add coins to the total coin count
    public void AddCoins(int value)
    {
        coinCount += value;

        // Update the UI text to show the new coin count
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " + coinCount.ToString();
        }
    }
}
