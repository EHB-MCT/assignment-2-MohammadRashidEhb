using UnityEngine;
using TMPro;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject playerCapsule;
    [SerializeField] private GameObject leavePrompt;
    [SerializeField] private StarterAssets.StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private AudioSource backgroundMusic; // <-- NEW

    private bool playerInZone = false;

    [System.Obsolete]
    private void Start()
    {
        if (leavePrompt != null)
            leavePrompt.SetActive(false);

        if (starterAssetsInputs == null)
            starterAssetsInputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
    }

    private void Update()
    {
        if (playerInZone && starterAssetsInputs != null && starterAssetsInputs.interact)
        {
            AttemptTeleport();
            starterAssetsInputs.interact = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && InventoryManager.Instance != null && InventoryManager.Instance.AllItemsCollected())
        {
            playerInZone = true;
            if (leavePrompt != null)
                leavePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            if (leavePrompt != null)
                leavePrompt.SetActive(false);
        }
    }

    private void AttemptTeleport()
    {
        CharacterController controller = playerCapsule.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            playerCapsule.transform.position = teleportTarget.position;
            playerCapsule.transform.rotation = teleportTarget.rotation;
            controller.enabled = true;
        }

        // STOP BACKGROUND MUSIC
        if (backgroundMusic != null)
            backgroundMusic.Stop();

        if (leavePrompt != null)
            leavePrompt.SetActive(false);

        Debug.Log("🚪 Player teleported to next room.");
        playerInZone = false;
    }
}
