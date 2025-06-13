using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayCursorSetup : MonoBehaviour
{
    [System.Obsolete]
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Force Keyboard&Mouse control scheme if using PlayerInput
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
    }
}
