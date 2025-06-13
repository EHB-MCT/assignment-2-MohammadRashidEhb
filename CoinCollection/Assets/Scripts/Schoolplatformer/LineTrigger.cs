using UnityEngine;
using UnityEngine.InputSystem; // Needed for PlayerInput

public class LineTrigger : MonoBehaviour
{
    public enum LineType { Start, Finish }
    [SerializeField] private LineType lineType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lineType == LineType.Start)
            {
                Debug.Log("[LineTrigger] Player crossed the START line!");
                if (GameTimer.Instance != null)
                {
                    GameTimer.Instance.StartTimer();
                }
            }
            else if (lineType == LineType.Finish)
            {
                Debug.Log("[LineTrigger] Player crossed the FINISH line!");
                if (GameTimer.Instance != null)
                {
                    GameTimer.Instance.FinishTimer();
                }

                // Disable player input if using the new Input System
                var playerInput = other.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.enabled = false;
                }

                // Get stats
                string time = GameTimer.Instance.GetFormattedTime();
                int falls = FindFirstObjectByType<PlayerRespawn>().fallCount;

                // Show finish UI
                if (FinishUIController.Instance != null)
                {
                    FinishUIController.Instance.ShowFinishScreen(time, falls);
                }
            }
        }
    }
}
