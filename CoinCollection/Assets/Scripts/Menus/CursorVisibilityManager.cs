using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CursorVisibilityManager : MonoBehaviour
{
    private void Update()
    {
        // Show cursor if mouse is moved
        if (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // Hide cursor if any key or gamepad button is pressed
        else if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
                 (Gamepad.current != null && Gamepad.current.allControls.IndexOf(
                     c => c is ButtonControl btn && btn.wasPressedThisFrame) != -1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
