using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class CutsceneTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayableDirector cutsceneDirector;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject cutsceneCamera;
    [SerializeField] private GameObject cutsceneAkaza;
    [SerializeField] private GameObject interactionUIPrompt;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject skipPromptText;

    private StarterAssets.ThirdPersonController controller;
    private StarterAssets.StarterAssetsInputs input;
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private InteractionUI interactionUIScript;
    private InputAction skipAction;
    private bool cutscenePlaying = false;

    private bool playerInRange = false;

    private void Start()
    {
        controller = player.GetComponent<StarterAssets.ThirdPersonController>();
        input = player.GetComponent<StarterAssets.StarterAssetsInputs>();
        playerInput = player.GetComponent<PlayerInput>();
        playerAnimator = player.GetComponent<Animator>();
        interactionUIScript = GetComponent<InteractionUI>();

        if (cutsceneAkaza != null)
            cutsceneAkaza.SetActive(false);

        if (interactionUIPrompt != null)
            interactionUIPrompt.SetActive(false);

        if (playerInput != null)
        {
            skipAction = playerInput.actions["SkipCutscene"];
        }

        if (skipPromptText != null)
            skipPromptText.SetActive(false);
    }

    private void OnEnable()
    {
        if (skipAction != null)
            skipAction.Enable();
    }

    private void OnDisable()
    {
        if (skipAction != null)
            skipAction.Disable();
    }

    private void Update()
    {
        if (playerInRange)
        {
            Debug.Log("Player in range"); // Track range detection

            if (input != null)
            {
                Debug.Log($"Interact input value: {input.interact}"); // Show input state

                if (input.interact)
                {
                    Debug.Log("Interact button pressed!"); // Confirm button press
                    PlayCutscene();
                    input.interact = false; // Reset so it only triggers once per press
                }
            }
            else
            {
                Debug.LogError("StarterAssetsInputs component missing on player!");
            }
        }

        if (interactionUIPrompt != null)
            interactionUIPrompt.SetActive(playerInRange);

        // Check for skip input during cutscene
        if (cutscenePlaying && skipAction != null && skipAction.WasPerformedThisFrame())
        {
            Debug.Log("Skip input detected! Fast-forwarding cutscene.");
            cutsceneDirector.time = cutsceneDirector.duration - 0.1f; // Fast forward to end
        }
    }

   private void PlayCutscene()
{
    Debug.Log("Attempting to play cutscene");

    if (cutsceneDirector == null)
    {
        Debug.LogError("Cutscene Director reference missing!");
        return;
    }

    // Disable gameplay components, but KEEP PlayerInput enabled for skip functionality
    controller.enabled = false;
    input.enabled = false;
    // playerInput.enabled = false; // ❌ DON'T disable this - we need it for skip

    // Hide visuals
    SetPlayerVisible(false);
    if (playerAnimator != null)
        playerAnimator.enabled = false;

    // Hide main UI (but not cutscene UI)
    if (interactionUIPrompt != null)
        interactionUIPrompt.SetActive(false);

    if (uiCanvas != null)
        uiCanvas.SetActive(false); // Main gameplay UI

    // Enable cutscene version
    if (cutsceneAkaza != null)
        cutsceneAkaza.SetActive(true);

    // Switch cameras
    if (playerFollowCamera != null)
        playerFollowCamera.SetActive(false);
    if (cutsceneCamera != null)
        cutsceneCamera.SetActive(true);

    // Play cutscene
    cutsceneDirector.Play();
    cutsceneDirector.stopped += OnCutsceneEnded;

    cutscenePlaying = true;

    // Show skip prompt (make sure this is NOT a child of the disabled uiCanvas)
    if (skipPromptText != null)
        skipPromptText.SetActive(true);

    Debug.Log("Cutscene started playing");
}

private void OnCutsceneEnded(PlayableDirector director)
{
    Debug.Log("Cutscene ended");

    // Re-enable gameplay
    controller.enabled = true;
    input.enabled = true;
    playerInput.enabled = true; // Re-enable if you were disabling it

    SetPlayerVisible(true);
    if (playerAnimator != null)
        playerAnimator.enabled = true;

    // Hide cutscene Akaza
    if (cutsceneAkaza != null)
        cutsceneAkaza.SetActive(false);

    // Switch cameras
    if (playerFollowCamera != null)
        playerFollowCamera.SetActive(true);
    if (cutsceneCamera != null)
        cutsceneCamera.SetActive(false);

    // Re-enable main UI
    if (uiCanvas != null)
        uiCanvas.SetActive(true);

    cutscenePlaying = false;

    if (skipPromptText != null)
        skipPromptText.SetActive(false);

    cutsceneDirector.stopped -= OnCutsceneEnded;
}

    private void SetPlayerVisible(bool visible)
    {
        foreach (var renderer in player.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = visible;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name}");
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionUIScript != null)
                interactionUIScript.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Trigger exited by: {other.gameObject.name}");
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionUIScript != null)
                interactionUIScript.SetPlayerInRange(false);
        }
    }
}
