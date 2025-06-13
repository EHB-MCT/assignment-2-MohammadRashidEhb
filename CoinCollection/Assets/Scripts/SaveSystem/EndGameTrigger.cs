using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "LinkUpMenu";
    [SerializeField] private GameObject fadePanel; // Assign your local fade panel
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private int totalBuildingsRequired = 3; // Set as needed

    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading && BuildingTracker.AllBuildingsVisited(totalBuildingsRequired))
        {
            StartCoroutine(FadeAndLoadMenu());
        }
    }

    private IEnumerator FadeAndLoadMenu()
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

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(menuSceneName);
    }
}
