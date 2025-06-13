using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class FinishUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup finishPanel;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject playAgainButton; 

    public static FinishUIController Instance;



    void Awake()
    {
        Instance = this;
        finishPanel.alpha = 0;
        finishPanel.blocksRaycasts = false;
        finishPanel.gameObject.SetActive(false);
    }

   public void ShowFinishScreen(string time, int falls)
{
    finishPanel.gameObject.SetActive(true);
    statsText.text = $"<size=150%><b>FINISHED!</b></size>\n\nTime: <color=#FFD700>{time}</color>\nFalls: <color=#FF5555>{falls}</color>";
    StartCoroutine(FadeIn());
}

private IEnumerator FadeIn()
{
    float elapsed = 0;
    while (elapsed < fadeDuration)
    {
        elapsed += Time.deltaTime;
        finishPanel.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
        yield return null;
    }
    finishPanel.alpha = 1;
    finishPanel.blocksRaycasts = true;
    finishPanel.interactable = true;

    // Unlock and show the cursor
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    // Set the first selected button for controller navigation
    if (playAgainButton != null)
        EventSystem.current.SetSelectedGameObject(playAgainButton);
}


    public void PlayAgain()
    {
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToCity()
    {
        // Loads the main city scene by name
        SceneManager.LoadScene("City 2");
    }


}

