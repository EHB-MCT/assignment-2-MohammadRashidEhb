using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ControllerRefocus : MonoBehaviour
{
    private GameObject lastSelected;
    private bool isUsingController;

    void Start()
    {
        InitializeSelection();
    }

    void Update()
    {
        CheckInputType();
        MaintainControllerSelection();
    }

    private void InitializeSelection()
    {
        if (EventSystem.current.currentSelectedGameObject == null && 
            EventSystem.current.firstSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    private void CheckInputType()
    {
        // Detect controller input
        bool controllerInput = Gamepad.current != null && 
            (Gamepad.current.leftStick.ReadValue().magnitude > 0.1f ||
             Gamepad.current.buttonSouth.isPressed);

        // Detect keyboard input
        bool keyboardInput = Keyboard.current != null && 
            Keyboard.current.anyKey.isPressed;

        // Detect mouse movement
        bool mouseMoved = Mouse.current != null && 
            Mouse.current.delta.ReadValue().sqrMagnitude > 0;

        isUsingController = controllerInput || keyboardInput;
        
        // Show/hide cursor based on input
        if (mouseMoved)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (isUsingController)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void MaintainControllerSelection()
    {
        if (isUsingController)
        {
            if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
            else
            {
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
