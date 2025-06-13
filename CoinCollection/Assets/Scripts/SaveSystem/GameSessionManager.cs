using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    private void Start()
    {
        // Reset building progress at the start of each City 2 session
        BuildingTracker.ResetProgress();
        Debug.Log("Building progress reset for new session");
    }
}
