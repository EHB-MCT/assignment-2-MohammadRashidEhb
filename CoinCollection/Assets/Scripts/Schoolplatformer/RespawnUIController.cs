using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RespawnUIController : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private TextMeshProUGUI respawnMessage;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float messageDuration = 1.5f;

    public static RespawnUIController Instance;

    private void Awake()
    {
        Instance = this;
        if (fadePanel != null)
            fadePanel.color = new Color(0, 0, 0, 0);
        if (respawnMessage != null)
            respawnMessage.text = "";
    }

    public void ShowRespawnSequence(System.Action onFadeComplete)
    {
        StartCoroutine(RespawnSequenceCoroutine(onFadeComplete));
    }

    private IEnumerator RespawnSequenceCoroutine(System.Action onFadeComplete)
    {
        // Show message
        if (respawnMessage != null)
            respawnMessage.text = "You Fell\nSpawning to last checkpoint...";

        // Fade out
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        // Wait for message
        yield return new WaitForSeconds(messageDuration);

        // Callback to respawn the player
        onFadeComplete?.Invoke();

        // Fade in
        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        // Hide message
        if (respawnMessage != null)
            respawnMessage.text = "";
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            if (fadePanel != null)
                fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        if (fadePanel != null)
            fadePanel.color = new Color(0, 0, 0, to);
    }
}
