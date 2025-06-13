using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("UI Prompt")]
    [SerializeField] private GameObject promptUI;

    private bool playerInRange;

    [HideInInspector] public bool isInteractionBlocked = false; // 🚫 New Flag

    private void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false); // Always off at start
    }

    private void Update()
    {
        if (promptUI != null)
        {
            // Hide if blocked (e.g. during cutscene)
            if (isInteractionBlocked)
            {
                promptUI.SetActive(false);
                return;
            }

            // Show only when in range
            promptUI.SetActive(playerInRange);
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("🟩 Man Trigger Entered");
        playerInRange = true;
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("🟥 Man Trigger Exited");
        playerInRange = false;
    }
}


    public void SetPlayerInRange(bool inRange)
{
    playerInRange = inRange;
}

}
