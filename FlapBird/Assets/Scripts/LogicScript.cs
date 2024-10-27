using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This scripts handles the game logic related to the player's score 
public class LogicScript : MonoBehaviour
{
    public int playerScore; // Variable to store score
    public Text scoreText; // Ref to the UI text component that displays the score

    // Call from the Unity editor to increase the score.
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore +scoreToAdd;
        scoreText.text = playerScore.ToString();
    }
}
