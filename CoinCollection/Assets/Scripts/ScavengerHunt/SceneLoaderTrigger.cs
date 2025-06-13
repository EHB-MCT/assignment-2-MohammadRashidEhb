using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using System.Collections;

public class SceneLoaderTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName = "City 2";
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private string interactionPrompt = "Leave";
    [SerializeField] private GameObject fadePanel; // Assign your FadePanel here
    [SerializeField] private float fadeDuration = 1f;

    private bool playerInZone = false;
    private bool isFading = false;

    [System.Obsolete]
    private void Start()
    {
        if (starterAssetsInputs == null)
            starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        if (fadePanel != null)
            fadePanel.SetActive(false);
    }

    private void Update()
    {
        if (playerInZone && !isFading && starterAssetsInputs != null && starterAssetsInputs.interact)
        {
            starterAssetsInputs.interact = false;
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Item_Name.instance?.EnableInteractionText(interactionPrompt);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            Item_Name.instance?.DisableInteractionText();
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isFading = true;
        if (fadePanel != null)
        {
            fadePanel.SetActive(true);
            CanvasGroup cg = fadePanel.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = fadePanel.AddComponent<CanvasGroup>();

            cg.alpha = 0f;
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                yield return null;
            }
            cg.alpha = 1f;
        }

        yield return new WaitForSecondsRealtime(0.2f); // Optional small delay
        SceneManager.LoadScene(sceneName);
    }
}
