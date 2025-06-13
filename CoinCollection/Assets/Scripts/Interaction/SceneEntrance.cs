using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class SceneEntrance : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad = "YourSceneNameHere";
    public GameObject player; // Assign PlayerCapsule in Inspector

    [Header("UI")]
    public GameObject interactionPromptText;

    [Header("Fade Settings")]
    public GameObject fadePanel;
    public float fadeDuration = 1f;

    [Header("Building Tracking")]
    public string buildingName = "Office"; // Set unique name per building

    private bool isPlayerInZone = false;
    private bool isFading = false;
    private PlayerInput playerInput;
    private InputAction interactAction;

    [System.Obsolete]
    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            interactAction = playerInput.actions["Interact"];

        if (interactionPromptText != null)
            interactionPromptText.SetActive(false);

        if (fadePanel != null)
            fadePanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (interactAction != null)
            interactAction.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null)
            interactAction.Disable();
    }

    private void Update()
    {
        if (isPlayerInZone && !isFading)
        {
            if (interactionPromptText != null)
                interactionPromptText.SetActive(true);

            if (interactAction != null && interactAction.WasPerformedThisFrame())
            {
                // Save player position before teleport
                if (player != null)
                    PlayerPositionTracker.LastCityPosition = player.transform.position;
                else
                    Debug.LogError("Player reference not assigned!");

                StartCoroutine(FadeAndLoadScene());
            }
        }
        else if (!isFading)
        {
            if (interactionPromptText != null)
                interactionPromptText.SetActive(false);
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        if (interactionPromptText != null)
            interactionPromptText.SetActive(false);

        if (fadePanel != null)
        {
            fadePanel.SetActive(true);
            CanvasGroup cg = fadePanel.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = fadePanel.AddComponent<CanvasGroup>();

            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                yield return null;
            }
        }

        // Track building visit (saves to PlayerPrefs automatically)
        BuildingTracker.MarkBuildingVisited(buildingName);

        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInZone = false;
    }
}
