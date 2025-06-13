using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionController : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private float fadeDuration = 1f;

    public static SceneTransitionController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (loadingText != null)
            loadingText.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FadeAndLoadScene(string sceneName, string difficulty = "")
    {
        if (!string.IsNullOrEmpty(difficulty))
            PlayerPrefs.SetString("difficulty", difficulty);

        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        if (loadingText != null)
            loadingText.SetActive(true);

        float elapsed = 0;
        Color color = fadePanel.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }
        color.a = 1;
        fadePanel.color = color;

        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool isMenuScene = scene.name == "OfficeMenu" ||
                          scene.name == "LinkUpMenu" ||
                          scene.name == "SchoolMenu" ||
                          scene.name == "ScavengerHuntMenu";

        Cursor.lockState = isMenuScene ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuScene;

        // Reset fade panel on scene load
        if (fadePanel != null)
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0);
    }
}
