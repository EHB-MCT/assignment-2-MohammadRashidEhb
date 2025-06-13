using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Item Tracking")]
    public int totalItems = 7;
    private int itemsCollected = 0;

    [Header("UI")]
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject dismissSuccessButton;
    [SerializeField] private GameObject appointmentCanvas;
    [SerializeField] private float appointmentDisplayTime = 3f; // seconds

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (successScreen) successScreen.SetActive(false);
        if (appointmentCanvas) appointmentCanvas.SetActive(false);
    }

    [System.Obsolete]
    public void ItemCollected()
    {
        itemsCollected++;
        Debug.Log($"Item Collected: {itemsCollected}/{totalItems}");
        if (itemsCollected >= totalItems)
            ShowSuccess();
    }

    [System.Obsolete]
    private void ShowSuccess()
    {
        if (successScreen) successScreen.SetActive(true);
        if (dismissSuccessButton)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(dismissSuccessButton);
        }
        FindObjectOfType<MissionTimer>()?.StopTimer();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Call this to show the appointment guide text for a few seconds
    public void ShowAppointmentGuide()
    {
        if (appointmentCanvas)
        {
            appointmentCanvas.SetActive(true);
            StartCoroutine(HideAppointmentGuideAfterDelay());
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator HideAppointmentGuideAfterDelay()
    {
        yield return new WaitForSeconds(appointmentDisplayTime);
        if (appointmentCanvas)
            appointmentCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HideSuccessScreen()
    {
        if (successScreen) successScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ResetInventory() => itemsCollected = 0;
    public bool AllItemsCollected() => itemsCollected >= totalItems;
}
