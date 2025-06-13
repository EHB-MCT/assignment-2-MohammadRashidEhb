using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayCursorReset : MonoBehaviour
{
    [System.Obsolete]
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Optional: Reset PlayerInput control scheme
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
    }
}
