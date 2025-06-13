using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject player; // Assign PlayerCapsule
    [SerializeField] private Transform defaultSpawnPoint; // Assign default spawn

    private void Start()
    {
    if (SceneManager.GetActiveScene().name == "City 2" && 
        SceneManager.GetSceneByName("LinkUpMenu").isLoaded)
    {
        PlayerPositionTracker.Reset();
    }


        if (player == null || defaultSpawnPoint == null)
        {
            Debug.LogError("Player or DefaultSpawnPoint not assigned in PlayerSpawnManager!");
            return;
        }

        // Check if we have a saved spawn position
        if (PlayerPositionTracker.LastCityPosition != Vector3.zero)
        {
            player.transform.position = PlayerPositionTracker.LastCityPosition;
            Debug.Log("Player spawned at SAVED position: " + PlayerPositionTracker.LastCityPosition);
        }
        else
        {
            player.transform.position = defaultSpawnPoint.position;
            Debug.Log("Player spawned at DEFAULT position: " + defaultSpawnPoint.position);
        }
    }
}
