using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [Header("Default Buttons")]
    [SerializeField] private GameObject defaultButton;

    private GameObject lastSelectedObject;

    void Update()
    {
        // Track last selected UI object for controller
        if (EventSystem.current.currentSelectedGameObject != null)
            lastSelectedObject = EventSystem.current.currentSelectedGameObject;

        // Handle mouse clicks outside UI
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!IsPointerOverUI())
            {
                // Mouse clicked outside UI - reset to last controller selection
                if (lastSelectedObject != null)
                    EventSystem.current.SetSelectedGameObject(lastSelectedObject);
                else
                    EventSystem.current.SetSelectedGameObject(defaultButton);
            }
        }
    }

    // Helper method to check if pointer is over UI
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Call this when opening a menu
    public void SetDefaultButton(GameObject newDefault)
    {
        defaultButton = newDefault;
        EventSystem.current.SetSelectedGameObject(defaultButton);
        lastSelectedObject = defaultButton;
    }
}
