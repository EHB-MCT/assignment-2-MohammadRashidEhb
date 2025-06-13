using System.Collections;
using StarterAssets;
using UnityEngine;

public class BoothInteraction : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject confirmationMessage;
    [SerializeField] private GameObject missingItemsMessage;
    [SerializeField] private GameObject appointmentCanvas;

    [Header("Input Reference")]
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    private bool playerInRange = false;

    private void Start()
    {
        if (starterAssetsInputs == null)
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        if (confirmationMessage != null)
            confirmationMessage.SetActive(false);

        if (missingItemsMessage != null)
            missingItemsMessage.SetActive(false);

        if (appointmentCanvas != null)
            appointmentCanvas.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && starterAssetsInputs.interact)
        {
            TryInteract();
            starterAssetsInputs.interact = false;
        }
    }

    private void TryInteract()
    {
        if (InventoryManager.Instance == null)
            return;

        if (InventoryManager.Instance.AllItemsCollected())
        {
            ConfirmAppointment();
        }
        else
        {
            StartCoroutine(ShowTemporaryWarning());
        }
    }

    private void ConfirmAppointment()
    {
        Debug.Log("✅ Appointment confirmed!");

        // Show the appointment text briefly, no dismiss button needed
        InventoryManager.Instance.ShowAppointmentGuide();

        // Optionally, you can still show a confirmation message if you want
        if (confirmationMessage != null)
            confirmationMessage.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideAppointmentMessage()
    {
        if (appointmentCanvas != null)
            appointmentCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private IEnumerator ShowTemporaryWarning()
    {
        if (missingItemsMessage != null)
        {
            Debug.Log("⚠ Showing missing items warning!");
            missingItemsMessage.SetActive(true);
            yield return new WaitForSeconds(3f);
            missingItemsMessage.SetActive(false);
        }
    }
}
